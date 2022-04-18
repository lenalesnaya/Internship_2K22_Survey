using System;

namespace ItechArt.Survey.WebApp.ViewModels;

public class UserViewModel
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public DateTime RegistrationDate { get; set; }

    public int CountSurveys { get; set; }
}