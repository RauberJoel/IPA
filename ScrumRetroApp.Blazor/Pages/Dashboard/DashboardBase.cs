using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using ScrumRetroApp.Blazor.Services;
using ScrumRetroApp.Data.Models;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Blazor.Pages
{
	public class DashboardBase : ComponentBase
	{

		protected List<BenutzerDTO> dtosBenutzer;

		#region Properties
		[Inject]
		private ISession _session { get; set; }

		[Inject]
		private IBenutzerService _benutzerService { get; set; }

		[Parameter]
		public EventCallback OnLoggedInEvent { get; set; }

		[Inject]
		public NavigationManager Manager { get; set; }

		[Inject]
		public ITFSService _tfsService { get; set; }

		protected string imagesrc;
		#endregion

		public async void RemoveColumn(BenutzerDTO dto)
		{
			await this._benutzerService.RemoveBenutzer(dto.Id);

			dtosBenutzer.Remove(dto);

			StateHasChanged();
		}

		public void Logout()
		{
			this._session.LoggedIn = false;
			this._session.Email = null;

			this.Manager.NavigateTo("/");
		}

		protected async override Task OnInitializedAsync()
		{
			if(!this._session.LoggedIn) this.Manager.NavigateTo("/");

			Refresh();

			Task.Run(SetNecessaryData).Wait();

			await base.OnInitializedAsync();
		}

		private void SetNecessaryData()
		{
			if(this._session.Email != null)
			{
				imagesrc = this._tfsService.GetProfilePicture(this._session.Email).Result;
			}

			dtosBenutzer = new List<BenutzerDTO>();

			dtosBenutzer = this._benutzerService.GetAllBenutzer().Result;

			foreach(BenutzerDTO dto in dtosBenutzer.Where(dto => dto.Mail == this._session.Email))
			{
				this._session.Admin = dto.Admin;
			}
		}

		private void Refresh()
		{
			this.OnLoggedInEvent.InvokeAsync(true);
		}

		public async void AddColumn()
		{
			BenutzerDTO dtoBenutzer = new BenutzerDTO()
			                          {
										  Vorname = "Type here",
										  Name = "Type here",
										  Mail = "Type here",
										  Passwort = ""
			                          };

			dtoBenutzer.Id = await this._benutzerService.CreateBenutzer(dtoBenutzer);

			dtosBenutzer.Add(dtoBenutzer);

			StateHasChanged();
		}

		public async void OnChanged(BenutzerDTO dto, string strType, string strValue)
		{
			switch(strType)
			{
				case ("Vorname"):
					dto.Vorname = strValue;
					break;
				case ("Name"):
					dto.Name = strValue;
					break;
				case ("Mail"):
					dto.Mail = strValue;
					break;
				case ("Blockiert"):
					dto.Blockiert = !dto.Blockiert;
					break;
			}

			await this._benutzerService.EditBenutzer(dto);
		}
	}
}