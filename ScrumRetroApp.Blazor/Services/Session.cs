using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR.Client;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Blazor.Services
{
	public class Session : ISession
	{
		#region Properties
		public int BenutzerId { get; set; }
		#endregion
	}
}