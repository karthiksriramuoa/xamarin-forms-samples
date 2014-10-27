using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using FormsGallery.Models;
using FormsGallery.Repositories;

namespace FormsGallery
{
	public class AddExistingRecordPage : ContentPage
	{
		private RecordType recordType;
		private TableSection recordsSection;
		private Dictionary<AbstractRecord, TextCell> records;

		public AddExistingRecordPage (RecordType recordType)
		{
			this.recordType = recordType;
			this.records = new Dictionary<AbstractRecord, TextCell> ();
			foreach (var record in RecordRepository.Instance.GetRecords (recordType).OrderBy(r => r.ToString()))
			{
				this.records.Add (record, CreateCellFromRecord (record));
			}
		}

		private TextCell CreateCellFromRecord(AbstractRecord record)
		{
//			Command<Type> notesEditorPage = 
//				new Command<Type>(async (Type pageType) =>
//					{
//						NotesEditorPage page = (NotesEditorPage)Activator.CreateInstance(pageType, this.contact);
//						await this.Navigation.PushAsync(page);
//						this.notesCell.Detail = this.contact.Notes;
//					});
			var cell = new TextCell
			{
				Text = record.ToString()
			};

			return cell;
		}
	}
}

