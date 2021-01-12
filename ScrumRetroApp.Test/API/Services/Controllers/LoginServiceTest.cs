/*using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ScrumRetroApp.API.Services;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Test.API.Controllers
{
	[TestClass]
	public class LoginServiceTest
	{
		#region Fields
		private IBenutzerService _loginService;

		private ITeamService _teamService;
		private IUserService _userService;

		private ILogger<BenutzerService> _logger;

		private readonly int _nTeamId = 1;
		#endregion

		#region Initialize and Cleanup
		[TestInitialize]
		public void InitializeTest()
		{
			_teamService = Substitute.For<ITeamService>();
			_userService = Substitute.For<IUserService>();
			_logger = Substitute.For<ILogger<BenutzerService>>();

			_loginService = new BenutzerService(_teamService, _userService, _logger);
		}
		#endregion

		#region Tests
		[TestMethod]
		public void TestGetDashboard_AllGood_ShouldReturnUserDTO()
		{
			// Arrange
			string strName = "Rauber, Joël";
			string strMail = "joel.rauber@m-s.ch";
			string strTeamname = "AHV Team 3";
			
			TeamDTO dtoTeam = new TeamDTO
			                  {
				                  Id = _nTeamId,
				                  Name = strTeamname
			                  };

			_teamService.GetTeam(Arg.Any<string>())
			            .Returns(dtoTeam);

			UserDTO dtoUser = new UserDTO
			                  {
				                  Id = 1,
				                  TeamId = _nTeamId,
				                  Name = strName,
				                  Mail = strMail
			                  };

			_userService.GetUser(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>())
			            .Returns(dtoUser);

			// Act 
			UserDTO dtoResult = _loginService.GetUserByTeam(strName, strMail, strTeamname);

			// Assert
			Assert.AreEqual(dtoUser, dtoResult);
		}
		#endregion
	}
}*/