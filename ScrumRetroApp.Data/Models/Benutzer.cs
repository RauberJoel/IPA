namespace ScrumRetroApp.Data.Models
{
	public class Benutzer
	{
		#region Properties
		public int Id { get; set; }
		public string Name { get; set; }
		public string Vorname { get; set; }
		public string Mail { get; set; }
		public string Passwort { get; set; }
		public bool? Blockiert { get; set; }
		public bool? Admin { get; set; }
		#endregion
	}
}