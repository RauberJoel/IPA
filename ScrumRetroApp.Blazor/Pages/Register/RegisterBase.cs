using Microsoft.AspNetCore.Components;
using ScrumRetroApp.Blazor.Services;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Blazor.Pages
{
	public class RegisterBase : ComponentBase
	{
		#region Properties
		[Inject]
		private ISession _session { get; set; }

		[Inject]
		private IBenutzerService _benutzerService { get; set; }

		[Inject]
		public NavigationManager Manager { get; set; }

		protected string strPrename;
		protected string strName;
		protected string strEmail;
		protected string strPassword;

		public async void Register()
		{
			if(strPrename == null || strName == null || strEmail == null || strPassword == null) return;

			BenutzerDTO dto = new BenutzerDTO()
			                  {
								  Vorname = strPrename,
								  Name = strName,
								  Mail = strEmail,
								  Passwort = strPassword
			};

			await this._benutzerService.CreateBenutzer(dto);

			this.Manager.NavigateTo("/");
		}

		public void Login()
		{
			this.Manager.NavigateTo("/login");
		}
		#endregion
	}
}