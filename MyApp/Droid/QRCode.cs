using Xamarin.Forms;
using MyApp.Droid;
using System.IO;
using Android.Graphics;
using System.Threading.Tasks;

[assembly: Dependency(typeof(QRCode))]

namespace MyApp.Droid
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
			var stream = new MemoryStream();
			bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream); // this is the difference 
			stream.Position = 0;

			return stream;
		}

		public async Task<string> ScanQRCode()
		{
			string text = null;

			var scanner = new ZXing.Mobile.MobileBarcodeScanner();
			var result = await scanner.Scan();

			if (result != null)
			{
				text = result.Text;
			}

			return text;
		}
	}
}
