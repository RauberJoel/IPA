using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ScrumRetroApp.API.Services;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Test.API.Controllers
{
	[TestClass]
	public class DashboardServiceTest
	{
		#region Fields
		private IDashboardService _dashboardService;

		private IDatabaseService _databaseService;

		private ILogger<DashbaordService> _logger;

		private readonly int _nTeamId = 1;
		#endregion

		#region Initialize and Cleanup
		[TestInitialize]
		public void InitializeTest()
		{
			_databaseService = Substitute.For<IDatabaseService>();
			_logger = Substitute.For<ILogger<DashbaordService>>();
			_dashboardService = new DashbaordService(_databaseService, _logger);
		}
		#endregion

		#region Tests
		[TestMethod]
		public void TestGetDashboard_Available_ShouldReturnDashboard()
		{
			// Arrange
			string strTitle = "Dashboard von Team 3";

			DashboardDTO dto = new DashboardDTO
			                   {
				                   Id = 1,
				                   TeamId = _nTeamId,
				                   Title = strTitle
			                   };

			_databaseService.RepoDashboard.GetDashboard(Arg.Any<int>())
			                .Returns(dto);

			// Act 
			DashboardDTO dtoResult = _dashboardService.GetDashboard(_nTeamId);

			// Assert
			Assert.AreEqual(dto, dtoResult);
		}

		[TestMethod]
		public void TestGetDashboard_NotAvailable_ShouldReturnNewDashboard()
		{
			// Arrange
			string strTitle = "Dashboard von Team 3";

			DashboardDTO dto = null;

			_databaseService.RepoDashboard.GetDashboard(Arg.Any<int>())
			                .Returns(dto);

			// Act 
			DashboardDTO dtoResult = _dashboardService.GetDashboard(_nTeamId);

			// Assert
			Assert.AreEqual(dtoResult.Title, "");
			Assert.AreEqual(dtoResult.TeamId, _nTeamId);
		}
		#endregion
	}
}