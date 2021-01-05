using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ScrumRetroApp.API.Services;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Test.API.Controllers
{
	[TestClass]
	public class TeamServiceTest
	{
		#region Fields
		private ITeamService _teamService;

		private IDatabaseService _databaseService;

		private ILogger<TeamService> _logger;
		#endregion

		#region Initialize and Cleanup
		[TestInitialize]
		public void InitializeTest()
		{
			_databaseService = Substitute.For<IDatabaseService>();
			_logger = Substitute.For<ILogger<TeamService>>();
			_teamService = new TeamService(_databaseService, _logger);
		}
		#endregion

		#region Tests
		[TestMethod]
		public void TestGetUser_AlreadyAvailable_ShouldReturnUser()
		{
			// Arrange
			string strName = "AHV Team 3";

			TeamDTO dto = new TeamDTO
			              {
				              Id = 1,
				              Name = strName
			              };

			_databaseService.RepoTeam.GetTeamByName(Arg.Any<string>())
			                .Returns(dto);

			// Act 
			TeamDTO dtoResult = _teamService.GetTeam(strName);

			// Assert
			Assert.AreEqual(dto, dtoResult);
		}

		[TestMethod]
		public void TestGetUser_NotAvailable_ShouldReturnNewUser()
		{
			// Arrange
			string strName = "AHV Team 3";
			int nTeamId = 1;

			TeamDTO dto = null;

			_databaseService.RepoTeam.GetTeamByName(Arg.Any<string>())
			                .Returns(dto);

			_databaseService.RepoTeam.CreateTeam(Arg.Any<TeamDTO>())
			                .Returns(nTeamId);

			// Act 
			TeamDTO dtoResult = _teamService.GetTeam(strName);

			// Assert
			Assert.AreEqual(nTeamId, dtoResult.Id);
		}
		#endregion
	}
}