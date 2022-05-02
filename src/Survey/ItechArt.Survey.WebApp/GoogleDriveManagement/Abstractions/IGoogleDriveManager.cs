using System.Collections.Generic;
using File = Google.Apis.Drive.v3.Data.File;

namespace ItechArt.Survey.WebApp.GoogleDriveManagement.Abstractions;

public interface IGoogleDriveManager
{
    bool Authorize();

    IList<File> GetFileList();

    bool FileCreate(string name, byte[] value, out string id);

    byte[] ReadFile(string fileId);
}