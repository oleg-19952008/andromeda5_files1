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
        private static bool CheckVersion()
        {
            bool flag = false;
            int num = 0;
            while (!flag)
            {
                string str = StaticData.Translate("key_su_error_connecting");
                try
                {
                    Program.f.statusNr = 0;
                    Program.SetState("Connecting ...");
                    Program.c = new TcpClient()
                    {
                        ReceiveTimeout = Program.NET_TIMEOUT,
                        SendTimeout = Program.NET_TIMEOUT
                    };
                    Program.c.Connect(new IPEndPoint(IPAddress.Parse(Program.loginServer), 13900));
                 //   MessageBox.Show(Program.LAUNCHER_VERSION + "s");
                    InitialRequest initialRequest = new InitialRequest()
                    {
                        launcherVersion = Program.LAUNCHER_VERSION,
                        chosenLang = Program.f.chosenLang
                    };
                 //   MessageBox.Show("z");
                    TransferablesFramework.SerializeITransferable(Program.c.GetStream(), initialRequest, TransferContext.InitialRequestV1);
                    InitialPack initialPack = (InitialPack)TransferablesFramework.DeserializeITransferable(new BinaryReader(Program.c.GetStream()));
                    Program.c.Close();
                    if (!initialPack.isNeedNewLauncher)
                    {
                        Program.serverVersion = initialPack.version;
                        StaticData.translations = initialPack.translations;
                        StaticData.languages = initialPack.languages;
                        Program.f.BeginInvoke(new MethodInvoker(Program.f.SetLabels));
                        flag = true;
                        if (num > 0)
                        {
                            Program.f.BeginInvoke(new MethodInvoker(Program.f.RefreshBrowser));
                        }
                    }
                    else
                    {
                        Program.SelfUpdate();
                    }
                }
                catch (Exception e)
                {
                    Program.f.error = str;
                    Program.f.BeginInvoke(new MethodInvoker(Program.f.SetError));
                    Program.PlayStatusRetry("Connecting ...", 5);
                    num++;
                }
                Program.NET_TIMEOUT = Math.Max(Program.NET_TIMEOUT_INCREASE + Program.NET_TIMEOUT, Program.NET_TIMEOUT_MAX);
            }
            return Program.LocalVersion == Program.serverVersion;
        }
    }
}
