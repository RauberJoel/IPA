/*using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ScrumRetroApp.API.Controllers;
using ScrumRetroApp.API.Services;

namespace ScrumRetroApp.Test.API.Controllers
{
	[TestClass]
	public class TFSControllerTest
	{
		#region Fields
		private ITFSController _TFSController;

		private ITFSService _TFSService;

		private ILogger<TFSController> _logger;

		private string _strMail = "joel.rauber@m-s.ch";
		#endregion

		#region Initialize and Cleanup
		[TestInitialize]
		public void InitializeTest()
		{
			_TFSService = Substitute.For<ITFSService>();
			_logger = Substitute.For<ILogger<TFSController>>();
			_TFSController = new TFSController(_TFSService, _logger);
		}
		#endregion

		#region Tests
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestGetTeam_MailNull_ArgumentNullException()
		{
			// Arrange 
			_strMail = null;

			// Act 
			_TFSController.GetTeam(_strMail);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestGetYourTeamMembers_MailNull_ArgumentNullException()
		{
			// Arrange 
			_strMail = null;

			// Act 
			_TFSController.GetYourTeamMembers(_strMail);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestGetDisplayName_MailNull_ArgumentNullException()
		{
			// Arrange 
			_strMail = null;

			// Act 
			_TFSController.GetDisplayName(_strMail);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestGetProfilePicture_MailNull_ArgumentNullException()
		{
			// Arrange 
			_strMail = null;

			// Act 
			_TFSController.GetProfilePicture(_strMail);
		}

		[TestMethod]
		public void TestGetTeam_AllGood_ShouldReturnTeamname()
		{
			// Arrange
			string strTeamname = "AHV Team 3";

			_TFSService.GetTeam(Arg.Any<string>())
			           .Returns(strTeamname);

			// Act
			IActionResult actionResult = _TFSController.GetTeam(_strMail);

			// Assert
			OkObjectResult okObjectResult = actionResult as OkObjectResult;
			Assert.IsNotNull(okObjectResult);

			string strResult = okObjectResult.Value as string;
			Assert.IsNotNull(strResult);

			Assert.AreEqual(strTeamname, strResult);
		}

		[TestMethod]
		public void TestGetYourTeamMembers_AllGood_ShouldReturnTeamMembers()
		{
			// Arrange
			List<string> strTeamMembers = new List<string>
			                              {
				                              "Rauber, Joël",
				                              "Tikue, Yo-EL"
			                              };

			_TFSService.GetYourTeamMembers(Arg.Any<string>())
			           .Returns(strTeamMembers);

			// Act
			IActionResult actionResult = _TFSController.GetYourTeamMembers(_strMail);

			// Assert
			OkObjectResult okObjectResult = actionResult as OkObjectResult;
			Assert.IsNotNull(okObjectResult);

			List<string> strResult = okObjectResult.Value as List<string>;
			Assert.IsNotNull(strResult);

			Assert.AreEqual(strTeamMembers, strResult);
		}

		[TestMethod]
		public void TestGetCurrentSprint_AllGood_ShouldReturnCurrentSprint()
		{
			// Arrange
			string strSprint = "Sprint 2020-10-12";

			_TFSService.GetCurrentSprint()
			           .Returns(strSprint);

			// Act
			IActionResult actionResult = _TFSController.GetCurrentSprint();

			// Assert
			OkObjectResult okObjectResult = actionResult as OkObjectResult;
			Assert.IsNotNull(okObjectResult);

			string strResult = okObjectResult.Value as string;
			Assert.IsNotNull(strResult);

			Assert.AreEqual(strSprint, strResult);
		}

		[TestMethod]
		public void TestGetAllSprints_AllGood_ShouldReturnAllSprints()
		{
			// Arrange
			List<string> strAllSprints = new List<string>
			                             {
				                             "Sprint 2020-10-12",
				                             "Sprint 2020-10-26"
			                             };

			_TFSService.GetAllSprints()
			           .Returns(strAllSprints);

			// Act
			IActionResult actionResult = _TFSController.GetAllSprints();

			// Assert
			OkObjectResult okObjectResult = actionResult as OkObjectResult;
			Assert.IsNotNull(okObjectResult);

			List<string> strResult = okObjectResult.Value as List<string>;
			Assert.IsNotNull(strResult);

			Assert.AreEqual(strAllSprints, strResult);
		}

		[TestMethod]
		public void TestGetDisplayName_AllGood_ShouldReturnDisplayName()
		{
			// Arrange
			string strDisplayName = "Rauber, Joël";

			_TFSService.GetDisplayName(_strMail)
			           .Returns(strDisplayName);

			// Act
			IActionResult actionResult = _TFSController.GetDisplayName(_strMail);

			// Assert
			OkObjectResult okObjectResult = actionResult as OkObjectResult;
			Assert.IsNotNull(okObjectResult);

			string strResult = okObjectResult.Value as string;
			Assert.IsNotNull(strResult);

			Assert.AreEqual(strDisplayName, strResult);
		}
		#endregion
	}
}*/