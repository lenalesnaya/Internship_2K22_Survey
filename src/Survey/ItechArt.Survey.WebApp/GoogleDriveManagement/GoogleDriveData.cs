using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using System.Collections.Generic;

namespace ItechArt.Survey.WebApp.GoogleDriveManagement;

public class GoogleDriveData
{
    public static string[] Scopes { get; set; } = { DriveService.Scope.DriveFile };  //Массив для работы с файлами
    public static string ApplicationName { get; set; } = "Survey";      //Наименование программы
    public static UserCredential Credential { get; set; } = null;             //Ключи авторизации
    public static string Extensions { get; set; } = "image/*";                   //Расширение для сохраняемых файлов
}