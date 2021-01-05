using System;
using Microsoft.EntityFrameworkCore;

namespace ScrumRetroApp.Data.Models
{
	public class IPAContext : DbContext
	{
		#region Properties
		public virtual DbSet<Benutzer> Benutzer { get; set; }
		#endregion

		#region Constructors
		public IPAContext()
		{
		}

		public IPAContext(DbContextOptions<IPAContext> options)
			: base(options)
		{
		}

		public IPAContext(string connectionString) : base(GetOptions(connectionString))
		{
		}
		#endregion

		#region Protecteds
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if(!optionsBuilder.IsConfigured)
			{
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
				optionsBuilder.UseSqlServer("Server=localhost;Database=IPA;Trusted_connection=yes");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Benutzer>(entity =>
			                              {
				                              entity.Property(e => e.Mail)
				                                    .IsRequired()
				                                    .HasMaxLength(75)
				                                    .IsUnicode(false);

				                              entity.Property(e => e.Name)
				                                    .IsRequired()
				                                    .HasMaxLength(50)
				                                    .IsUnicode(false);

				                              entity.Property(e => e.Passwort)
				                                    .IsRequired()
				                                    .HasMaxLength(50)
				                                    .IsUnicode(false);

				                              entity.Property(e => e.Vorname)
				                                    .IsRequired()
				                                    .HasMaxLength(50)
				                                    .IsUnicode(false);
			                              });

			OnModelCreatingPartial(modelBuilder);
		}
		#endregion

		#region Privates
		private static DbContextOptions GetOptions(string connectionString)
		{
			return new DbContextOptionsBuilder().UseSqlServer(connectionString).Options;
		}

		private void OnModelCreatingPartial(ModelBuilder modelBuilder)
		{
			
		}
		#endregion
	}
}