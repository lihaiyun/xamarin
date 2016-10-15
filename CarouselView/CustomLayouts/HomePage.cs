using System;
using Xamarin.Forms;
using CustomLayouts.Controls;
using CustomLayouts.ViewModels;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Timers;

namespace CustomLayouts
{
	public class HomePage : ContentPage
	{
		View _tabs;

		RelativeLayout relativeLayout;

		CarouselLayout.IndicatorStyleEnum _indicatorStyle;

		SwitcherPageViewModel viewModel;

		Timer aTimer;

		public HomePage(CarouselLayout.IndicatorStyleEnum indicatorStyle)
		{
			_indicatorStyle = indicatorStyle;

			viewModel = new SwitcherPageViewModel();
			BindingContext = viewModel;

			Title = _indicatorStyle.ToString();

			relativeLayout = new RelativeLayout 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			var pagesCarousel = CreatePagesCarousel();
			var dots = CreatePagerIndicatorContainer();
			_tabs = CreateTabs();

			Button btnNext = new Button();
			btnNext.Text = "Next";
			btnNext.Clicked += OnButtonClicked;

			switch(pagesCarousel.IndicatorStyle)
			{
				case CarouselLayout.IndicatorStyleEnum.Dots:
					relativeLayout.Children.Add (pagesCarousel,
						Constraint.RelativeToParent ((parent) => { return parent.X; }),
						Constraint.RelativeToParent ((parent) => { return parent.Y; }),
						Constraint.RelativeToParent ((parent) => { return parent.Width; }),
						Constraint.RelativeToParent ((parent) => { return parent.Width/16.0*9.0; })
					);

					relativeLayout.Children.Add (dots, 
						Constraint.Constant (0),
						Constraint.RelativeToView (pagesCarousel, 
							(parent,sibling) => { return sibling.Height - 18; }),
						Constraint.RelativeToParent (parent => parent.Width),
						Constraint.Constant (18)
					);

					relativeLayout.Children.Add(btnNext,
						Constraint.Constant(0),
						Constraint.RelativeToView(pagesCarousel,
							(parent, sibling) => { return sibling.Height + 30; }),
						Constraint.RelativeToParent(parent => parent.Width),
						Constraint.Constant(30)
					);
					
					break;
				case CarouselLayout.IndicatorStyleEnum.Tabs:
					var tabsHeight = 50;
					relativeLayout.Children.Add (_tabs, 
						Constraint.Constant (0),
						Constraint.RelativeToParent ((parent) => { return parent.Height - tabsHeight; }),
						Constraint.RelativeToParent (parent => parent.Width),
						Constraint.Constant (tabsHeight)
					);

					relativeLayout.Children.Add (pagesCarousel,
						Constraint.RelativeToParent ((parent) => { return parent.X; }),
						Constraint.RelativeToParent ((parent) => { return parent.Y; }),
						Constraint.RelativeToParent ((parent) => { return parent.Width; }),
						Constraint.RelativeToView (_tabs, (parent, sibling) => { return parent.Height - (sibling.Height); })
					);
					break;
				default:
					relativeLayout.Children.Add (pagesCarousel,
						Constraint.RelativeToParent ((parent) => { return parent.X; }),
						Constraint.RelativeToParent ((parent) => { return parent.Y; }),
						Constraint.RelativeToParent ((parent) => { return parent.Width; }),
						Constraint.RelativeToParent ((parent) => { return parent.Height; })
					);
					break;
			}

			Content = relativeLayout;

			// Haiyun
			aTimer = new Timer();
			aTimer.Elapsed += OnTimedEvent;
			aTimer.Interval = 5000;
			//aTimer.Enabled = true;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			aTimer.Enabled = true;
			System.Diagnostics.Debug.WriteLine("*****Here*****");
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			aTimer.Enabled = false;
			System.Diagnostics.Debug.WriteLine("*****Gone*****");
		}

		private void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			viewModel.NextPage();
		}

		void OnButtonClicked(object sender, EventArgs e)
		{
			viewModel.NextPage();
		}

		CarouselLayout CreatePagesCarousel ()
		{
			var carousel = new CarouselLayout {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				IndicatorStyle = _indicatorStyle,
				ItemTemplate = new DataTemplate(typeof(HomeView))
			};
			carousel.SetBinding(CarouselLayout.ItemsSourceProperty, "Pages");
			carousel.SetBinding(CarouselLayout.SelectedItemProperty, "CurrentPage", BindingMode.TwoWay);

			return carousel;
		}

		View CreatePagerIndicatorContainer()
		{
			return new StackLayout {
				Children = { CreatePagerIndicators() }
			};
		}

		View CreatePagerIndicators()
		{
			var pagerIndicator = new PagerIndicatorDots() { DotSize = 5, DotColor = Color.White };
			pagerIndicator.SetBinding (PagerIndicatorDots.ItemsSourceProperty, "Pages");
			pagerIndicator.SetBinding (PagerIndicatorDots.SelectedItemProperty, "CurrentPage");
			return pagerIndicator;
		}

		View CreateTabsContainer()
		{
			return new StackLayout {
				Children = { CreateTabs() }
			};
		}

		View CreateTabs()
		{
			var pagerIndicator = new PagerIndicatorTabs() { HorizontalOptions = LayoutOptions.CenterAndExpand };
			pagerIndicator.RowDefinitions.Add(new RowDefinition() { Height = 50 });
			pagerIndicator.SetBinding (PagerIndicatorTabs.ItemsSourceProperty, "Pages");
			pagerIndicator.SetBinding (PagerIndicatorTabs.SelectedItemProperty, "CurrentPage");

			return pagerIndicator;
		}
	}

	public class SpacingConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var items = value as IEnumerable<HomeViewModel>;

			var collection = new ColumnDefinitionCollection();
			foreach(var item in items)
			{
				collection.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
			}
			return collection;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

