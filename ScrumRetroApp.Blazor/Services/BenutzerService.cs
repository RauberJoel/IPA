using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Blazor.Services
{
	public class BenutzerService : IBenutzerService
	{
		#region Fields
		private readonly HttpClient _httpClient;
		#endregion

		#region Properties
		[Inject]
		private ISession _session { get; }
		#endregion

		#region Constructors
		public BenutzerService(HttpClient client, ISession session)
		{
			_httpClient = client;
			this._session = session;
		}
		#endregion
		public async Task<int> CreateBenutzer(BenutzerDTO dto)
		{
			return await JsonSerializer.DeserializeAsync<int>
				(await _httpClient.GetStreamAsync("api/benutzer/create/" + dto),
				 new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		}

		public async Task EditBenutzer(BenutzerDTO dto)
		{
			await _httpClient.GetStreamAsync("api / benutzer / create / " + dto);
		}

		public async Task RemoveBenutzer(int nBenutzerId)
		{
			await _httpClient.GetStreamAsync("api / benutzer / create / " + nBenutzerId);
		}

		public async Task<bool> Login(string strMail, string strPasswort)
		{
			return await JsonSerializer.DeserializeAsync<bool>
				(await _httpClient.GetStreamAsync("api/benutzer/create/" + strMail + "/" + strPasswort),
				 new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		}
	}
}