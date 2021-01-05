using System;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ScrumRetroApp.API.Services;
using ScrumRetroApp.Data.Repositories;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Test.API.Services.Controllers
{
	[TestClass]
	public class RetroServiceTest
	{
		#region Fields
		private IRetroService _retroService;

		private IDatabaseService _databaseService;

		private IRetroRepository _retroRepository;

		private ILogger<RetroService> _logger;
		#endregion

		#region Initialize and Cleanup
		[TestInitialize]
		public void InitializeTest()
		{
			_retroRepository = Substitute.For<IRetroRepository>();
			_databaseService = Substitute.For<IDatabaseService>();
			_databaseService.RepoRetro = _retroRepository;
			_logger = Substitute.For<ILogger<RetroService>>();

			_retroService = new RetroService(_databaseService, _logger);
		}
		#endregion

		#region Tests
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestSetRetroStep_RetroIdInvalid_ArgumentOutOfRangeException()
		{
			// Act 
			_retroService.SetRetroStep(-1, RetroStep.Pending);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestSetRetroStep_RetroStepInvalid_ArgumentOutOfRangeException()
		{
			// Act 
			_retroService.SetRetroStep(1, null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestSetRetroStatus_RetroIdInvalid_ArgumentOutOfRangeException()
		{
			// Act 
			_retroService.SetRetroStatus(-1, true);
		}
		#endregion
	}
}