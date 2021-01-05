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
	public class CurrentParticipantsControllerTest
	{
		#region Fields
		private ICurrentParticipantsController _currentParticipantsController;

		private ICurrentParticipantsService _currentParticipantsService;

		private ILogger<CurrentParticipantsController> _logger;

		private readonly int nRetroId = 1;
		#endregion

		#region Initialize and Cleanup
		[TestInitialize]
		public void InitializeTest()
		{
			_currentParticipantsService = Substitute.For<ICurrentParticipantsService>();
			_logger = Substitute.For<ILogger<CurrentParticipantsController>>();
			_currentParticipantsController = new CurrentParticipantsController(_currentParticipantsService, _logger);
		}
		#endregion

		#region Tests
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestCreateActiveUser_RetroIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsController.CreateActiveUser(-1, 1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestCreateActiveUser_UserIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsController.CreateActiveUser(1, -1);
		}

		[TestMethod]
		public void TestCreateActiveUser_ValidArguments_ReturnActiveUserId()
		{
			// Arrange
			_currentParticipantsService.CreateActiveUser(Arg.Any<int>(), Arg.Any<int>()).Returns(1);

			// Act
			IActionResult result = _currentParticipantsController.CreateActiveUser(1, 1);

			// Assert
			OkObjectResult okObjectResult = result as OkObjectResult;
			Assert.IsNotNull(okObjectResult);
			int activeUserId = (int) okObjectResult.Value;
			Assert.AreEqual(1, activeUserId);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestRemoveActiveUser_RetroIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsController.RemoveActiveUser(-1, 1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestRemoveActiveUser_UserIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsController.RemoveActiveUser(1, -1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestGetActiveUsersByRetro_RetroIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsController.GetActiveUsersByRetro(-1);
		}

		[TestMethod]
		public void TestGetActiveUsersByRetro_ValidArguments_ReturnActiveUserDTOs()
		{
			// Arrange
			List<ActiveUserDTO> listtDto = new List<ActiveUserDTO>();
			ActiveUserDTO dto = new ActiveUserDTO
			                    {
				                    Id = 1,
				                    UserId = 1,
				                    RetroId = nRetroId
			                    };
			listtDto.Add(dto);

			_currentParticipantsService.GetActiveUsersByRetro(nRetroId).Returns(listtDto);

			// Act
			IActionResult result = _currentParticipantsController.GetActiveUsersByRetro(nRetroId);

			// Assert
			OkObjectResult okObjectResult = result as OkObjectResult;
			Assert.IsNotNull(okObjectResult);
			List<ActiveUserDTO> dtoResult = (List<ActiveUserDTO>) okObjectResult.Value;
			Assert.AreEqual(listtDto, dtoResult);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestSetUserReady_RetroIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsController.SetUserReady(-1, 1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestSetUserReady_UserIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsController.SetUserReady(1, -1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestSetUserNotReady_RetroIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsController.SetUserNotReady(-1, 1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestSetUserNotReady_UserIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsController.SetUserNotReady(1, -1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestSetAllUsersNotReady_RetroIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsController.SetAllUsersNotReady(-1);
		}
		#endregion
	}
}