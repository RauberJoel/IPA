using Microsoft.AspNetCore.Components;
using ScrumRetroApp.Blazor.Services;

namespace ScrumRetroApp.Blazor.Components
{
	public class RetroFooterComponentBase : ComponentBase
	{
		#region Properties
		[Inject]
		public ISession Session { get; set; }

		[Inject]
		private NavigationManager Manager { get; set; }
		#endregion
	}
}