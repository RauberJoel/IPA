using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.Identity;
using Microsoft.VisualStudio.Services.Identity.Client;
using Microsoft.VisualStudio.Services.WebApi;
using ScrumRetroApp.API.Controllers;

namespace ScrumRetroApp.API.Services
{
	public class TFSService : ITFSService
	{
		#region Fields
		private readonly ILogger<TFSController> _logger;

		private VssConnection _tfs;
		#endregion

		#region Properties
		public VssConnection Tfs
		{
			get
			{
				if(_tfs != null) return _tfs;

				VssCredentials credentials = this.VssCredentials;
				_tfs = credentials != null ? new VssConnection(new Uri("https://tfs.m-s.ch/tfs/DefaultCollection"), credentials) : null;

				return _tfs;
			}
		}

		public ICredentials Credentials => CredentialCache.DefaultNetworkCredentials;

		private VssCredentials VssCredentials
		{
			get
			{
				ICredentials credentials = this.Credentials;
				return credentials != null ? new VssCredentials(new WindowsCredential(credentials)) : new VssCredentials();
			}
		}
		#endregion

		#region Constructors
		public TFSService(ILogger<TFSController> logger)
		{
			_logger = logger;
		}
		#endregion

		#region Publics
		public async Task<string> GetProfilePicture(string strMail)
		{
			IdentitiesCollection identity = await GetIdentity(strMail);

			string strURL = "https://tfs.m-s.ch/tfs/DefaultCollection/_API/_common/IdentityImage?id=" + identity.FirstOrDefault()?.Id;

			return strURL;
		}
		#endregion

		#region Privates
		private async Task<IdentitiesCollection> GetIdentity(string strMail)
		{
			IdentityHttpClient accountClient = this.Tfs.GetClient<IdentityHttpClient>();
			return await accountClient.ReadIdentitiesAsync(IdentitySearchFilter.MailAddress, strMail);
		}
		#endregion
	}
}