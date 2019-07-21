using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TGUApp.Presentation.EventArgs;
using TGUApp.Utility;

namespace TGUApp.Presentation.Views.Shared
{
    public enum BackButtonState
    {
        Hidden,
        Visible
    }

    public enum MenuButtonState
    {
        Hidden,
        Visible
    }

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NavBar : ContentView
	{
        private BackButtonState state = BackButtonState.Hidden;
        private MenuButtonState menuState = MenuButtonState.Visible;

        public event EventHandler<MenuTappedEventArgs> MenuTapped;

		public NavBar ()
		{
			InitializeComponent ();

            var backButtonGestureRecognizer = new TapGestureRecognizer();
            backButtonGestureRecognizer.Tapped += BackButtonGestureRecognizer_Tapped;

            BackButton.GestureRecognizers.Add(backButtonGestureRecognizer);

            var menuButtonGestureRecognizer = new TapGestureRecognizer();
            menuButtonGestureRecognizer.Tapped += MenuButtonGestureRecognizer_Tapped;

            MenuButton.GestureRecognizers.Add(menuButtonGestureRecognizer);
        }

        private void MenuButtonGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if (menuState == MenuButtonState.Visible)
                MenuTapped(this, new MenuTappedEventArgs());
        }

        private void BackButtonGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if(state == BackButtonState.Visible)
                Navigation.PopAsync();
        }

        public BackButtonState BackButtonState
        {
            get
            {
                return state;
            }

            set
            {
                state = value;

                if (state == BackButtonState.Visible)
                {
                    BackButton.IsVisible = true;
                    BackButton.Source = ImageSource.FromResource("TGUApp.Presentation.Images.BackButton.jpg");
                }
                else
                {
                    BackButton.IsVisible = false;
                    BackButton.Source = ImageSource.FromResource("TGUApp.Presentation.Images.BlankButton.jpg");
                }
            }
        }

        public MenuButtonState MenuButtonState
        {
            get
            {
                return menuState;
            }

            set
            {
                menuState = value;

                if (menuState == MenuButtonState.Visible)
                {
                    MenuButton.IsVisible = true;
                    MenuButton.Source = ImageSource.FromResource("TGUApp.Presentation.Images.Menu.jpg");
                }
                else
                {
                    MenuButton.IsVisible = false;
                    MenuButton.Source = ImageSource.FromResource("TGUApp.Presentation.Images.BlankButton.jpg");
                }
            }
        }
	}
}