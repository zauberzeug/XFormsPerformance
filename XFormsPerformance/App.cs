using System;
using Xamarin.Forms;

namespace XFormsPerformance
{
    public class App: Application
    {
        public static DateTime StartTime;

        public App()
        {    
            MainPage = new NavigationPage(new StartPage());
        }
    }

    public class QuickerLabel : View
    {
        public string Text = "";

        protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
        {
            return new SizeRequest(new Size(100, 20));
        }
    }

    public class StartPage : ContentPage
    {
        public StartPage()
        {
            Content = new StackLayout {
                HorizontalOptions = LayoutOptions.Start,
                Children = {
                    new Button {
                        Text = "Instantiate Labels in StackLayout",
                        Command = new Command(o => {
                            App.StartTime = DateTime.Now;
                            Navigation.PushAsync(new StopPage<StackLayout>(i => new Label{ Text = "Label " + i }));
                        }),
                    },
                    new Button {
                        Text = "Instantiate QuickerLabels in StackLayout",
                        Command = new Command(o => {
                            App.StartTime = DateTime.Now;
                            Navigation.PushAsync(new StopPage<StackLayout>(i => new QuickerLabel{ Text = "Label " + i }));
                        }),
                    },
                    new Button {
                        Text = "Instantiate Labels in QuickStackLayout",
                        Command = new Command(o => {
                            App.StartTime = DateTime.Now;
                            Navigation.PushAsync(new StopPage<QuickStackLayout>(i => new Label{ Text = "Label " + i }));
                        }),
                    },
                    new Button {
                        Text = "Instantiate QuickerLabels in QuickStackLayout",
                        Command = new Command(o => {
                            App.StartTime = DateTime.Now;
                            Navigation.PushAsync(new StopPage<QuickStackLayout>(i => new QuickerLabel{ Text = "Label " + i }));
                        }),
                    },
                }
            };
        }
    }

    public class QuickStackLayout : StackLayout
    {
        protected override bool ShouldInvalidateOnChildAdded(View child)
        {
            return false;
        }

        protected override bool ShouldInvalidateOnChildRemoved(View child)
        {
            return false;
        }

        protected override void OnChildMeasureInvalidated()
        {
        }
    }

    public class StopPage<T> : ContentPage where T : StackLayout, new()
    {
        public StopPage(Func<int, View> createLabel)
        {
            Content = new T();
            for (var i = 0; i < 100; i++)
                (Content as T).Children.Add(createLabel(i));
        }

        protected override void OnAppearing()
        {
            Console.WriteLine("Stop after " + (DateTime.Now - App.StartTime).TotalMilliseconds + " ms");
            base.OnAppearing();
        }
    }
}

