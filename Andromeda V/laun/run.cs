using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace AndromedaLauncher
{
public partial class Program
    {
        public static void Run()
        {
            Program.waiter.WaitOne();
            try
            {
                try
                {
                    Program.SetState("Checking game integrity...");
                    Program.GetServers();
                    Program.gamePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "andromeda5\\Andromeda5v164\\Andromeda5.exe");
                    Program.DeleteOldMiddleMan();
                    if (Program.CheckVersion())
                    {
                        Program.f.statusNr = 3;
                        Program.SetState(StaticData.Translate("key_launcher_status_ready"));
                        Program.f.progress = 100;
                        Program.f.progressMax = 100;
                    }
                    else
                    {
                        //   Program.UpdateVersion();
                           }
                        Program.f.BeginInvoke(new MethodInvoker(Program.f.EnableStartGame));
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    if (Program.isRunning)
                    {
                        Program.SetError(exception.Message);
                    }
                }
            }
            finally
            {
                Program.isRunning = false;
            }
        }
    }
}
