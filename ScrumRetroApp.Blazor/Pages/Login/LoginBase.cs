using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using ScrumRetroApp.Blazor.Services;

namespace ScrumRetroApp.Blazor.Pages
{
	public class LoginBase : ComponentBase
	{
		#region Properties
		[Inject]
		private ISession _session { get; set; }

		[Inject]
		private IBenutzerService _benutzerService { get; set; }

		[Inject]
		public NavigationManager Manager { get; set; }

		protected string strEmail;
		protected string strPassword;

		public async void Login()
		{
			if(strEmail == null || strPassword == null) return;

			bool bResult = await this._benutzerService.Login(strEmail, strPassword);

			if(bResult != true) return;
			this._session.Email = strEmail;
			this._session.LoggedIn = true;

			this.Manager.NavigateTo("/dashboard");
		}

		public void Register()
		{
			this.Manager.NavigateTo("/register");
		}
		#endregion
	}
}