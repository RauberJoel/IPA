/*using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ScrumRetroApp.API.Services;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Test.API.Controllers
{
	[TestClass]
	public class UserServiceTest
	{
		#region Fields
		private IUserService _userService;

		private IDatabaseService _databaseService;

		private ILogger<UserService> _logger;
		#endregion

		#region Initialize and Cleanup
		[TestInitialize]
		public void InitializeTest()
		{
			_databaseService = Substitute.For<IDatabaseService>();
			_logger = Substitute.For<ILogger<UserService>>();
			_userService = new UserService(_databaseService, _logger);
		}
		#endregion

		#region Tests
		[TestMethod]
		public void TestGetUser_AlreadyAvailable_ShouldReturnUser()
		{
			// Arrange
			string strName = "Rauber, Joël";
			string strMail = "joel.rauber@m-s.ch";
			int nTeamId = 1;

			UserDTO dto = new UserDTO
			              {
				              Id = 1,
				              TeamId = nTeamId,
				              Name = strName,
				              Mail = strMail
			              };

			_databaseService.RepoUser.GetUserDTOByMail(Arg.Any<string>())
			                .Returns(dto);

			// Act 
			UserDTO dtoResult = _userService.GetUser(strName, strMail, nTeamId);

			// Assert
			Assert.AreEqual(dto, dtoResult);
		}

		[TestMethod]
		public void TestGetUser_NotAvailable_ShouldReturnNewUser()
		{
			// Arrange
			string strName = "Rauber, Joël";
			string strMail = "joel.rauber@m-s.ch";
			int nTeamId = 1;
			int nUserId = 1;

			UserDTO dto = null;

			_databaseService.RepoUser.GetUserDTOByMail(Arg.Any<string>())
			                .Returns(dto);

			_databaseService.RepoUser.CreateUser(Arg.Any<UserDTO>())
			                .Returns(nUserId);

			// Act 
			UserDTO dtoResult = _userService.GetUser(strName, strMail, nTeamId);

			// Assert
			Assert.AreEqual(nUserId, dtoResult.Id);
		}
		#endregion
	}
}*/