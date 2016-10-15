using System;
using System.IO;
using System.Threading.Tasks;

namespace MyApp
{
	public interface IQRCode
	{
		Stream GetImageStream(string text, int width = 300, int height = 300);

		Task<string> ScanQRCode();
	}
}
