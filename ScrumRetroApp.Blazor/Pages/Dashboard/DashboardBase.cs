using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ScrumRetroApp.Blazor.Components;
using ScrumRetroApp.Blazor.Services;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Blazor.Pages
{
	public class DashboardBase : ComponentBase
	{
		#region Fields
		public string Title;
		public List<RetroDTO> DtosActiveRetro = new List<RetroDTO>();
		#endregion

		#region Properties
		protected CurrentParticipantsComponentBase CurrentParticipant { get; set; }

		[CascadingParameter]
		protected EventCallback OnUserJoinedEvent { get; set; }

		[Inject]
		private ISession _session { get; set; }

		[Inject]
		private IRetroService _retroService { get; set; }
		#endregion

		#region Publics
		public void OnUserJoined()
		{
			this.OnUserJoinedEvent.InvokeAsync(true);
		}
		#endregion

		#region Protecteds
		protected override void OnParametersSet()
		{
			Title = this._session.DTODashboard.Title;
			base.OnParametersSet();
		}

		protected override Task OnInitializedAsync()
		{
			Task.Run(ActiveRetros).Wait();

			return base.OnInitializedAsync();
		}

		protected void Join(int nRetroId)
		{
			this._session.RetroId = nRetroId;
			this.CurrentParticipant.CreateActiveUser();
		}
		#endregion

		#region Privates
		private void ActiveRetros()
		{
			DtosActiveRetro = this._retroService.GetActiveRetrosByTeamId(this._session.TeamId).Result;
		}
		#endregion
	}
}