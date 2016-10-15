using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using ZXing;
using ZXing.Common;
using ZXing.Net.Mobile.Forms;

namespace MyApp
{
	public partial class MyAppPage : ContentPage
	{
		public MyAppPage()
		{
			Title = "My App";
			InitializeComponent();

			//var writer = new BarcodeWriter
			//{
			//	Format = BarcodeFormat.QR_CODE,
			//	Options = new EncodingOptions
			//	{
			//		Height = 300,
			//		Width = 300
			//	}
			//};
			//var byteArray = writer.Write("My content");

			//MemoryStream stream = new MemoryStream(byteArray);
			//stream.Position = 0;
			//imgQR.Source = ImageSource.FromStream(() => stream);

			string text = "Haiyun";
			var QRCode = DependencyService.Get<IQRCode>();
			if (QRCode != null)
			{
				Stream stream = QRCode.GetImageStream(text);
				imgQR.Source = ImageSource.FromStream(() => stream);
			}

			//ZXingBarcodeImageView barcode = new ZXingBarcodeImageView
			//{
			//	HorizontalOptions = LayoutOptions.FillAndExpand,
			//	VerticalOptions = LayoutOptions.FillAndExpand,
			//};
			//barcode.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
			//barcode.BarcodeOptions.Width = 300;
			//barcode.BarcodeOptions.Height = 300;
			//barcode.BarcodeOptions.Margin = 10;
			//barcode.BarcodeValue = "ZXing.Net.Mobile";
			//myContent.Content = barcode;
			//myContent.IsVisible = true;
		}

		async void OnCall(object sender, System.EventArgs e)
		{
			string phoneNumber = "97356432";

			if (await this.DisplayAlert(
					"Dial a Number",
					"Would you like to call " + phoneNumber + "?",
					"Yes",
					"No"))
			{
				var dialer = DependencyService.Get<IDialer>();
				if (dialer != null)
				{
					dialer.Dial(phoneNumber);
				}					
			}
		}

		void OnEmail(object sender, System.EventArgs e)
		{
			string[] toRecipients = new string[] { "john@doe.com" };
			string subject = "mail test";
			string body = "this is a test";

			var emailSender = DependencyService.Get<ISender>();
			if (emailSender != null)
			{
				emailSender.Send(toRecipients, subject, body);
			}
		}

		async void OnScan(object sender, System.EventArgs e)
		{
			var QRCode = DependencyService.Get<IQRCode>();
			if (QRCode != null)
			{
				string text = await QRCode.ScanQRCode();
				label.Text = text;
			}

			//var scanPage = new ZXingScannerPage();

			//scanPage.OnScanResult += (result) =>
			//{
			//	// Stop scanning
			//	scanPage.IsScanning = false;

			//	// Pop the page and show the result
			//	Device.BeginInvokeOnMainThread(() =>
			//	{
			//		Navigation.PopAsync();
			//		DisplayAlert("Scanned Barcode", result.Text, "OK");
			//	});
			//};

			// Navigate to our scanner page
			//await Navigation.PushAsync(scanPage);

			//var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
			//options.PossibleFormats = new List<ZXing.BarcodeFormat>() {
			//		ZXing.BarcodeFormat.QR_CODE
			//	};


			//var scanPage = new ZXingScannerPage(options);

			//scanPage.OnScanResult += (result) =>
			//{
			//	// Stop scanning
			//	scanPage.IsScanning = false;

			//	// Pop the page and show the result
			//	Device.BeginInvokeOnMainThread(() =>
			//	{
			//		DisplayAlert("Scanned Barcode", result.Text, "OK");
			//	});
			//};

			//await Navigation.PushModalAsync(scanPage);
		}
	}
}
