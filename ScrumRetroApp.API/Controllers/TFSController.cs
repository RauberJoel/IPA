using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScrumRetroApp.API.Services;

namespace ScrumRetroApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TFSController : ControllerBase, ITFSController
	{
		#region Fields
		private readonly ILogger<TFSController> _logger;

		private readonly ITFSService _TFSService;
		#endregion

		#region Constructors
		public TFSController(ITFSService TFSService,
		                     ILogger<TFSController> logger)
		{
			_TFSService = TFSService;
			_logger = logger;
		}
		#endregion

		#region Publics
		[HttpGet("profilepicture/{strMail}")]
		public IActionResult GetProfilePicture(string strMail)
		{
			if(strMail == null)
			{
				_logger.LogError($"{nameof(strMail)} should not be null");
				throw new ArgumentNullException(nameof(strMail));
			}

			string strURL = _TFSService.GetProfilePicture(strMail).Result;

			return Ok(strURL);
		}
		#endregion
	}
}