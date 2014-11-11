using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Widget;

[assembly: ExportRenderer(typeof(XFormsPerformance.QuickerLabel), typeof(XFormsPerformance.Android.QuickerLabelRenderer))]

namespace XFormsPerformance.Android
{
    public class QuickerLabelRenderer : ViewRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
           
            var view = new TextView(base.Context);
            view.Text = (Element as QuickerLabel).Text;
            //var view = new global::Android.Views.View(Context);
            //view.SetBackgroundColor(Color.FromHex("#222222").ToAndroid());
            SetNativeControl(view);
        }
    }
}

