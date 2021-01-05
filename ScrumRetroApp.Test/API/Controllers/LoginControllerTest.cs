using System;
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
	public class LoginControllerTest
	{
		#region Fields
		private IBenutzerController _loginController;

		private IBenutzerService _loginService;

		private ILogger<BenutzerController> _logger;

		private string _strMail = "joel.rauber@m-s.ch";
		private readonly string _strUsername = "Rauber, Joël";
		private readonly string _strTeamname = "AHV Team 3";
		#endregion

		#region Initialize and Cleanup
		[TestInitialize]
		public void InitializeTest()
		{
			_loginService = Substitute.For<IBenutzerService>();
			_logger = Substitute.For<ILogger<BenutzerController>>();
			_loginController = new BenutzerController(_loginService, _logger);
		}
		#endregion

		#region Tests
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestGetUserByMailAndTeamName_MailNull_ArgumentNullException()
		{
			// Arrange
			_strMail = null;

			// Act 
			_loginController.GetUserByMailAndTeamName(_strMail, _strUsername, _strTeamname);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestGetUserByMailAndTeamName_UsernameNull_ArgumentNullException()
		{
			// Arrange
			_strMail = null;

			// Act 
			_loginController.GetUserByMailAndTeamName(_strMail, _strUsername, _strTeamname);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestGetUserByMailAndTeamName_TeamnameNull_ArgumentNullException()
		{
			// Arrange
			_strMail = null;

			// Act 
			_loginController.GetUserByMailAndTeamName(_strMail, _strUsername, _strTeamname);
		}

		[TestMethod]
		public void TestGetUserByMailAndTeamName_AllGood_ShouldReturnUserDTO()
		{
			// Arrange
			UserDTO dto = new UserDTO
			              {
				              Id = 1,
				              TeamId = 1,
				              Name = _strUsername,
				              Mail = _strMail
			              };

			_loginService.GetUserByTeam(Arg.Any<string>(),
			                            Arg.Any<string>(),
			                            Arg.Any<string>())
			             .Returns(dto);

			// Act 
			IActionResult actionResult = _loginController.GetUserByMailAndTeamName(_strMail, _strUsername, _strTeamname);

			// Assert
			OkObjectResult okObjectResult = actionResult as OkObjectResult;
			Assert.IsNotNull(okObjectResult);

			UserDTO dtoResult = okObjectResult.Value as UserDTO;
			Assert.IsNotNull(dtoResult);

			Assert.AreEqual(dto.Id, dtoResult.Id);
		}
		#endregion
	}
}