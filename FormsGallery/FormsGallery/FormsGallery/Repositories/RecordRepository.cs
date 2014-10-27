using System;
using System.Collections.Generic;
using System.Linq;
using FormsGallery.Models;
using FormsGallery.Repositories;

namespace FormsGallery.Repositories
{
	public class RecordRepository
	{
		// Singleton accessor
		private static RecordRepository instance;
		public static RecordRepository Instance
		{
			get
			{
				if (instance == null)
					instance = new RecordRepository();

				return instance;
			}
		}

		private List<AbstractRecord> records;

		private RecordRepository ()
		{
			// Create some mock records
			this.records = new List<AbstractRecord> ();

			this.records.Add (new Contact{
				RecordId = 1,
				IsCompany = false,
				FirstName = "Charles",
				LastName = "Hills",
				JobTitle = "",
				Source = "",
				Notes = "Runs an architectural journal"
			});

			this.records.Add (new Contact {
				RecordId = 2,
				IsCompany = true,
				CompanyName = "Perfection in Portraits Ltd.",
				Source = "Referral from friends",
				Notes = ""
			});
		}

		public List<AbstractRecord> GetRecords(RecordType recordType)
		{
			return this.records.Where (r => r.RecordType == recordType).ToList ();
		}

		public int Add(AbstractRecord record)
		{
			int id = this.records.Select(r => r.RecordId).OrderByDescending(r => r).FirstOrDefault() + 1;
			record.RecordId = id;
			this.records.Add (record);

			return id;
		}
	}
}

