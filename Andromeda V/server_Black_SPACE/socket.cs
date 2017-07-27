using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlackSPACE.socket
{
    public class socket
    {
        //public byte[] buffer = new byte[1024];
        //public static Socket socket_ = null;
        //public socket(Socket sock)
        //{
        //    socket_ = sock;
        //}
        public static int port = 0;
        public static ManualResetEvent alldone = new ManualResetEvent(false);
        
        public    static void start( )
        {
            Console.WriteLine("socket at port " + port + " started !");
            //var s = new Socket(socket_);
      
            var LEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(LEP);
                listener.Listen(100);
                while (true)
                {
                    alldone.Reset();
                    listener.BeginAccept(new AsyncCallback(socket.Accept_call_back), listener);
                    alldone.WaitOne();
                }
            }
            catch
            {

            }
           
        }
        public static void Accept_call_back(IAsyncResult ar)
        {
            alldone.Set();
            var listener = (Socket)ar.AsyncState;
            var handler = listener.EndAccept(ar);
           // new BlackSPACE.Server.sql().init_sql();
            new BlackSPACE.Server.user(handler);
        }
    }
}