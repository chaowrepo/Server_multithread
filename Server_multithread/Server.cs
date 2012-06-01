using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace Server_multithread {
    public class Server {
        private TcpListener listener;
        public Thread listenthread;
        public delegate void ExceptionReceivedHandler(exception e);
        public event ExceptionReceivedHandler ExceptionReceived;

        public Server(int port) {
            listener = new TcpListener(IPAddress.Any, port);
            listenthread = new Thread(new ThreadStart(ListenForClients));
            listenthread.Start();
        }

        private void ListenForClients() {
            listener.Start();
            while (true) {
                TcpClient client = listener.AcceptTcpClient();
                Thread clientthread = new Thread(new ParameterizedThreadStart(HandleClient));
                clientthread.Start(client);
            }
        }

        private void HandleClient(object tcpclient) {
            TcpClient client = (TcpClient)tcpclient;
            NetworkStream stream = client.GetStream();
            while (true) {
                try {
                    if (stream.DataAvailable && client.Available != 0) {
                        BinaryReader br = new BinaryReader(stream);
                        exception e = new exception(br.ReadBytes(client.Available));
                        ExceptionReceived(e);
                        br.Close();
                    }
                }
                catch {
                    // socket error
                    break;
                }
            }
        }
    }
}
