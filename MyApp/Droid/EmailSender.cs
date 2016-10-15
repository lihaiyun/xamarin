using Android.Content;
using Xamarin.Forms;
using MyApp.Droid;

[assembly: Dependency(typeof(EmailSender))]

namespace MyApp.Droid
{
	public class EmailSender : ISender
	{
		public void Send(string[] toRecipients, string subject, string body)
		{
			var email = new Intent(Android.Content.Intent.ActionSend);

			email.PutExtra(Android.Content.Intent.ExtraEmail, toRecipients);
			email.PutExtra(Android.Content.Intent.ExtraSubject, subject);
			email.PutExtra(Android.Content.Intent.ExtraText, body);

			email.SetType("message/rfc822");

			Forms.Context.StartActivity(email);
		}
	}
}
