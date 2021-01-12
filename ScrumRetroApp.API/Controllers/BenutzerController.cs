using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScrumRetroApp.API.Services;
using ScrumRetroApp.Data.Models;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BenutzerController : ControllerBase, IBenutzerController
	{
		#region Fields
		private readonly ILogger<BenutzerController> _logger;

		private readonly IBenutzerService _benutzerService;
		#endregion

		#region Constructors
		public BenutzerController(IBenutzerService benutzerService,
		                       ILogger<BenutzerController> logger)
		{
			_benutzerService = benutzerService;
			_logger = logger;
		}
		#endregion

		#region Publics
		[HttpPost("create")]
		public IActionResult CreateBenutzer([FromBody] BenutzerDTO dtoBenutzer)
		{
			if(dtoBenutzer == null)
			{
				_logger.LogError($"{nameof(dtoBenutzer)} should not be null");
				throw new ArgumentNullException(nameof(dtoBenutzer));
			}

			int nResult = _benutzerService.CreateBenutzer(dtoBenutzer);

			return Ok(nResult);
		}

		[HttpPost("edit")]
		public IActionResult EditBenutzer([FromBody] BenutzerDTO dtoBenutzer)
		{
			if(dtoBenutzer == null)
			{
				_logger.LogError($"{nameof(dtoBenutzer)} should not be null");
				throw new ArgumentNullException(nameof(dtoBenutzer));
			}

			_benutzerService.EditBenutzer(dtoBenutzer);

			return Ok();
		}

		[HttpGet("remove/{nBenutzerId}")]
		public IActionResult RemoveBenutzer(int nBenutzerId)
		{
			if(nBenutzerId <= 0)
			{
				_logger.LogError($"{nameof(nBenutzerId)} should not be null");
				throw new ArgumentNullException(nameof(nBenutzerId));
			}

			_benutzerService.RemoveBenutzer(nBenutzerId);

			return Ok();
		}

		[HttpGet("login/{strMail}/{strPasswort}")]
		public IActionResult Login(string strMail, string strPasswort)
		{
			if(strMail == null)
			{
				_logger.LogError($"{nameof(strMail)} should not be null");
				throw new ArgumentNullException(nameof(strMail));
			}

			if(strPasswort == null)
			{
				_logger.LogError($"{nameof(strPasswort)} should not be null");
				throw new ArgumentNullException(nameof(strPasswort));
			}

			bool bResult = _benutzerService.Login(strMail, strPasswort);

			return Ok(bResult);
		}

		[HttpGet("getall")]
		public IActionResult GetAllBenutzer()
		{
			List<BenutzerDTO> listResult = _benutzerService.GetAllBenutzer();

			return Ok(listResult);
		}
		#endregion
	}
}