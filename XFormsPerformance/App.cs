using System;
using Xamarin.Forms;

namespace XFormsPerformance
{
    public static class App
    {
        public static DateTime StartTime;

        public static Page GetMainPage()
        {    
            return new NavigationPage(new StartPage());
        }
    }

    public class StartPage : ContentPage
    {
        public StartPage()
        {
            Content = new Button {
                Text = "Start",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Command = new Command(o => {
                    App.StartTime = DateTime.Now;
                    Navigation.PushAsync(new StopPage());
                }),
            };
        }
    }

    public class StopPage : ContentPage
    {
        public StopPage()
        {
            Content = new Label {
                Text = "Stop",
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            (Content as Label).Text += " after " + (DateTime.Now - App.StartTime).TotalMilliseconds + " ms";
        }
    }
}

