using System;
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
		public void TestGetProfilePicture_MailNull_ArgumentNullException()
		{
			// Arrange 
			_strMail = null;

			// Act 
			_TFSController.GetProfilePicture(_strMail);
		}
		#endregion
	}
}