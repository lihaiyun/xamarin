using Xamarin.Forms;
using MyApp.iOS;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using UIKit;

[assembly: Dependency(typeof(QRCode))]

namespace MyApp.iOS
{
	public class QRCode : IQRCode
	{
		public Stream GetImageStream(string text, int width = 300, int height = 300)
		{
			var barcodeWriter = new ZXing.Mobile.BarcodeWriter
			{
				Format = ZXing.BarcodeFormat.QR_CODE,
				Options = new ZXing.Common.EncodingOptions
				{
					Width = width,
					Height = height
				}
			};
			barcodeWriter.Renderer = new ZXing.Mobile.BitmapRenderer();
			var bitmap = barcodeWriter.Write(text);
			var stream = bitmap.AsPNG().AsStream(); // this is the difference 
			stream.Position = 0;

			return stream;
		}

		public async Task<string> ScanQRCode()
		{
			string text = null;

			var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;
			var navcontroller = rootController as UINavigationController;
			if (navcontroller != null)
				rootController = navcontroller.VisibleViewController;

			var scanner = new ZXing.Mobile.MobileBarcodeScanner(rootController);
			var result = await scanner.Scan(true);

			if (result != null)
			{
				text = result.Text;
			}

			return text;
		}
	}
}
