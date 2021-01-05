using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using ScrumRetroApp.Data.Models;
using ScrumRetroApp.Shared.DTOs;

namespace ScrumRetroApp.Data.Repositories
{
	public class BenutzerRepository : Repository<Benutzer>, IBenutzerRepository, IDisposable
	{
		#region Constructors
		public BenutzerRepository(DbContext context) : base(context)
		{
		}
		#endregion

		#region Publics
		public int CreateBenutzer(BenutzerDTO benutzerDto)
		{
			Benutzer benutzer = MapDTOToEntity(benutzerDto);

			Add(benutzer);

			return benutzer.Id;
		}

		public void EditBenutzer(BenutzerDTO dto)
		{
			Benutzer benutzer = FirstOrDefault(u => u.Id == dto.Id);

			if(benutzer == null) return;

			benutzer.Name = dto.Name;
			benutzer.Vorname = dto.Vorname;
			benutzer.Mail = dto.Mail;
			benutzer.Passwort = dto.Passwort;
			benutzer.Blockiert = dto.Blockiert;
			benutzer.Admin = dto.Admin;

			context.SaveChanges();
		}

		public void RemoveBenutzer(int nBenutzerId)
		{
			Benutzer benutzer = FirstOrDefault(u => u.Id == nBenutzerId);

			Remove(benutzer);
		}

		public bool Login(string strMail, string strPasswort)
		{
			Benutzer benutzer = FirstOrDefault(u => u.Mail == strMail);

			return benutzer.Passwort == strPasswort;
		}

		public void Dispose()
		{
		}
		#endregion

		#region Privates
		private Benutzer MapDTOToEntity(BenutzerDTO dto)
		{
			return new Benutzer
			{
				Id = dto.Id,
				Name = dto.Name,
				Vorname = dto.Vorname,
				Mail = dto.Mail,
				Passwort = dto.Passwort,
				Blockiert = dto.Blockiert,
				Admin = dto.Admin
			};
		}

		private BenutzerDTO MapEntityToDTO(Benutzer benutzer)
		{
			return new BenutzerDTO
			{
				       Id = benutzer.Id,
				       Name = benutzer.Name,
				       Vorname = benutzer.Vorname,
				       Mail = benutzer.Mail,
				       Passwort = benutzer.Passwort,
					   Blockiert = benutzer.Blockiert,
					   Admin = benutzer.Admin
			};
		}
		#endregion
	}
}