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
	public class DashboardControllerTest
	{
		#region Fields
		private IDashboardController _dashboardController;

		private IDashboardService _dashboardService;

		private ILogger<DashboardController> _logger;

		private int _nTeamId = 1;
		#endregion

		#region Initialize and Cleanup
		[TestInitialize]
		public void InitializeTest()
		{
			_dashboardService = Substitute.For<IDashboardService>();
			_logger = Substitute.For<ILogger<DashboardController>>();
			_dashboardController = new DashboardController(_dashboardService, _logger);
		}
		#endregion

		#region Tests
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestGetDashboard_TeamIdNull_ArgumentOutOfRangeException()
		{
			// Arrange 
			_nTeamId = 0;

			// Act 
			_dashboardController.GetDashboard(_nTeamId);
		}

		[TestMethod]
		public void TestGetDashboard_AllGood_ShouldReturnDashboardDTO()
		{
			// Arrange
			DashboardDTO dto = new DashboardDTO
			                   {
				                   Id = 1,
				                   TeamId = _nTeamId
			                   };

			_dashboardService.GetDashboard(Arg.Any<int>())
			                 .Returns(dto);

			// Act 
			IActionResult actionResult = _dashboardController.GetDashboard(_nTeamId);

			// Assert
			OkObjectResult okObjectResult = actionResult as OkObjectResult;
			Assert.IsNotNull(okObjectResult);

			DashboardDTO dtoResult = okObjectResult.Value as DashboardDTO;
			Assert.IsNotNull(dtoResult);

			Assert.AreEqual(dto.Id, dtoResult.Id);
		}
		#endregion
	}
}