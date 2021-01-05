using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ScrumRetroApp.API.Services;
using ScrumRetroApp.Data.Repositories;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Test.API.Controllers
{
	[TestClass]
	public class CurrentParticipantsServiceTest
	{
		#region Fields
		private ICurrentParticipantsService _currentParticipantsService;

		private IDatabaseService _databaseService;

		private IActiveUserRepository _activeUserRepository;

		private ILogger<CurrentParticipantsService> _logger;

		private readonly int _nRetroId = 1;
		#endregion

		#region Initialize and Cleanup
		[TestInitialize]
		public void InitializeTest()
		{
			_activeUserRepository = Substitute.For<IActiveUserRepository>();
			_databaseService = Substitute.For<IDatabaseService>();
			_databaseService.RepoActiveUser = _activeUserRepository;
			_logger = Substitute.For<ILogger<CurrentParticipantsService>>();

			_currentParticipantsService = new CurrentParticipantsService(_databaseService, _logger);
		}
		#endregion

		#region Tests
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestGetActiveUsersByRetro_RetroIdInvalid_ArgumentOutOfRangeException()
		{
			// Act 
			_currentParticipantsService.GetActiveUsersByRetro(-1);
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
				                    RetroId = 1
			                    };
			listtDto.Add(dto);

			_currentParticipantsService.GetActiveUsersByRetro(_nRetroId).Returns(listtDto);

			// Act
			List<ActiveUserDTO> result = _currentParticipantsService.GetActiveUsersByRetro(_nRetroId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(listtDto, result);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestCreateActiveUser_RetroIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsService.CreateActiveUser(-1, 1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestCreateActiveUser_UserIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsService.CreateActiveUser(1, -1);
		}

		[TestMethod]
		public void TestCreateActiveUser_ValidArguments_ReturnActiveUserId()
		{
			// Arrange
			_activeUserRepository.Create(Arg.Any<ActiveUserDTO>()).Returns(1);

			// Act
			int result = _currentParticipantsService.CreateActiveUser(1, 1);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestRemoveActiveUser_RetroIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsService.RemoveActiveUser(1, -1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestRemoveActiveUser_UserIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsService.RemoveActiveUser(1, -1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestSetUserReady_UserIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsService.SetUserReady(1, -1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestSetUserReady_RetroIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsService.SetUserReady(-1, 1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestSetUserNotReady_UserIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsService.SetUserReady(1, -1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestSetUserNotReady_RetroIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsService.SetUserReady(-1, 1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestSetAllUsersNotReady_UserIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsService.SetUserReady(1, -1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestSetAllUsersNotReady_RetroIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_currentParticipantsService.SetUserReady(-1, 1);
		}
		#endregion
	}
}