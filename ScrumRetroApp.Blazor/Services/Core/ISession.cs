using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR.Client;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Blazor.Services
{
	public interface ISession
	{
		int BenutzerId { get; set; }

		string Email { get; set; }

		bool? Admin { get; set; }

		bool LoggedIn { get; set; }
	}
}