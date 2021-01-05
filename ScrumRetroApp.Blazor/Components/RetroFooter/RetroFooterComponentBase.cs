using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using ScrumRetroApp.API.Services;
using ScrumRetroApp.Blazor.Services;
using ScrumRetroApp.Shared.DTOs;
using IBenutzerService = ScrumRetroApp.Blazor.Services.IBenutzerService;
using IRetroService = ScrumRetroApp.Blazor.Services.IRetroService;

namespace ScrumRetroApp.Blazor.Components
{
	public class RetroFooterComponentBase : ComponentBase
	{
		#region Properties
		[Inject]
		public ISession Session { get; set; }

		protected bool ShowStartButton => this.Session.IsScrumMaster && this.Session.RetroStep == RetroStep.Pending;

		protected bool ShowGoButton => this.Session.IsScrumMaster && this.Session.RetroStep != RetroStep.Pending &&
		                               this.Session.RetroStep != RetroStep.Evaluation && this.Session.ActiveRetro;

		protected bool ShowReadyButton => this.Session.RetroStep != RetroStep.Pending &&
		                                  this.Session.RetroStep != RetroStep.Evaluation && this.Session.ActiveRetro;

		protected bool ShowEndButton => this.Session.IsScrumMaster && this.Session.RetroStep == RetroStep.Evaluation && this.Session.ActiveRetro;

		[Inject]
		private IBenutzerService _currentParticipantsService { get; set; }

		[Inject]
		private IRetroService _retroService { get; set; }

		[Inject]
		private ITFSService tfsService { get; set; }

		[Inject]
		private NavigationManager Manager { get; set; }
		#endregion

		#region Publics
		public void OnStartClicked()
		{
			ChangeStep(RetroStep.EmotionalState, true);
		}

		public async Task OnEndClicked()
		{
			this.Session.ActiveRetro = false;
			await this._retroService.SetRetroStatus();
			await this._currentParticipantsService.RemoveAllActiveUsers();
			ChangeStep("", true);
		}

		public async Task OnGoClicked()
		{
			switch(this.Session.RetroStep)
			{
				case RetroStep.EmotionalState:
					await this._currentParticipantsService.SetAllUsersNotReady();
					ChangeStep(RetroStep.EmotionalStateRepresentation, true);
					break;
				case RetroStep.EmotionalStateRepresentation:
					await this._currentParticipantsService.SetAllUsersNotReady();
					ChangeStep(RetroStep.Collect, true);
					break;
				case RetroStep.Collect:
					await this._currentParticipantsService.SetAllUsersNotReady();
					ChangeStep(RetroStep.Group, true);
					break;
				case RetroStep.Group:
					await this._currentParticipantsService.SetAllUsersNotReady();
					ChangeStep(RetroStep.Vote, true);
					break;
				case RetroStep.Vote:
					await this._currentParticipantsService.SetAllUsersNotReady();
					ChangeStep(RetroStep.Focus, true);
					break;
				case RetroStep.Focus:
					await this._currentParticipantsService.SetAllUsersNotReady();
					ChangeStep(RetroStep.Evaluation, true);
					break;
			}

			this.Session.UserURLImagesRetroFooter.Clear();
			await this._retroService.SetRetroStep();
		}

		public void Refresh()
		{
			StateHasChanged();
		}

		public async void ChangeStep(string retrostep, bool bSendToHub)
		{
			this.Session.RetroStep = retrostep;

			this.Manager.NavigateTo("/" + retrostep);

			if(bSendToHub)
			{
				await this.Session.HubConnection.SendAsync("ChangeStep", retrostep, this.Session.RetroId);
			}
		}
		#endregion

		#region Protecteds
		protected async Task OnReadyClicked()
		{
			this.Session.Ready = !this.Session.Ready;

			if(!this.Session.Ready)
			{
				await this._currentParticipantsService.SetUserNotReady();

				string userImage = await this.tfsService.GetProfilePicture(this.Session.UserMail);

				this.Session.UserURLImagesRetroFooter.Remove(userImage);

				await this.Session.HubConnection.SendAsync("SetNotReady", this.Session.UserURLImagesRetroFooter, this.Session.RetroId);
			}

			if(this.Session.Ready)
			{
				await this._currentParticipantsService.SetUserReady();

				string userImage = await this.tfsService.GetProfilePicture(this.Session.UserMail);

				this.Session.UserURLImagesRetroFooter.Add(userImage);

				await this.Session.HubConnection.SendAsync("SetReady", this.Session.UserURLImagesRetroFooter, this.Session.RetroId);
			}
		}
		#endregion
	}
}