using System;
using System.Collections.Generic;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.API.Services
{
	public interface IBenutzerService
	{
		int CreateBenutzer(BenutzerDTO dto);
		void EditBenutzer(BenutzerDTO dto);
		void RemoveBenutzer(int nBenutzerId);
		bool Login(string strMail, string strPasswort);
		List<BenutzerDTO> GetAllBenutzer();
	}
}