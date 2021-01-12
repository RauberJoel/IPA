using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Blazor.Services
{
	public interface IBenutzerService
	{
		Task<int> CreateBenutzer(BenutzerDTO dtoBenutzer);
		Task EditBenutzer(BenutzerDTO dtoBenutzer);
		Task RemoveBenutzer(int nBenutzerId);
		Task<bool> Login(string strMail, string strPasswort);
		Task<List<BenutzerDTO>> GetAllBenutzer();
	}
}