using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ScrumRetroApp.Blazor.Services;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Blazor.Components
{
	public class NewRetroComponentBase : ComponentBase
	{
		#region Fields
		protected List<EmojiDTO> dtosEmoji;
		protected List<string> lstSprints;
		protected List<RetroColumnDTO> dtosRetroColumn;
		protected string EmojiOptions;

		protected bool bAnonymity;
		protected string strSelectedSprint;
		protected int nMaxVotes;
		#endregion

		#region Properties
		[Parameter]
		public Action OnCancel { get; set; }

		[Parameter]
		public EventCallback<RetroDTO> OnRetroCreatedCallback { get; set; }

		[Inject] private ISessionService _sessionService { get; set; }

		[Inject]
		public ISession Session { get; set; }

		[Inject]
		public NavigationManager Manager { get; set; }

		[Inject]
		private IRetroService _retroService { get; set; }
		#endregion

		#region Publics
		public void RemoveColumn(RetroColumnDTO dto)
		{
			dtosRetroColumn.Remove(dto);
		}

		public void AddColumn()
		{
			RetroColumnDTO dtoDefault = new RetroColumnDTO
			                            {
				                            Id = 1,
				                            EmojiId = 1,
				                            Name = "Type here",
				                            Color = "#ff9900"
			                            };

			dtosRetroColumn.Add(dtoDefault);
		}

		public void Cancel()
		{
			this.OnCancel();
		}

		public async void Create()
		{
			if(strSelectedSprint == null) strSelectedSprint = await this._sessionService.GetCurrentSprint();
			if(nMaxVotes <= 0) nMaxVotes = 5;

			RetroConfigurationDTO dtoRetroConfiguration = new RetroConfigurationDTO
			                                              {
				                                              Anonymity = bAnonymity,
				                                              MaxVotes = nMaxVotes
			                                              };

			RetroDTO dtoRetro = await this._retroService.CreateRetro(this.Session.TeamId,
			                                                         this.Session.UserId,
			                                                         dtoRetroConfiguration,
			                                                         dtosRetroColumn,
			                                                         strSelectedSprint);

			if(dtoRetro == null) return;

			this.Session.RetroId = dtoRetro.Id;
			this.Session.IsScrumMaster = true;
			this.Session.RetroStep = dtoRetro.Step;

			ChangeStep(this.Session.RetroStep);

			await this.OnRetroCreatedCallback.InvokeAsync(dtoRetro);
		}
		#endregion

		#region Protecteds
		protected override Task OnInitializedAsync()
		{
			// Einen Wait eigebaut, da diese Daten schon am Anfang notwendig sind
			Task.Run(SetNecessaryData).Wait();

			PrepareEmojiOptions();

			return base.OnInitializedAsync();
		}

		protected void PrepareEmojiOptions()
		{
			StringBuilder builder = new StringBuilder();
			foreach(EmojiDTO dto in dtosEmoji) builder.Append($"<option value='{dto.Id}'>{dto.Unicode}</option>");

			EmojiOptions = builder.ToString();
		}
		#endregion

		#region Privates
		private void SetNecessaryData()
		{
			lstSprints = this._retroService.GetAllSprints().Result;

			dtosEmoji = this._retroService.GetEmojis().Result;

			CreateRetroColumnTemplates();
		}

		private void CreateRetroColumnTemplates()
		{
			RetroColumnDTO dtoStart = new RetroColumnDTO
			                          {
				                          EmojiId = 1,
				                          Name = "Start",
				                          Color = "#ff9900"
			                          };

			RetroColumnDTO dtoStop = new RetroColumnDTO
			                         {
				                         EmojiId = 2,
				                         Name = "Stop",
				                         Color = "#ff0000"
			                         };

			RetroColumnDTO dtoContinue = new RetroColumnDTO
			                             {
				                             EmojiId = 1,
				                             Name = "Continue",
				                             Color = "#33cc33"
			                             };

			dtosRetroColumn = new List<RetroColumnDTO>
			                  {
				                  dtoStart, dtoContinue, dtoStop
			                  };
		}

		private void ChangeStep(string retrostep)
		{
			this.Session.RetroStep = retrostep;
			this.Manager.NavigateTo("/" + retrostep);
		}
		#endregion
	}
}