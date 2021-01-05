using System;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using ScrumRetroApp.Data.Models;
using ScrumRetroApp.Data.Repositories;

namespace ScrumRetroApp.API.Services
{
    public class DatabaseService : IDatabaseService
    {
        #region Properties

        public IBenutzerRepository RepoBenutzer { get; set; }

        #endregion

        #region Constructors

        public DatabaseService(string connectionString)
        {
            DbContext context = new IPAContext(connectionString);
            this.RepoBenutzer = new BenutzerRepository(context);
        }

        #endregion
    }
}