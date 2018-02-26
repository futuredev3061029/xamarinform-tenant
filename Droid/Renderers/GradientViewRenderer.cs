﻿using System.Linq;
using Android.Graphics;
using Android.Graphics.Drawables;

using Android.Widget;
using MyTenantWorld;
using MyTenantWorld.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(GradientViewRender), typeof(GradientViewRenderer))]
namespace MyTenantWorld.Droid
{
	public class GradientViewRenderer : ViewRenderer<GradientViewRender, Android.Views.View>
	{
		LinearLayout layout;
		Xamarin.Forms.Color[] gradientColors;
		float cornerRadius;
		double viewHeight;
		double viewWidth;
		bool roundCorners;

		protected override void OnElementChanged(ElementChangedEventArgs<GradientViewRender> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
				layout = new LinearLayout(this.Context);
                layout.SetBackgroundColor(Android.Graphics.Color.White);

				gradientColors = (Xamarin.Forms.Color[])e.NewElement.GradientColors;
				cornerRadius = (float)e.NewElement.CornerRadius;
				viewWidth = (double)e.NewElement.ViewWidth;
				viewHeight = (double)e.NewElement.ViewHeight;
				roundCorners = (bool)e.NewElement.RoundCorners;

				CreateLayout();
			}

			if (e.OldElement != null)
			{
				// Unsubscribe from event handlers and cleanup any resources
			}

			if (e.NewElement != null)
			{
				// Configure the control and subscribe to event handlers
				gradientColors = (Xamarin.Forms.Color[])e.NewElement.GradientColors;
				cornerRadius = (float)e.NewElement.CornerRadius;
				viewWidth = (double)e.NewElement.ViewWidth;
				viewHeight = (double)e.NewElement.ViewHeight;
				roundCorners = (bool)e.NewElement.RoundCorners;

				CreateLayout();
			}
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == GradientViewRender.ViewHeightProperty.PropertyName)
			{
				this.viewHeight = (double)this.Element.ViewHeight;
				CreateLayout();
			}
			else if (e.PropertyName == GradientViewRender.ViewWidthProperty.PropertyName)
			{
				this.viewWidth = (double)this.Element.ViewWidth;
				CreateLayout();
			}
			else if (e.PropertyName == GradientViewRender.GradientColorsProperty.PropertyName)
			{
				this.gradientColors = (Xamarin.Forms.Color[])this.Element.GradientColors;
				CreateLayout();
			}
			else if (e.PropertyName == GradientViewRender.CornerRadiusProperty.PropertyName)
			{
				this.cornerRadius = (float)this.Element.CornerRadius;
				CreateLayout();
			}
			else if (e.PropertyName == GradientViewRender.RoundCornersProperty.PropertyName)
			{
				this.roundCorners = (bool)this.Element.RoundCorners;
				CreateLayout();
			}
		}

		private void CreateLayout()
		{
			layout.SetMinimumWidth((int)viewWidth);
			layout.SetMinimumHeight((int)viewHeight);

			CreateGradient();

			SetNativeControl(layout);
		}

		public void CreateGradient()
		{
			//Need to convert the colors to Android Color objects
			int[] androidColors = new int[gradientColors.Count()];

			for (int i = 0; i < gradientColors.Count(); i++)
			{
				Xamarin.Forms.Color temp = gradientColors[i];
				androidColors[i] = temp.ToAndroid();
			}

			GradientDrawable gradient = new GradientDrawable(GradientDrawable.Orientation.LeftRight, androidColors);

			if (roundCorners)
				gradient.SetCornerRadii(new float[] { cornerRadius, cornerRadius, cornerRadius, cornerRadius, cornerRadius, cornerRadius, cornerRadius, cornerRadius });

			layout.SetBackground(gradient);
		}
	}
}
