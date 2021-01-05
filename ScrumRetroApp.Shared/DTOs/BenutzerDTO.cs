using System.Runtime.Serialization;

namespace ScrumRetroApp.Shared.DTOs
{
	[DataContract]
	public class BenutzerDTO
	{
		#region Properties
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Vorname { get; set; }

		[DataMember]
		public string Mail { get; set; }

		[DataMember]
		public string Passwort { get; set; }

		[DataMember]
		public bool? Blockiert { get; set; }

		[DataMember]
		public bool? Admin { get; set; }
		#endregion
	}
}