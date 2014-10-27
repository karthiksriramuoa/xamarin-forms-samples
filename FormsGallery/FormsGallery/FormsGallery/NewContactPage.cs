using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using FormsGallery.Models;
using FormsGallery.Repositories;

namespace FormsGallery
{
	public class AddExistingRecordCommandParameters
	{
		public RecordType RecordType { get; set; }

		public AbstractRecord ParentRecord { get; set; }
	}

	class NewContactPage : ContentPage
    {
		private TableSection detailsSection;
		private TableView detailsView;
		private EntryCell companyNameCell;
		private EntryCell titleCell;
		private EntryCell firstNameCell;
		private EntryCell lastNameCell;
		private EntryCell jobTitleRoleCell;
		private EntryCell sourceCell;
		private SwitchCell companyCell;
		private TextCell notesCell;
		private Contact contact;
		private AbstractRecord parentRecord;

		private TableSection linkedRecordsSection;
		//private List<TextCell> linkedContactCells;
		private TextCell addNewLinkCell;
		private TextCell addExistingLinkCell;

		/// <summary>
		/// Initializes a new instance of the <see cref="FormsGallery.NewContactPage"/> class.
		/// </summary>
		/// <param name="parentRecord">Allows a parent record to be set. Use null if there is no parent.</param>
        public NewContactPage(AbstractRecord parentRecord)
        {
			this.parentRecord = parentRecord;
			this.contact = new Contact { Notes = "" };

			Command<Type> notesEditorCommand = 
				new Command<Type>(async (Type pageType) =>
					{
						NotesEditorPage page = (NotesEditorPage)Activator.CreateInstance(pageType, this.contact);
						await this.Navigation.PushAsync(page);
						this.notesCell.Detail = this.contact.Notes;
					});

			// Contact (General Information)
			Label contactHeader = new Label
            {
                Text = "Contact",
                Font = Font.SystemFontOfSize(30, FontAttributes.Bold),
                HorizontalOptions = LayoutOptions.Center
            };

			this.companyNameCell = new EntryCell 	{ Label = "Company Name" };
			this.titleCell = new EntryCell 			{ Label = "Title" };
			this.firstNameCell = new EntryCell 		{ Label = "First Name" };
			this.lastNameCell = new EntryCell 		{ Label = "Last Name" };
			this.jobTitleRoleCell = new EntryCell 	{ Label = "Job Title / Role" };
			this.sourceCell = new EntryCell 		{ Label = "Source" };
			this.companyCell = new SwitchCell		{ Text = "Company" };
			this.notesCell = new TextCell
			{
				Text = "Notes",
				Detail = contact.Notes,
				Command = notesEditorCommand,
				CommandParameter = typeof(NotesEditorPage)
			};

			this.detailsSection = new TableSection ("Contact");
            this.detailsView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot("Details") { detailsSection }
            };

			companyCell.OnChanged += (object sender, ToggledEventArgs e) => { ChangeCompanyMode(this.companyCell.On); };

			// Linked Records
			Command addNewRecordCommand = 
				new Command(async () =>
					{
						string action = await DisplayActionSheet(null, "Cancel", null, "Contact", "Shoot", "Quote", "Sale");
						if (action == "Contact")
						{
							var page = new NewContactPage(this.contact);
							await this.Navigation.PushAsync(page);
						}
					});
			Command addExistingRecordCommand = 
				new Command(async () =>
					{
						string action = await DisplayActionSheet(null, "Cancel", null, "Contact", "Shoot", "Quote", "Sale");
						if (action == "Contact")
						{
							var page = new AddExistingRecordPage(RecordType.Contact, this.contact);
							await this.Navigation.PushAsync(page);
						}
					});
			this.linkedRecordsSection = new TableSection("Linked Records");
			this.addNewLinkCell = new TextCell { Text = "Add New", Command = addNewRecordCommand };
			this.addExistingLinkCell = new TextCell{ Text = "Add Existing", Command = addExistingRecordCommand };

			PopulateLinkedRecordsSection ();
			this.detailsView.Root.Add (this.linkedRecordsSection);

            // Build the page.
            this.Content = new StackLayout
            {
                Children = { contactHeader, detailsView }
            };

			ChangeCompanyMode(this.companyCell.On);
        }

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			// Refresh the notes cell
			if (this.contact != null)
			{
				this.notesCell.Detail = this.contact.Notes ?? "";
			}

			// Refresh the linked records
			this.linkedRecordsSection.Clear ();
			PopulateLinkedRecordsSection ();
			detailsView.Root [1] = this.linkedRecordsSection;
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();

			// Save the current record
			this.contact.IsCompany = this.companyCell.On;
			if (this.contact.IsCompany)
			{
				this.contact.CompanyName = this.companyNameCell.Text;
			}
			else
			{
				this.contact.Title = this.titleCell.Text;
				this.contact.FirstName = this.firstNameCell.Text;
				this.contact.LastName = this.lastNameCell.Text;
				this.contact.JobTitle = this.jobTitleRoleCell.Text;
			}

			this.contact.Source = this.sourceCell.Text;

			RecordRepository.Instance.Add (this.contact);

			// Add to parent, if present
			if (this.parentRecord != null)
				this.parentRecord.LinkedRecords.Add (this.contact);
		}

		private void ChangeCompanyMode(bool isCompanyMode)
		{
			detailsSection.Clear ();
			if (isCompanyMode)
			{
				detailsSection.Add (companyNameCell);
			}
			else
			{
				detailsSection.Add (titleCell);
				detailsSection.Add (firstNameCell);
				detailsSection.Add (lastNameCell);
				detailsSection.Add (jobTitleRoleCell);
			}

			detailsSection.Add (sourceCell);
			detailsSection.Add (companyCell);
			detailsSection.Add (notesCell);

			detailsView.Root[0] = detailsSection;
		}

		private void PopulateLinkedRecordsSection()
		{
			foreach (var record in this.contact.LinkedRecords.OrderBy(r => r.ToString()))
			{
				var cell = new TextCell { Text = record.ToString() };
				this.linkedRecordsSection.Add(cell);
			}

			this.linkedRecordsSection.Add (this.addNewLinkCell);
			this.linkedRecordsSection.Add (this.addExistingLinkCell);
		}
    }
}
