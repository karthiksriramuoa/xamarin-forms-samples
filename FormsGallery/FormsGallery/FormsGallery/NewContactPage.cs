using System;
using System.Collections.Generic;
using Xamarin.Forms;
using FormsGallery.Models;
using FormsGallery.Repositories;

namespace FormsGallery
{
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

		private TableSection linkedRecordsSection;
		private List<TextCell> linkedContactCells;
		private TextCell addNewLinkCell;
		private TextCell addExistingLinkCell;

        public NewContactPage()
        {
			this.contact = new Contact { Notes = "" };

			Command<Type> notesEditorCommand = 
				new Command<Type>(async (Type pageType) =>
					{
						NotesEditorPage page = (NotesEditorPage)Activator.CreateInstance(pageType, this.contact);
						await this.Navigation.PushAsync(page);
						this.notesCell.Detail = this.contact.Notes;
					});

			Command<Type> addExistingRecordCommand = 
				new Command<Type>(async (Type pageType) =>
					{
						var action = await DisplayActionSheet(null, "Cancel", null, "Contact", "Shoot", "Quote", "Sale");
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
			this.linkedContactCells = GetLinkedContacts();
			this.addNewLinkCell = new TextCell { Text = "Add New" };
			this.addExistingLinkCell = new TextCell{ Text = "Add Existing", Command = addExistingRecordCommand };
			this.linkedRecordsSection = new TableSection("Linked Records");

			this.linkedRecordsSection.Add (this.linkedContactCells);
			this.linkedRecordsSection.Add (this.addNewLinkCell);
			this.linkedRecordsSection.Add (this.addExistingLinkCell);

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

		private List<TextCell> GetLinkedContacts()
		{
			var contacts = new List<TextCell> ();

			// TODO: Build contact cells

			return contacts;
		}
    }
}
