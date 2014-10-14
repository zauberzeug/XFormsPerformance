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
            Content = new StackLayout();
            for (var i = 0; i < 40; i++)
                (Content as StackLayout).Children.Add(new Label{ Text = "Label " + i });
        }

        protected override void OnAppearing()
        {
            ((Content as StackLayout).Children[0] as Label).Text = "Stop after " + (DateTime.Now - App.StartTime).TotalMilliseconds + " ms";
        
            base.OnAppearing();
        }
    }
}

