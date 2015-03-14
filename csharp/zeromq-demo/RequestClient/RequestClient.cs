using System;
using NetMQ;

namespace RequestClient
{
	internal class RequestClient
	{
		private const string _clientEndPoint = "tcp://127.0.0.1:5559";

		public static void Main (string[] args)
		{
			var count = 0;

			using (var context = NetMQContext.Create ())
			using (var client = context.CreateRequestSocket ()) {
				client.Connect (_clientEndPoint);

				while(true) {
					var msg = new NetMQMessage ();
					msg.Append ("Message_" + count);
					client.SendMessage (msg);
					Console.WriteLine ("Sent Message {0}", msg.Last.ConvertToString());

					var response = client.ReceiveMessage ();
					Console.WriteLine ("Received Message {0}", response.Last.ConvertToString ());
					count += 1;
				}

			}
		}
	}
}
