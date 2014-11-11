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

    public class QuickLabel : View
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
                            Navigation.PushAsync(new LabelStopPage());
                        }),
                    },
                    new Button {
                        Text = "Start QuickLabel Instanciation",
                        Command = new Command(o => {
                            App.StartTime = DateTime.Now;
                            Navigation.PushAsync(new QuickLabelStopPage());
                        }),
                    },
                }
            };
        }
    }

    public class LabelStopPage : ContentPage
    {
        public LabelStopPage()
        {
            Content = new StackLayout();
            for (var i = 0; i < 400; i++)
                (Content as StackLayout).Children.Add(new Label{ Text = "Label " + i });
        }

        protected override void OnAppearing()
        {
            var timingMessage = "Stop after " + (DateTime.Now - App.StartTime).TotalMilliseconds + " ms";
            ((Content as StackLayout).Children [0] as Label).Text = timingMessage;
            Console.WriteLine(timingMessage);
            base.OnAppearing();
        }
    }

    public class QuickLabelStopPage : ContentPage
    {
        public QuickLabelStopPage()
        {
            Content = new StackLayout();
            for (var i = 0; i < 400; i++)
                (Content as StackLayout).Children.Add(new QuickLabel{ Text = "Label " + i });
        }

        protected override void OnAppearing()
        {
            var timingMessage = "Stop after " + (DateTime.Now - App.StartTime).TotalMilliseconds + " ms";
            Console.WriteLine(timingMessage);
            base.OnAppearing();
        }
    }
}

