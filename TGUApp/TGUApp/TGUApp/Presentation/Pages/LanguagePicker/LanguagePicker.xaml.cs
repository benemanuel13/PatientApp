using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TGUApp.Models;
using TGUApp.Presentation.ViewModels;
using TGUApp.Presentation.Views.LanguagePicker;
using TGUApp.Presentation.EventArgs;

namespace TGUApp.Presentation.Pages.LanguagePicker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LanguagePicker : ContentPage
	{
        public event EventHandler<LanguageSelectedEventArgs> LanguageSelected;

        LanguagesViewModel viewModel;

        public LanguagePicker (LanguagesViewModel model)
		{
			InitializeComponent();

            viewModel = model;

            Languages.ItemsSource = viewModel.Languages;
            Languages.ItemTemplate = new DataTemplate(typeof(LanguagePickerListItemCell));

            Languages.ItemTapped += Languages_ItemTapped;
		}

        private void Languages_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Language language = (Language)e.Item;

            LanguageSelected(this, new LanguageSelectedEventArgs(language.LangCode));

            Navigation.PopAsync();
        }
    }
}