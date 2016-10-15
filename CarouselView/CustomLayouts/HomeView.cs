using System;
using Xamarin.Forms;

namespace CustomLayouts
{
	public class HomeView : ContentView
	{
		public HomeView()
		{
			BackgroundColor = Color.White;

			var label = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = Color.Black
			};

			var image = new Image
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Aspect = Aspect.AspectFit
			};

			label.SetBinding(Label.TextProperty, "Title");
			image.SetBinding(Image.SourceProperty, "ImageSource");
			this.SetBinding(BackgroundColorProperty, "Background");

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					image
				}
			};
		}
	}
}

