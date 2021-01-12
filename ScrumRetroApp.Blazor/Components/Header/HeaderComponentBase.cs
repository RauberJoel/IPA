using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ScrumRetroApp.Blazor.Services;

namespace ScrumRetroApp.Blazor.Components
{
	public class HeaderComponentBase : ComponentBase
	{
		#region Properties
		[Inject]
		public ISession _session { get; set; }

		[Inject]
		public NavigationManager Manager { get; set; }

		[Inject]
		public ITFSService _tfsService { get; set; }

		protected string imagesrc;

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();

			if(this._session.Email != null)
			{
				imagesrc = await this._tfsService.GetProfilePicture(this._session.Email);
			}
		}

		public void Refresh()
		{
			StateHasChanged();
		}
		#endregion
	}
}