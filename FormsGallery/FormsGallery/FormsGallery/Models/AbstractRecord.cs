using System;

namespace FormsGallery.Models
{
	public enum  RecordType
	{
		Contact, Shoot, Quote, Sale
	}
		
	public abstract class AbstractRecord
	{
		public RecordType RecordType { get; set; }
		public int RecordId { get; set; }
		public string Notes { get; set; }

		public AbstractRecord (RecordType recordType)
		{
			this.RecordType = recordType;
		}
	}
}

