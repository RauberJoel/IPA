using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using ScrumRetroApp.Shared.DTOs;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

		#region Publics
		public async Task<int> CreateBenutzer(BenutzerDTO dto)
		{
			string json = JsonConvert.SerializeObject(dto);
			StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await _httpClient.PostAsync("api/benutzer/create/", data);
			int nResult = response.Content.ReadAsAsync<int>().Result;

			return nResult;
		}

		public async Task EditBenutzer(BenutzerDTO dto)
		{
			string json = JsonConvert.SerializeObject(dto);
			StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await _httpClient.PostAsync("api/benutzer/edit/", data);
		}

		public async Task RemoveBenutzer(int nBenutzerId)
		{
			await _httpClient.GetStreamAsync("api/benutzer/remove/" + nBenutzerId);
		}

		public async Task<bool> Login(string strMail, string strPasswort)
		{
			return await JsonSerializer.DeserializeAsync<bool>
				(await _httpClient.GetStreamAsync("api/benutzer/login/" + strMail + "/" + strPasswort),
				 new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		}

		public async Task<List<BenutzerDTO>> GetAllBenutzer()
		{
			return await JsonSerializer.DeserializeAsync<List<BenutzerDTO>>
				(await _httpClient.GetStreamAsync("api/benutzer/getall"),
				 new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		}
		#endregion
	}
}