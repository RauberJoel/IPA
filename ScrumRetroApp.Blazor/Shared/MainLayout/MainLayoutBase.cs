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

		public EventCallback OnLoggedInEvent => EventCallback.Factory.Create(this, LoggedInEvent);

		[Inject]
		private NavigationManager Manager { get; set; }

		public void LoggedInEvent()
		{
			this.Header.Refresh();
		}
		#endregion
	}
}