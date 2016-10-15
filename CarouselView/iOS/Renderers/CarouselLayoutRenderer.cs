using System;
using Xamarin.Forms;
using CustomLayouts.Controls;
using CustomLayouts.iOS.Renderers;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.ComponentModel;
using System.Drawing;
using System.Timers;

[assembly: ExportRenderer(typeof(CarouselLayout), typeof(CarouselLayoutRenderer))]

namespace CustomLayouts.iOS.Renderers
{
	public class CarouselLayoutRenderer : ScrollViewRenderer
	{
		UIScrollView _native;
		double _lastPos = 0.5;
		//DateTime _lastScrollTime = DateTime.Now;

		public CarouselLayoutRenderer()
		{
			PagingEnabled = true;
			ShowsHorizontalScrollIndicator = false;

			// Haiyun
			//Timer aTimer = new Timer();
			//aTimer.Elapsed += OnTimedEvent;
			//aTimer.Interval = 5000;
			//aTimer.Enabled = true;
		}

		//private void OnTimedEvent(object source, ElapsedEventArgs e)
		//{
		//	// Haiyun
		//	CarouselLayout carousel = (CarouselLayout)Element;
		//	int maxIndex = carousel.ItemsSource.Count - 1;
		//	int index = carousel.SelectedIndex + 1;
		//	if (index > maxIndex)
		//	{
		//		index = 0;
		//	}

		//	carousel.SelectedIndex = index;
		//}

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null) return;

			_native = (UIScrollView)NativeView;
			_native.Scrolled += NativeScrolled;
			e.NewElement.PropertyChanged += ElementPropertyChanged;
		}

		void NativeScrolled(object sender, EventArgs e)
		{
			//var center = _native.ContentOffset.X + (_native.Bounds.Width / 2);
			//((CarouselLayout)Element).SelectedIndex = ((int)center) / ((int)_native.Bounds.Width);

			// Haiyun
			CarouselLayout carousel = (CarouselLayout)Element;
			int minIndex = 0;
			double minPos = 0.5;
			int maxIndex = carousel.ItemsSource.Count - 1;
			double maxPos = maxIndex + 0.5;

			var center = _native.ContentOffset.X + (_native.Bounds.Width / 2.0);
			var pos = center / _native.Bounds.Width;
			int index;
			if (Math.Abs(pos - minPos) < 0.000001)
			{
				index = _lastPos < minPos ? maxIndex : minIndex;
			}
			else if (Math.Abs(pos - maxPos) < 0.000001)
			{
				index = _lastPos > maxPos ? minIndex : maxIndex;
			}
			else
			{
				index = (int)pos;
			}
			carousel.SelectedIndex = index;

			_lastPos = pos;
		}

		void ElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == CarouselLayout.SelectedIndexProperty.PropertyName && !Dragging)
			{
				ScrollToSelection(false);
			}
		}

		void ScrollToSelection(bool animate)
		{
			if (Element == null) return;

			_native.SetContentOffset(new CoreGraphics.CGPoint
				(_native.Bounds.Width *
					Math.Max(0, ((CarouselLayout)Element).SelectedIndex),
					_native.ContentOffset.Y),
				animate);

			// Haiyun
			//_lastScrollTime = DateTime.Now;
		}

		public override void Draw(CoreGraphics.CGRect rect)
		{
			base.Draw(rect);
			ScrollToSelection(false);
		}
	}
}

