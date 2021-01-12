using System;
using System.Collections.Generic;
using ScrumRetroApp.Data.Models;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Data.Repositories
{
	public interface IBenutzerRepository
	{
		int CreateBenutzer(BenutzerDTO dto);
		void EditBenutzer(BenutzerDTO dto);
		void RemoveBenutzer(int nBenutzerId);
		bool Login(string strMail, string strPasswort);
		List<BenutzerDTO> GetAllBenutzer();
	}
}