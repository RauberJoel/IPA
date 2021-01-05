using System;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ScrumRetroApp.API.Controllers;
using ScrumRetroApp.API.Services;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Test.API.Controllers
{
	[TestClass]
	public class RetroControllerTest
	{
		#region Fields
		private IRetroController _retroController;

		private IRetroService _retroService;

		private ILogger<RetroController> _logger;
		#endregion

		#region Initialize and Cleanup
		[TestInitialize]
		public void InitializeTest()
		{
			_retroService = Substitute.For<IRetroService>();
			_logger = Substitute.For<ILogger<RetroController>>();
			_retroController = new RetroController(_retroService, _logger);
		}
		#endregion

		#region Tests
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestSetRetroStep_RetroIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_retroController.SetRetroStep(-1, RetroStep.EmotionalState);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void TestSetRetroStep_RetroStepInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_retroController.SetRetroStep(1, null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestSetRetroStatus_RetroIdInvalid_ArgumentOutOfRangeException()
		{
			// Act
			_retroController.SetRetroStatus(-1, true);
		}
		#endregion
	}
}