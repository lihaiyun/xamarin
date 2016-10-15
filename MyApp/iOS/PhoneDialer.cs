using Foundation;
using UIKit;
using Xamarin.Forms;
using MyApp.iOS;

[assembly: Dependency(typeof(PhoneDialer))]

namespace MyApp.iOS
{
	public class PhoneDialer : IDialer
	{
		public bool Dial(string number)
		{
			return UIApplication.SharedApplication.OpenUrl(
				new NSUrl("tel:" + number));
		}
	}
}
