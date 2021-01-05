using Microsoft.AspNetCore.Components;
using ScrumRetroApp.Blazor.Components;
using ScrumRetroApp.Blazor.Services;

namespace ScrumRetroApp.Blazor.Shared
{
	public class MainLayoutBase : LayoutComponentBase
	{
		#region Properties
		[Inject]
		public ISession Session { get; set; }

		protected RetroFooterComponentBase RetroFooter { get; set; }

		protected HeaderComponent Header { get; set; }

		[Inject]
		private NavigationManager Manager { get; set; }
		#endregion
	}
}