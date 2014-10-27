using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using FormsGallery.Models;
using FormsGallery.Repositories;

namespace FormsGallery
{
	/// <summary>
	/// Allows an existing record to be linked to another record
	/// </summary>
	public class AddExistingRecordPage : ContentPage
	{
		private AbstractRecord parentRecord;
		private RecordType recordType;

		/// <summary>
		/// Initializes a new instance of the <see cref="FormsGallery.AddExistingRecordPage"/> class.
		/// </summary>
		/// <param name="recordType">The types of record to be listed</param>
		/// <param name="parentRecord">The parent record</param>
		public AddExistingRecordPage (RecordType recordType, AbstractRecord parentRecord)
		{
			this.parentRecord = parentRecord;
			this.recordType = recordType;

			var tableSection = new TableSection (this.recordType.ToString ()); // TODO: Proper title

			// Get records that aren't already linked to the parent record
			var selectableRecords = 
				RecordRepository
					.Instance
					.GetRecords (this.recordType)
					.Except (this.parentRecord.LinkedRecords)
					.OrderBy (r => r.ToString ());

			foreach (var record in selectableRecords)
			{
				TextCell cell = CreateCellFromRecord (record);
				tableSection.Add(cell);
			}

			var tableView = new TableView
			{
				Intent = TableIntent.Menu,
				Root = new TableRoot (this.recordType.ToString ()) { tableSection } // TODO: Proper title
			};

			this.Content = new StackLayout{ Children = {tableView} };
		}

		/// <summary>
		/// Creates a TextCell from a record and attaches a Command to select the item
		/// </summary>
		private TextCell CreateCellFromRecord(AbstractRecord record)
		{
			Command selectRecordCommand = 
				new Command( async() =>
					{
						this.parentRecord.LinkedRecords.Add(record);
						await this.Navigation.PopAsync();
					});

			var cell = new TextCell
			{
				Text = record.ToString(),
				Command = selectRecordCommand
			};

			return cell;
		}
	}
}

