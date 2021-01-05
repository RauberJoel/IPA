using ScrumRetroApp.Data.Repositories;

namespace ScrumRetroApp.API.Services
{
    public interface IDatabaseService
    {
        IBenutzerRepository RepoBenutzer { get; set; }
    }
}