using System;
using NetMQ;

namespace RequestResponseBroker
{
	internal static class Broker
	{
		private const string FrontendEndPoint = "tcp://127.0.0.1:5559";
		private const string BackendEndPoint = "tcp://127.0.0.1:5560";

		public static void Main (string[] args)
		{
			using (var context = NetMQContext.Create ())
			using (var frontend = context.CreateRouterSocket ())
			using (var backend = context.CreateDealerSocket ()) {
				frontend.Bind (FrontendEndPoint);
				backend.Bind (BackendEndPoint);

				// Handler for message coming in to the frontend
				frontend.ReceiveReady += (sender, e) => {
					var msg = frontend.ReceiveMessage();
					backend.SendMessage(msg); // Reply this message to the backend
				};

				// Handler for message comming into the backend
				backend.ReceiveReady += (sender, e) => {
					var msg = backend.ReceiveMessage();
					frontend.SendMessage(msg); // Reply this message to the frontend
				};

				using (var poller = new Poller ()) {
					poller.AddSocket (backend);
					poller.AddSocket (frontend);

					// Listen out for events on both socket and raise events when message come in
					poller.Start();
				}
			}
		}
	}
}
