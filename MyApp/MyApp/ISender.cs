using System;
namespace MyApp
{
	public interface ISender
	{
		void Send(string[] toRecipients, string subject, string body);
	}
}
