using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MobileCRM.Models
{

	public class EventsOptionItem : OptionItem
	{
	}

    public class EnquiriesOptionItem : OptionItem
    {
        public override string Title { get { return "Enquiries"; } }
        public override string Icon { get { return "enquiry.png"; } }
    }

	public class ShootsOptionItem : OptionItem
	{
	}

	public class TasksOptionItem : OptionItem
	{
	}

    public class ContactsOptionItem : OptionItem
    {
    }

    public class PurchasesOptionItem : OptionItem
    {
    }

    public  abstract class OptionItem
    {
        public virtual string Title { get { var n = GetType().Name; return n.Substring(0, n.Length - 10); } }
        public virtual int Count { get; set; }
        public virtual bool Selected { get; set; }
        public virtual string Icon { get { return 
                Title.ToLower().TrimEnd('s') + ".png" ; } }
        public ImageSource IconSource { get { return ImageSource.FromFile(Icon); } }
    }
}

