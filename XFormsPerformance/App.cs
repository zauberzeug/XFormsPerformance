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

    public class QuickerLabel : View
    {
        protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
        {
            return new SizeRequest(new Size(100, 20));
        }

        public string Text = "";
    }

    public class StartPage : ContentPage
    {
        public StartPage()
        {
            Content = new StackLayout {
                Children = {
                    new Button {
                        Text = "Start Label Instanciation",
                        Command = new Command(o => {
                            App.StartTime = DateTime.Now;
                            Navigation.PushAsync(new StopPage(i => new Label{ Text = "Label " + i }));
                        }),
                    },
                    new Button {
                        Text = "Start QuickerLabel Instanciation",
                        Command = new Command(o => {
                            App.StartTime = DateTime.Now;
                            Navigation.PushAsync(new StopPage(i => new QuickerLabel{ Text = "Label " + i }));
                        }),
                    },
                }
            };
        }
    }

    public class StopPage : ContentPage
    {
        public StopPage(Func<int, View> createLabel)
        {
            Content = new StackLayout();
            for (var i = 0; i < 40; i++)
                (Content as StackLayout).Children.Add(createLabel(i));
        }

        protected override void OnAppearing()
        {
            var timingMessage = "Stop after " + (DateTime.Now - App.StartTime).TotalMilliseconds + " ms";
            Console.WriteLine(timingMessage);
            base.OnAppearing();
        }
    }
}

