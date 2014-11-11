using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Widget;

[assembly: ExportRenderer(typeof(XFormsPerformance.QuickLabel), typeof(XFormsPerformance.Android.CustomLabelRenderer))]

namespace XFormsPerformance.Android
{
    public class CustomLabelRenderer : ViewRenderer
    {
        public override SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            return new SizeRequest(new Size(100, 80));
        }

        TextView view;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (this.view == null) {
                this.view = new TextView(base.Context);
                view.Text = (Element as QuickLabel).Text;
                SetNativeControl(this.view);
            }
        }
    }
}

