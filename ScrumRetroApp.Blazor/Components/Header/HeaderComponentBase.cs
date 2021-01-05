using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ScrumRetroApp.Blazor.Services;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Blazor.Components
{
	public class HeaderComponentBase : ComponentBase
	{
		#region Fields
		protected bool IsModalOpen;

		protected bool IsRetroHeader;
		#endregion

		#region Properties
		[Parameter]
		public Action OnChanged { get; set; }

		[Parameter]
		public EventCallback<RetroDTO> OnRetroCreatedCallback { get; set; }

		[Parameter]
		public EventCallback OnActiveUserJoinedCallback { get; set; }

		[Inject]
		public ISession _session { get; set; }

		[Inject]
		public NavigationManager Manager { get; set; }

		protected HamburgerMenuComponentBase HamburgerMenuComponent { get; set; }

		protected CurrentParticipantsComponentBase CurrentParticipant { get; set; }

		[Inject]
		private IRetroService _retroService { get; set; }

		#endregion

		#region Publics
		public void ShowNewRetroPopUp()
		{
			IsModalOpen = true;
			StateHasChanged();
		}

		public void Show()
		{
			this.HamburgerMenuComponent.Visible();
		}

		public void CancelCreate()
		{
			IsModalOpen = false;
			StateHasChanged();
		}

		public async void OnUserJoined()
		{
			RetroDTO dtoRetro = await this._retroService.GetRetroById(this._session.RetroId);
			this._session.ActiveRetro = true;
			IsRetroHeader = true;
			this._session.RetroStep = dtoRetro.Step;
			this._session.ScrumMasterId = dtoRetro.ScrumMasterId;

			ChangeStep(dtoRetro.Step);
		}
		#endregion

		#region Protecteds
		protected async Task OnCreatedRetro(RetroDTO dtoRetro)
		{
			this._session.ActiveRetro = true;
			IsModalOpen = false;
			IsRetroHeader = true;
			this.CurrentParticipant.CreateActiveUser(true);
			StateHasChanged();

			await this.OnRetroCreatedCallback.InvokeAsync(dtoRetro);
		}
		#endregion

		#region Privates
		private void ChangeStep(string retrostep)
		{
			this._session.RetroStep = retrostep;
			this.Manager.NavigateTo("/" + retrostep);
		}
		#endregion
	}
}