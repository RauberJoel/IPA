using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace ScrumRetroApp.Blazor.Services
{
	public class TFSService : ITFSService
	{
		#region Fields
		private readonly HttpClient _httpClient;
		#endregion

		#region Properties
		[Inject]
		private ISession _session { get; }
		#endregion

		#region Constructors
		public TFSService(HttpClient client, ISession session)
		{
			_httpClient = client;
			this._session = session;
		}
		#endregion

		public async Task<string> GetProfilePicture(string strMail)
		{
			return await _httpClient.GetStringAsync("api/tfs/profilepicture/" + strMail);
		}
	}
}