using Microsoft.AspNetCore.Mvc;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.API.Controllers
{
	public interface IBenutzerController
	{
		IActionResult CreateBenutzer([FromBody] BenutzerDTO dtoBenutzer);
		IActionResult EditBenutzer([FromBody] BenutzerDTO dtoBenutzer);
		IActionResult RemoveBenutzer(int nBenutzerId);
		IActionResult Login(string strMail, string strPasswort);
		IActionResult GetAllBenutzer();
	}
}