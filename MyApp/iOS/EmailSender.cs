using Foundation;
using UIKit;
using Xamarin.Forms;
using MyApp.iOS;
using MessageUI;
using System;

[assembly: Dependency(typeof(EmailSender))]

namespace MyApp.iOS
{
	public class EmailSender : ISender
	{
		public void Send(string[] toRecipients, string subject, string body)
		{
			MFMailComposeViewController mailController;

			if (MFMailComposeViewController.CanSendMail)
			{
				mailController = new MFMailComposeViewController();

				mailController.SetToRecipients(toRecipients);
				mailController.SetSubject(subject);
				mailController.SetMessageBody(body, false);

				mailController.Finished += (object s, MFComposeResultEventArgs args) =>
				{
					Console.WriteLine(args.Result.ToString());
					args.Controller.DismissViewController(true, null);
				};

				var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;
				var navcontroller = rootController as UINavigationController;
				if (navcontroller != null)
					rootController = navcontroller.VisibleViewController;
				rootController.PresentViewController(mailController, true, null);
			}
		}
	}
}
