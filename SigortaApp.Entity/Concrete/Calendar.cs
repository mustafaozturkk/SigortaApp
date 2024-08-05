using System;
namespace SigortaApp.Entity.Concrete
{
	public class Calendar : BaseEntity
	{
		public string title { get; set; }
		public DateTime start { get; set; }
		public DateTime end { get; set; }
		public string color { get; set; }
		public bool allDay { get; set; }
		public int CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}