using System;
using Xamarin.Forms;
using FormsGallery.Models;

namespace FormsGallery
{
	public class ChooseRecordTypePage : ContentPage
	{
		public ChooseRecordTypePage (Contact contact)
		{
			Command<Type> contactCommand = 
				new Command<Type>(async (Type pageType) =>
					{
						AddExistingRecordPage page = (AddExistingRecordPage)Activator.CreateInstance(pageType, contact, RecordType.Contact);
						await this.Navigation.PushAsync(page);
						await this.Navigation.PopAsync();
					});

			var recordTypeSection = new TableSection ("Choose Record Type");
			var detailsView = new TableView
			{
				Intent = TableIntent.Menu,
				Root = new TableRoot("Choose Record Type") { recordTypeSection }
			};

			recordTypeSection.Add (new TextCell {
				Text = "Contact",
				Command = contactCommand
			});


			// Build the page.
			this.Content = new StackLayout
			{
				Children = { detailsView }
			};

		}
	}
}

