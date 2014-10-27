using Xamarin.Forms;
using MobileCRM.Shared.Pages;
using MobileCRM.Shared.ViewModels;
using MobileCRM.Models;

namespace MobileCRM.Shared.Pages
{
    public class MasterPage<T> : TabbedPage where T: class, IContact, new()
    {
        public MapPage<T> Map { get; private set; }
        public ListPage<T> List { get; private set; }

        public MasterPage(OptionItem menuItem)
        {
            var viewModel = new MapViewModel<T>();
            BindingContext = viewModel;

            this.SetValue(Page.TitleProperty, menuItem.Title);
            this.SetValue(Page.IconProperty, menuItem.Icon);

			List = new ListPage<T>(viewModel) { Icon = "list.png" };
			//we don't want to use the map page
            //Map = new MapPage<T>(viewModel);
            

            //this.Children.Add(Map);
            this.Children.Add(List);
        }
    }
}
