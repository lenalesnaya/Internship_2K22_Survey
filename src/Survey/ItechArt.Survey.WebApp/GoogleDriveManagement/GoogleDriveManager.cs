using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using ItechArt.Survey.WebApp.GoogleDriveManagement.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using File = Google.Apis.Drive.v3.Data.File;

namespace ItechArt.Survey.WebApp.GoogleDriveManagement;

public class GoogleDriveManager : IGoogleDriveManager
{
    public bool Authorize()
    {
        using (FileStream stream =
                 new("client_secret.json", FileMode.Open, FileAccess.Read))
        {
            try
            {
                var credPath = Environment.CurrentDirectory.ToString();
                credPath = Path.Combine(credPath, "drive-bridge.json");

                GoogleDriveData.Credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    GoogleDriveData.Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            catch (Exception)
            {
                GoogleDriveData.Credential = null;
            }

        }
        return GoogleDriveData.Credential != null;
    }

    public IList<File> GetFileList()
    {
        IList<File> result = null;
        if (GoogleDriveData.Credential == null)
            Authorize();
        if (GoogleDriveData.Credential == null)
        {
            return result;
        }
        // Create Drive API service.
        using (DriveService service = new(new BaseClientService.Initializer()
        {
            HttpClientInitializer = GoogleDriveData.Credential,
            ApplicationName = GoogleDriveData.ApplicationName,
        }))
        {
            try
            {
                // Define parameters of request.
                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.PageSize = 1000;
                listRequest.Fields = "nextPageToken, files(id, name, size)";

                // List files.
                result = listRequest.Execute().Files;
            }
            catch (Exception)
            {
                return null;
            }
        }
        return result;
    }

    public bool FileCreate(string name, byte[] value, out string id)
    {
        bool result = false;
        id = null;
        if (GoogleDriveData.Credential == null)
            Authorize();
        if (GoogleDriveData.Credential == null)
        {
            return result;
        }
        using (var service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = GoogleDriveData.Credential,
            ApplicationName = GoogleDriveData.ApplicationName,
        }))

        {
            var body = new File
            {
                Name = name,
                MimeType = GoogleDriveData.Extensions,
                ViewersCanCopyContent = true
            };

            using (var stream = new MemoryStream(value))
            {
                FilesResource.CreateMediaUpload request = service.Files.Create(body, stream, body.MimeType);
                if (request.Upload().Exception == null)
                { id = request.ResponseBody.Id; result = true; }
            }
        }
        return result;
    }

    public byte[] ReadFile(string fileId)
    {
        if (String.IsNullOrEmpty(fileId))
        {
            return null;
        }
        bool result = false;
        byte[] value = null;
        if (GoogleDriveData.Credential == null)
            Authorize();
        if (GoogleDriveData.Credential != null)
        {
            using (var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GoogleDriveData.Credential,
                ApplicationName = GoogleDriveData.ApplicationName,
            }))
            {
                FilesResource.GetRequest request = service.Files.Get(fileId);
                using (var stream = new MemoryStream())
                {
                    request.MediaDownloader.ProgressChanged += (IDownloadProgress progress) =>
                    {
                        if (progress.Status == DownloadStatus.Completed)
                            result = true;
                    };
                    request.Download(stream);

                    if (result)
                    {
                        value = stream.GetBuffer();
                    }
                }
            }
        }
        return value;
    }
}