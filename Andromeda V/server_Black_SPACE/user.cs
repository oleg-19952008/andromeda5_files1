using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace BlackSPACE.Server
{
    public partial class user

    {

        public byte[] buffer = new byte[2048];

        public Socket Socket_ { get; set; }

        public user(Socket hnd)
        {
      

            Socket_ = hnd;
            hnd.BeginReceive(buffer, 0, buffer.Length, 0, Read_call_back, this);
            FromBytes(buffer, 2048);
        }

        static void Main(string[] args)
        {
            //     socket.socket.port = 13700;
            //new Thread(     socket.socket.start ).Start();
            //socket.socket.port = 13900;
            //new Thread(socket.socket.start).Start();
            try
            {
                if (args.Length == 0)
                {
                    socket.socket.port = 13900;
                    new Thread(socket.socket.start).Start();
                    Console.Title = "server at port " + socket.socket.port;
                    Process.Start(Environment.CurrentDirectory + "\\BlackSPACE.Server.exe", "1");
                }
                if (args[0] == "1")
                {
                    Process.Start(Environment.CurrentDirectory + "\\BlackSPACE.Server.exe", "2");
                    socket.socket.port = 13901;
                    Console.Title = "server at port " + socket.socket.port;
                    new Thread(socket.socket.start).Start();
                    user.in_space = true;
                }
                //if (args[0] == "2")
                //{
                //    Process.Start(Environment.CurrentDirectory + "\\BlackSPACE.Server.exe", "3");
                //    socket.socket.port = 14000;
                //    Console.Title = "server at port " + socket.socket.port; new Thread(socket.socket.start).Start();
                //}
            }
            catch { }
            //if (port == 13900)
            //{
            //    port = 13901;

            //    start2();
            //}

        }


        //public void h(string s)
        //{

        //}
    }
}