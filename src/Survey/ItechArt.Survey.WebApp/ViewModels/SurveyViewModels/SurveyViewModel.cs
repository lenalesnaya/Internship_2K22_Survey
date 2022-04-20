using System;
using ItechArt.Survey.WebApp.ViewModels.UserViewModels;

namespace ItechArt.Survey.WebApp.ViewModels.SurveyViewModels;

public class SurveyViewModel
{
    public long Id { get; set; }

    public string Title { get; set; }

    public bool IsAnonymous { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime LastUpdateDate { get; set; }

    public int СreatorId { get; set; }


    public UserViewModel Сreator { get; set; }
}