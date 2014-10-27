using System;
using Xamarin.Forms;
using FormsGallery.Models;

namespace FormsGallery
{
	public class NotesEditorPage : ContentPage
	{
		private Editor editor;

		private Contact contact;

		public NotesEditorPage (Contact contact)
		{
			this.contact = contact;

			Label header = new Label
			{
				Font = Font.SystemFontOfSize(30, FontAttributes.Bold),
				HorizontalOptions = LayoutOptions.Center
			};

			this.editor = new Editor
			{
				Text = this.contact.Notes,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			// Build the page.
			this.Content = new StackLayout { Children = { header, editor } };

			this.editor.TextChanged += (object sender, TextChangedEventArgs e) =>
			{
				this.contact.Notes = this.editor.Text;
			};
		}
	}
}

