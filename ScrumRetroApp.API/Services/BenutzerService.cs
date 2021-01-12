using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.API.Services
{
	public class BenutzerService : IBenutzerService
	{
		#region Fields
		private readonly ILogger<BenutzerService> _logger;

		private readonly IDatabaseService _databaseSerivce;
		#endregion

		#region Constructors
		public BenutzerService(IDatabaseService _databaseSerivce,
		                    ILogger<BenutzerService> logger)
		{
			this._databaseSerivce = _databaseSerivce;
			_logger = logger;
		}
		#endregion

		#region Publics
		public int CreateBenutzer(BenutzerDTO dto)
		{
			//TODO falls Benutzer schon vorhanden

			return _databaseSerivce.RepoBenutzer.CreateBenutzer(dto);
		}

		public void EditBenutzer(BenutzerDTO dto)
		{
			_databaseSerivce.RepoBenutzer.EditBenutzer(dto);
		}

		public void RemoveBenutzer(int nBenutzerId)
		{
			_databaseSerivce.RepoBenutzer.RemoveBenutzer(nBenutzerId);
		}

		public bool Login(string strMail, string strPasswort)
		{
			return _databaseSerivce.RepoBenutzer.Login(strMail, strPasswort);
		}

		public List<BenutzerDTO> GetAllBenutzer()
		{
			return _databaseSerivce.RepoBenutzer.GetAllBenutzer();
		}
		#endregion
	}
}