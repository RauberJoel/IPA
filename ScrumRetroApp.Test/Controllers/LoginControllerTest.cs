using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ScrumRetroApp.API.Controllers;
using ScrumRetroApp.API.Services;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Test.API.Controllers
{
	[TestClass]
	public class benutzerControllerTest
	{
		#region Fields
		private IBenutzerController _benutzerController;

		private IBenutzerService _benutzerService;

		private ILogger<BenutzerController> _logger;
		#endregion

		#region Initialize and Cleanup
		[TestInitialize]
		public void InitializeTest()
		{
			_benutzerService = Substitute.For<IBenutzerService>();
			_logger = Substitute.For<ILogger<BenutzerController>>();
			_benutzerController = new BenutzerController(_benutzerService, _logger);
		}
		#endregion

		#region Tests
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestCreateBenutzer_DTONull_ArgumentNullException()
		{
			// Arrange
			BenutzerDTO dto = null;

			// Act 
			_benutzerController.CreateBenutzer(dto);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestEditBenutzer_DTONull_ArgumentNullException()
		{
			// Arrange
			BenutzerDTO dto = null;

			// Act 
			_benutzerController.EditBenutzer(dto);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestRemoveBenutzer_BenutzerIdNull_ArgumentNullException()
		{
			// Arrange
			int nBenutzerId = 0;

			// Act 
			_benutzerController.RemoveBenutzer(nBenutzerId);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestLogin_MailNull_ArgumentNullException()
		{
			// Arrange
			string strMail = null;

			// Act 
			_benutzerController.Login(strMail, "");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestLogin_PasswortNull_ArgumentNullException()
		{
			// Arrange
			string strPasswort = null;

			// Act 
			_benutzerController.Login("", strPasswort);
		}

		[TestMethod]
		public void TestCreateBenutzer_AllGood_ShouldReturnBenutzerId()
		{
			// Arrange
			int nBenutzerId = 1;

			_benutzerService.CreateBenutzer(Arg.Any<BenutzerDTO>())
			                .Returns(nBenutzerId);

			BenutzerDTO dto = new BenutzerDTO
			                  {
				                  Id = 1,
				                  Vorname = "Joel",
				                  Name = "Rauber",
				                  Mail = "joel@rauber.ch",
				                  Passwort = ""
			                  };

			// Act 
			IActionResult actionResult = _benutzerController.CreateBenutzer(dto);

			// Assert
			OkObjectResult okObjectResult = actionResult as OkObjectResult;
			Assert.IsNotNull(okObjectResult);

			int nResult = (int) okObjectResult.Value;
			Assert.IsNotNull(nResult);

			Assert.AreEqual(nBenutzerId, nResult);
		}

		[TestMethod]
		public void TestLogin_AllGood_ShouldReturnTrue()
		{
			// Arrange
			_benutzerService.Login(Arg.Any<string>(),
			                       Arg.Any<string>())
			                .Returns(true);

			BenutzerDTO dto = new BenutzerDTO
			                  {
				                  Id = 1,
				                  Vorname = "Joel",
				                  Name = "Rauber",
				                  Mail = "joel@rauber.ch",
				                  Passwort = "Test"
			                  };

			// Act 
			IActionResult actionResult = _benutzerController.Login("joel@rauber.ch", "Test");

			// Assert
			OkObjectResult okObjectResult = actionResult as OkObjectResult;
			Assert.IsNotNull(okObjectResult);

			bool bResult = (bool) okObjectResult.Value;
			Assert.IsNotNull(bResult);
			Assert.IsTrue(bResult);
		}

		[TestMethod]
		public void TestLogin_WrongPassword_ShouldReturnFalse()
		{
			// Arrange
			_benutzerService.Login(Arg.Any<string>(),
			                       Arg.Any<string>())
			                .Returns(false);

			BenutzerDTO dto = new BenutzerDTO
			                  {
				                  Id = 1,
				                  Vorname = "Joel",
				                  Name = "Rauber",
				                  Mail = "joel@rauber.ch",
				                  Passwort = "Test"
			                  };

			// Act 
			IActionResult actionResult = _benutzerController.Login("joel@rauber.ch", "Testdf");

			// Assert
			OkObjectResult okObjectResult = actionResult as OkObjectResult;
			Assert.IsNotNull(okObjectResult);

			bool bResult = (bool) okObjectResult.Value;
			Assert.IsNotNull(bResult);
			Assert.IsFalse(bResult);
		}

		[TestMethod]
		public void TestGetAllBenutzer_AllGood_ShouldReturnDTOs()
		{
			// Arrange
			BenutzerDTO dto = new BenutzerDTO
			                  {
				                  Id = 1,
				                  Vorname = "Joel",
				                  Name = "Rauber",
				                  Mail = "joel@rauber.ch",
				                  Passwort = "Test"
			                  };

			List<BenutzerDTO> dtos = new List<BenutzerDTO>
			                         {
				                         dto
			                         };

			_benutzerService.GetAllBenutzer()
			                .Returns(dtos);

			// Act 
			IActionResult actionResult = _benutzerController.GetAllBenutzer();

			// Assert
			OkObjectResult okObjectResult = actionResult as OkObjectResult;
			Assert.IsNotNull(okObjectResult);

			List<BenutzerDTO> lstResult = (List<BenutzerDTO>) okObjectResult.Value;
			Assert.IsNotNull(lstResult);
			Assert.AreEqual(dtos, lstResult);
		}
		#endregion
	}
}