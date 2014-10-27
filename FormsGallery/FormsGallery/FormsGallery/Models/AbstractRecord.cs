using System;
using System.Collections.Generic;

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
		public List<AbstractRecord> LinkedRecords
		{
			get
			{
				if (this.linkedRecords == null)
					this.linkedRecords = new List<AbstractRecord> ();

				return this.linkedRecords;
			}
			set { this.linkedRecords = value; }
		}
		private List<AbstractRecord> linkedRecords;

		public AbstractRecord (RecordType recordType)
		{
			this.RecordType = recordType;
		}
	}
}

