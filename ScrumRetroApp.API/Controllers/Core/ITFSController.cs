using Microsoft.AspNetCore.Mvc;

namespace ScrumRetroApp.API.Controllers
{
	public interface ITFSController
	{
		IActionResult GetProfilePicture(string strMail);
	}
}