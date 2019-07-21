using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGUApp.Presentation.Views.LanguagePicker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LanguagePickerList : ListView
	{
		public LanguagePickerList ()
		{
			InitializeComponent ();
		}
	}
}