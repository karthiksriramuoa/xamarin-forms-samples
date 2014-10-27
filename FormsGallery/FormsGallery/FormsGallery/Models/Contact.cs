using System;
using System.Collections.Generic;
using FormsGallery.Models;

namespace FormsGallery.Models
{
	public class Contact : AbstractRecord
	{
		public string CompanyName { get; set; }

		public string Title { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string JobTitle { get; set; }

		public string Source { get; set; }
		public bool IsCompany { get; set; }

		public Contact () : base (RecordType.Contact)
		{

		}

		public override string ToString ()
		{
			if (this.IsCompany)
				return this.CompanyName;
			else
				return string.Format ("{0}, {1}", this.LastName, this.FirstName);
		}
	}
}

