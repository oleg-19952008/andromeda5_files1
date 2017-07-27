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
        public const string serverAPI = "http://127.0.0.1/andro/index.php?platform=exe&ver=1&output=xml";

    //    private const string LAUNCHER_DOWNLOAD_URL = "http://asset.andromeda5.com/assets/Launcher.zip";

        private static short LAUNCHER_VERSION;

        private static int NET_TIMEOUT_START;

        private static int NET_TIMEOUT;

        private static int NET_TIMEOUT_INCREASE;

        private static int NET_TIMEOUT_MAX;

        public static AutoResetEvent waiter;

        private static bool isRunning;

        public static string loginServer;

        public static string assetServer;

        private static Thread t;

        public static string gamePath;

        private static TcpClient c;

        private static HttpWebRequest req;

        private static Stream _WebStream;

        private static Form1 f;

        private static MemoryStream ms;

        public static short serverVersion;

        private static bool IsUACOn
        {
            get
            {
                object value = Registry.LocalMachine.GetValue("HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\EnableLUA", (long)1);
                return (long)value != (long)0;
            }
        }

        public static string Lang
        {
            get
            {
                string value;
                try
                {
                    RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("XS-User").OpenSubKey("Andromeda5");
                    string str = (
                        from w in registryKey.GetValueNames()
                        where w.StartsWith("Lang")
                        select w).First<string>();
                    value = (string)registryKey.GetValue(str);
                }
                catch (Exception exception)
                {
                    value = "en";
                }
                return value;
            }
        }

        private static short LocalVersion
        {
            get
            {
                short value;
                Program.DeleteTmpFolder();
                string directoryName = Path.GetDirectoryName(Program.gamePath);
                directoryName = Path.Combine(directoryName, "Andromeda5_Data\\Managed\\TransferableObjects.dll");
                if (File.Exists(directoryName))
                {
                    string tempPath = Path.GetTempPath();
                    Guid guid = Guid.NewGuid();
                    string str = Path.Combine(tempPath, "Andromeda5", string.Concat(guid.ToString(), ".kor"));
                    File.Copy(directoryName, str, true);
                    Assembly assembly = Assembly.LoadFile(str);
                    Type type = assembly.GetType("StaticData");
                    object obj = assembly.CreateInstance("StaticData");
                    value = (short)type.GetField("build").GetValue(obj);
                }
                else
                {
                    value = -1;
                }
                return value;
            }
        }

        static Program()
        {
            Program.LAUNCHER_VERSION = 3;
            Program.NET_TIMEOUT_START = 5000;
            Program.NET_TIMEOUT = 4000;
            Program.NET_TIMEOUT_INCREASE = 1000;
            Program.NET_TIMEOUT_MAX = 10000;
            Program.isRunning = true;
            Program.loginServer = "127.0.0.1";
            Program.assetServer = "http://127.0.0.1/andro/cache";
            Program.serverVersion = 164;
        }

       

        private static void ClearFolder(string FolderName)
        {
            int i;
            if (Directory.Exists(FolderName))
            {
                var directoryInfo = new DirectoryInfo(FolderName);
                var files = directoryInfo.GetFiles();
                for (i = 0; i < (int)files.Length; i++)
                {
                    FileInfo fileInfo = files[i];
                    fileInfo.IsReadOnly = false;
                    fileInfo.Delete();
                }
                DirectoryInfo[] directories = directoryInfo.GetDirectories();
                for (i = 0; i < (int)directories.Length; i++)
                {
                    DirectoryInfo directoryInfo1 = directories[i];
                    Program.ClearFolder(directoryInfo1.FullName);
                    directoryInfo1.Delete();
                }
            }
        }

        private static void DeleteOldMiddleMan()
        {
            string str = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Andromeda5\\MiddleMan");
            if (Directory.Exists(str))
            {
                int num = 0;
                bool flag = false;
                while (!flag)
                {
                    try
                    {
                        Program.ClearFolder(str);
                        Directory.Delete(str);
                        flag = true;
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        num++;
                        if (num > 4)
                        {
                            throw exception;
                        }
                        Thread.Sleep(num * 200);
                    }
                }
            }
        }

        private static void DeleteOldVersion()
        {
            Program.ClearFolder(Path.GetDirectoryName(Program.gamePath));
        }

        private static void DeleteTmpFolder()
        {
            string str = Path.Combine(Path.GetTempPath(), "Andromeda5");
            if (!Directory.Exists(str))
            {
                Directory.CreateDirectory(str);
            }
            else
            {
                Program.ClearFolder(str);
            }
        }

        private static void DownloadNewVersion()
        {
            string str;
            bool flag = false;
            Program.NET_TIMEOUT = Program.NET_TIMEOUT_START;
            while (!flag)
            {
                Program.f.statusNr = 1;
                str = StaticData.Translate("key_launcher_error_download");
                WebResponse response = null;
                try
                {
                    try
                    {
                        Program.SetState(string.Concat(StaticData.Translate("key_launcher_status_downloading"), Program.serverVersion.ToString()));
                        string str1 = string.Concat(Program.assetServer, "/Andromeda5v", Program.serverVersion.ToString(), ".zip");
                        MessageBox.Show(str1);
                        Program.req = (HttpWebRequest)WebRequest.Create(str1);
                        Program.req.Timeout = Program.NET_TIMEOUT;
                        Program.req.ReadWriteTimeout = Program.NET_TIMEOUT;
                        Program.req.KeepAlive = false;
                        Program.req.AllowWriteStreamBuffering = true;
                        Program.req.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
                   //     Program.req.Referer = "http://andromeda5.com/LAUNCHER";
                        response = Program.req.GetResponse();
                        int contentLength = (int)response.ContentLength;
                        Program.f.progressMax = contentLength;
                        Program.f.progress = 0;
                        Program.f.BeginInvoke(new MethodInvoker(Program.f.SetPercent));
                        Program.f.Invalidate();
                        byte[] numArray = new byte[contentLength];
                        int num = 0;
                        Program._WebStream = response.GetResponseStream();
                        while (num < contentLength)
                        {
                            int num1 = Program._WebStream.Read(numArray, num, Math.Min(1000000, contentLength - num));
                            num = num + num1;
                            Program.f.progress = num;
                            Program.f.DrawProgress();
                            Program.f.BeginInvoke(new MethodInvoker(Program.f.SetPercent));
                        }
                        response.Close();
                        Program.ms = new MemoryStream(numArray);
                        flag = true;
                    }
                    catch (Exception exception)
                    {
                        Program.f.error = str;
                        Program.f.BeginInvoke(new MethodInvoker(Program.f.SetError));
                        Program.PlayStatusRetry(StaticData.Translate("key_launcher_status_downloading"), 5);
                    }
                }
                finally
                {
                    try
                    {
                        response.Close();
                    }
                    catch (Exception exception1)
                    {
                    }
                }
                Program.NET_TIMEOUT = Math.Max(Program.NET_TIMEOUT_INCREASE + Program.NET_TIMEOUT, Program.NET_TIMEOUT_MAX);
            }
            flag = false;
            while (!flag)
            {
                Program.f.statusNr = 2;
                str = StaticData.Translate("key_launcher_error_unzip");
                try
                {
                    Program.SetState(string.Concat(StaticData.Translate("key_launcher_status_unzip"), Program.serverVersion.ToString()));
                    Program.UnzipFromStream(Program.ms, Path.GetDirectoryName(Program.gamePath));
                    flag = true;
                }
                catch (Exception exception2)
                {
                    MessageBox.Show(exception2.ToString());
                    Program.f.error = str;
                    Program.f.BeginInvoke(new MethodInvoker(Program.f.SetError));
                    Program.PlayStatusRetry(string.Concat(StaticData.Translate("key_launcher_status_unzip"), Program.serverVersion.ToString()), 5);
                }
            }
            Program.f.statusNr = 3;
            Program.SetState(StaticData.Translate("key_launcher_status_ready"));
        }

        private static void GetServers()
        {
            WebClient webClient = new WebClient();
            string str = "";
            try
            {
                str = webClient.DownloadString("http://127.0.0.1/andro/index.php?platform=exe&ver=1&output=xml");
                XmlReaderSettings xmlReaderSetting = new XmlReaderSettings()
                {
                    ConformanceLevel = ConformanceLevel.Fragment
                };
                XPathDocument xPathDocument = new XPathDocument(XmlReader.Create(new StringReader(str), xmlReaderSetting));
                XPathNavigator xPathNavigator = xPathDocument.CreateNavigator();
                Program.loginServer = xPathNavigator.SelectSingleNode("/main/geoip_login_server").ToString();
                Program.assetServer = xPathNavigator.SelectSingleNode("/main/geoip_asset_server").ToString();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private static void ib_Click(object sender, EventArgs e)
        {
            Program.RunGame();
            Program.f.BeginInvoke(new MethodInvoker(Program.f.Close));
        }

        public static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
        }

        [STAThread]
        private static void Main()
        {
            gamePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "andromeda5\\Andromeda5v164\\Andromeda5.exe");

            Program.waiter = new AutoResetEvent(false);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Program.f = new Form1()
            {
                chosenLang = Program.Lang
            };
            Program.f.ib.Click += new EventHandler(Program.ib_Click);
            Program.t = new Thread(new ThreadStart(Program.Run));
            Program.t.Start();
            Application.Run(Program.f);
            Program.StopMachine();
            //Application.Run(     new AuthAdim());

        }

        private static void PlayStatusRetry(string status, int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Program.f.statusNr = 4;
                string str = StaticData.Translate("key_launcher_status_retry");
                if (str == "key_launcher_status_retry")
                {
                    str = StaticData.Translate("key_su_retry_in_seconds");
                }
                Program.f.status = string.Format(str, i);
                Program.f.BeginInvoke(new MethodInvoker(Program.f.SetStatus));
                Thread.Sleep(1000);
            }
            Program.f.status = status;
            Program.f.BeginInvoke(new MethodInvoker(Program.f.SetStatus));
            Program.f.error = "";
            Program.f.BeginInvoke(new MethodInvoker(Program.f.SetError));
        }

        private static void PrepareAppExit()
        {
            Application.Exit();
        }

       

        private static void RunGame()
        {
            Program.SetState("Starting the game...");
            MessageBox.Show(gamePath);
            Process.Start(Program.gamePath);
        }

        private static void SelfUpdate()
        {
            if ((Program.IsAdministrator() ? false : !Program.IsUACOn))
            {
                MessageBox.Show(StaticData.Translate("key_su_need_admin"), StaticData.Translate("key_su_andromeda5_launcher"));
                Application.Exit();
                Thread.Sleep(50);
            }
            string str = "";
            bool flag = false;
            Program.NET_TIMEOUT = Program.NET_TIMEOUT_START;
            while (!flag)
            {
                Program.f.statusNr = 1;
                str = StaticData.Translate("key_self_update_error_download");
                WebResponse response = null;
                try
                {
                    try
                    {
                        Program.SetState(string.Concat(StaticData.Translate("key_self_update_status_downloading"), Program.serverVersion.ToString()));
                        string str1 = string.Concat(Program.assetServer, "/Launcher.zip");
                        Program.req = (HttpWebRequest)WebRequest.Create(str1);
                        Program.req.Timeout = Program.NET_TIMEOUT;
                        Program.req.ReadWriteTimeout = Program.NET_TIMEOUT;
                        Program.req.KeepAlive = false;
                        Program.req.AllowWriteStreamBuffering = true;
                        Program.req.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
                     //   Program.req.Referer = "http://andromeda5.com/LAUNCHER";
                        response = Program.req.GetResponse();
                        int contentLength = (int)response.ContentLength;
                        Program.f.progressMax = contentLength;
                        Program.f.progress = 0;
                        Program.f.BeginInvoke(new MethodInvoker(Program.f.SetPercent));
                        Program.f.Invalidate();
                        byte[] numArray = new byte[contentLength];
                        int num = 0;
                        Program._WebStream = response.GetResponseStream();
                        while (num < contentLength)
                        {
                            int num1 = Program._WebStream.Read(numArray, num, Math.Min(1000000, contentLength - num));
                            num = num + num1;
                            Program.f.progress = num;
                            Program.f.DrawProgress();
                            Program.f.BeginInvoke(new MethodInvoker(Program.f.SetPercent));
                        }
                        response.Close();
                        Program.ms = new MemoryStream(numArray);
                        flag = true;
                    }
                    catch (Exception exception)
                    {
                        Program.f.error = str;
                        Program.f.BeginInvoke(new MethodInvoker(Program.f.SetError));
                        Program.PlayStatusRetry(StaticData.Translate("key_self_update_status_downloading"), 5);
                    }
                }
                finally
                {
                    try
                    {
                        response.Close();
                    }
                    catch (Exception exception1)
                    {
                    }
                }
                Program.NET_TIMEOUT = Math.Max(Program.NET_TIMEOUT_INCREASE + Program.NET_TIMEOUT, Program.NET_TIMEOUT_MAX);
            }
            string str2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Andromeda5");
            flag = false;
            Program.f.statusNr = 2;
            str = StaticData.Translate("key_su_error_status_unzip");
            Program.SetState(string.Concat(StaticData.Translate("key_su_status_unzip"), Program.serverVersion.ToString()));
            Program.UnzipFromStream(Program.ms, str2);
            Program.f.statusNr = 10;
            string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
            bool flag1 = true;
            while (flag1)
            {
                try
                {
                    Process.Start(Path.Combine(str2, "MiddleMan", "MiddleMan.exe"), string.Concat("\"", directoryName, "\""));
                    flag1 = false;
                }
                catch (Exception exception2)
                {
                    flag1 = MessageBox.Show(StaticData.Translate("key_su_error_replace_files"), StaticData.Translate("key_su_andromeda5_launcher_update"), MessageBoxButtons.RetryCancel) == DialogResult.Retry;
                }
            }
            Program.f.Close();
            Application.Exit();
            Thread.Sleep(50);
        }

        private static void SetError(string p)
        {
            Program.f.error = p;
            Program.f.BeginInvoke(new MethodInvoker(Program.f.SetError));
        }

        private static void SetState(string val)
        {
            Program.f.status = val;
            Program.f.BeginInvoke(new MethodInvoker(Program.f.SetStatus));
        }

        private static void StopGameInstances()
        {
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < (int)processes.Length; i++)
            {
                Process process = processes[i];
                if (process.ProcessName.ToUpper() == "Andromeda5".ToUpper())
                {
                    Program.SetState("Closing game instance...");
                    process.CloseMainWindow();
                }
            }
        }

        private static void StopMachine()
        {
            Program.isRunning = false;
            try
            {
                Program.c.Client.Close(1);
            }
            catch (Exception exception)
            {
            }
            try
            {
                Program.req.Abort();
            }
            catch (Exception exception1)
            {
            }
            try
            {
            }
            catch (Exception exception2)
            {
            }
            try
            {
                Program._WebStream.Close();
            }
            catch (Exception exception3)
            {
            }
            try
            {
                Program.t.Abort();
            }
            catch (Exception exception4)
            {
            }
        }

        private static void UnzipFromStream(Stream zipStream, string outFolder)
        {
            Program.f.progress = 0;
            Program.f.Invalidate();
            ZipInputStream zipInputStream = new ZipInputStream(zipStream);
            ZipEntry nextEntry = zipInputStream.GetNextEntry();
            zipInputStream.Length.ToString();
            zipInputStream.Position.ToString();
            while (true)
            {
                if (nextEntry == null)
                {
                    Program.f.progress = 100;
                    Program.f.progressMax = 100;
                    Program.f.DrawProgress();
                    Program.f.BeginInvoke(new MethodInvoker(Program.f.SetPercent));
                    break;
                }
                else if (Program.isRunning)
                {
                    Program.f.progress = (int)Program.ms.Position;
                    Program.f.DrawProgress();
                    Program.f.BeginInvoke(new MethodInvoker(Program.f.SetPercent));
                    string name = nextEntry.Name;
                    if (!nextEntry.IsDirectory)
                    {
                        byte[] numArray = new byte[4096];
                        string str = Path.Combine(outFolder, name);
                        string directoryName = Path.GetDirectoryName(str);
                        if (directoryName.Length > 0)
                        {
                            Directory.CreateDirectory(directoryName);
                        }
                        FileStream fileStream = File.Create(str);
                        try
                        {
                            StreamUtils.Copy(zipInputStream, fileStream, numArray);
                        }
                        finally
                        {
                            if (fileStream != null)
                            {
                                ((IDisposable)fileStream).Dispose();
                            }
                        }
                        nextEntry = zipInputStream.GetNextEntry();
                    }
                    else
                    {
                        Directory.CreateDirectory(Path.Combine(outFolder, name));
                        nextEntry = zipInputStream.GetNextEntry();
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private static bool UpdateVersion()
        {
            Program.SetState(string.Concat("Updating to version ", Program.serverVersion.ToString()));
            Program.StopGameInstances();
            Program.SetState("Deleting old version...");
            Program.DeleteOldVersion();
            Program.DownloadNewVersion();
            return true;
        }
    }
}