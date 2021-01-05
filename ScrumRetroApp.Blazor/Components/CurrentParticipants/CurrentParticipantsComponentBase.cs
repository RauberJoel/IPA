using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using ScrumRetroApp.API.Services;
using ScrumRetroApp.Blazor.Services;
using ScrumRetroApp.Shared.DTOs;
using IBenutzerService = ScrumRetroApp.Blazor.Services.IBenutzerService;

namespace ScrumRetroApp.Blazor.Components
{
	public class CurrentParticipantsComponentBase : ComponentBase
	{
		#region Properties
		[Inject] 
		public ISession _session { get; set; }

		[Parameter]
		public Action OnUserJoined { get; set; }

		[Inject]
		private ITFSService tfsService { get; set; }

		[Inject]
		private IBenutzerService _currentParticipantsService { get; set; }
		#endregion

		#region Publics
		// CreateActiveUser wird entweder nach einem Create oder nach einem Join aufgerufen.
		// Bei einem Join sollte noch zusätzlich die aktiven User geladen werden und this.OnUserJoined() aufgerufen werden.
		public async void CreateActiveUser(bool? bInitial = false)
		{
			int activeUserId = await this._currentParticipantsService.CreateActiveUser();

			if(bInitial == false)
			{
				List<ActiveUserDTO> dtosActiveUser = await this._currentParticipantsService.GetActiveUsersByRetroId(this._session.RetroId);
				this._session.ActiveUsers = dtosActiveUser;
			}

			if(activeUserId >= 0)
			{
				string userImage = await this.tfsService.GetProfilePicture(this._session.UserMail);

				//TODO URL in ActiveUsersDTO
				this._session.UserURLImages.Add(userImage);

				await this._currentParticipantsService.JoinHub();
			}

			if(bInitial == false)
			{
				this.OnUserJoined();
			}

			StateHasChanged();
		}
		#endregion

		#region Protecteds
		protected async Task OnClickLeave()
		{
			await this._currentParticipantsService.RemoveActiveUser();

			string userImage = await this.tfsService.GetProfilePicture(this._session.UserMail);

			this._session.UserURLImages.Remove(userImage);

			await this._currentParticipantsService.LeaveHub();
		}

		protected void OnRefresh()
		{
			StateHasChanged();
		}
		#endregion
	}
}