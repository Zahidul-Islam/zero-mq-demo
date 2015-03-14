using System;
using NetMQ;
using System.Threading;

namespace ResponseWorker
{
	internal static class ResponseClient
	{
		private const string WorkerEndPoint = "tcp://127.0.0.1:5560";

		public static void Main (string[] args)
		{
			using (var context = NetMQContext.Create ())
			using (var worker = context.CreateResponseSocket ()) {
				worker.Connect (WorkerEndPoint);

				while (true) {
					var msg = worker.ReceiveMessage ();
					Console.WriteLine ("Processing Message {0}", msg.Last.ConvertToString());
				
					Thread.Sleep (500);

					worker.Send (msg.Last.ConvertToString ());
				}
			}
		}
	}
}
