using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace AndromedaLauncher
{
    public class Form1 :  MetroFramework.Forms.MetroForm
    {
        public int statusNr = 0;

        private Image imgFrameNews;

        private Image imgProgressBarFrame;

        private Image imgProgressBar;

        private Image imgMenuSeparator;

        public ImageButton ib;

        private PrivateFontCollection pfc = new PrivateFontCollection();

        private Font fn2;

        public string status = "";

        public string error = "";

        public int progressMax = 100;

        public int progress = 100;

        private int correctHeight = -15;

        public string chosenLang = "en";

        private bool doNotReactOnDDLLangChange = false;

        private IContainer components = null;

        public Label lblAction;

        public Label lblPercent;

        private LinkLabel linkLabel1;

        private LinkLabel linkLabel2;

        private TextBox lblError;

        private ComboBox comboBox1;

        private WebBrowser webBrowser1;
        private TextBox textBox1;
        private Button2 button21;
        private PictureBox pictureBox1;
        private LinkLabel linkLabel3;

        public Form1()
        {
            this.InitializeComponent();
            this.imgFrameNews = new Bitmap(Resource1.frame_news);
            this.imgProgressBarFrame = new Bitmap(Resource1.frame_loading);
            this.imgProgressBar = new Bitmap(Resource1.loading_bar);
            this.imgMenuSeparator = new Bitmap(Resource1.menu_separator);
            Font font = new Font("Arial", 12f);
            this.lblAction.Font = font;
            this.lblAction.BackColor = Color.Transparent;
            this.lblAction.ForeColor = Color.White;
            Label top = this.lblAction;
            top.Top = top.Top + this.correctHeight;
            this.lblPercent.BackColor = Color.Transparent;
            this.lblPercent.ForeColor = Color.White;
            this.lblPercent.Font = font;
            Label label = this.lblPercent;
            label.Top = label.Top + this.correctHeight;
            this.lblError.BackColor = Color.FromArgb(255, 16, 40, 52);
            this.linkLabel1.Font = font;
            this.linkLabel2.Font = font;
            this.linkLabel3.Font = font;
            Font font1 = new Font("Arial", 12f);
            this.lblError.Font = font1;
            this.ib = new ImageButton()
            {
                NormalImage = Resource1.button_inactive2,
                HoverImage = Resource1.button_hvr,
                Enabled = false,
                Left = 590,
                Top = 445 + this.correctHeight,
                Width = 252,
                Height = 44,
                Font = font,
                Text = "Play Game"
            };
            base.Controls.Add(this.ib);
            this.doNotReactOnDDLLangChange = true;
            this.comboBox1.SelectedIndex = 0;
            this.comboBox1.BackColor = Color.FromArgb(255, 16, 40, 52);
            this.SetBrowserUrl();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.doNotReactOnDDLLangChange)
            {
                this.chosenLang = StaticData.languages.Keys[this.comboBox1.SelectedIndex];
                (new Thread(new ThreadStart(this.RunSmallLangPackLoader))).Start();
                this.TrySetLangInRegistry();
            }
            else
            {
                this.doNotReactOnDDLLangChange = false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if ((!disposing ? false : this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        internal void DrawProgress()
        {
            this.DrawProgress2(base.CreateGraphics());
        }

        private void DrawProgress2(Graphics g)
        {
            if (this.progressMax != 0)
            {
                int num = (int)((float)this.progress / (float)this.progressMax * 525f);
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(this.imgProgressBar, new RectangleF(35f, (float)(461 + this.correctHeight), (float)num, 13f), new RectangleF(-0.5f, -0.5f, 1f, 13f), GraphicsUnit.Pixel);
            }
        }

        public void EnableStartGame()
        {
            this.ib.Enabled = true;
            this.ib.NormalImage = Resource1.button_active2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Program.waiter.Set();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblAction = new System.Windows.Forms.Label();
            this.lblPercent = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.lblError = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button21 = new AndromedaLauncher.Button2();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAction
            // 
            resources.ApplyResources(this.lblAction, "lblAction");
            this.lblAction.Name = "lblAction";
            // 
            // lblPercent
            // 
            resources.ApplyResources(this.lblPercent, "lblPercent");
            this.lblPercent.Name = "lblPercent";
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(163)))), ((int)(((byte)(209)))));
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(163)))), ((int)(((byte)(209)))));
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            resources.ApplyResources(this.linkLabel2, "linkLabel2");
            this.linkLabel2.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(163)))), ((int)(((byte)(209)))));
            this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel2.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(163)))), ((int)(((byte)(209)))));
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.TabStop = true;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // lblError
            // 
            this.lblError.BackColor = System.Drawing.Color.Navy;
            this.lblError.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblError.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.lblError, "lblError");
            this.lblError.Name = "lblError";
            this.lblError.ReadOnly = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.ForeColor = System.Drawing.Color.Khaki;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            resources.GetString("comboBox1.Items")});
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // webBrowser1
            // 
            resources.ApplyResources(this.webBrowser1, "webBrowser1");
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Url = new System.Uri("http://dev.andromeda5.com", System.UriKind.Absolute);
            // 
            // linkLabel3
            // 
            resources.ApplyResources(this.linkLabel3, "linkLabel3");
            this.linkLabel3.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel3.ForeColor = System.Drawing.Color.Transparent;
            this.linkLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel3.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(163)))), ((int)(((byte)(209)))));
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.TabStop = true;
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Navy;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ShortcutsEnabled = false;
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button21
            // 
            resources.ApplyResources(this.button21, "button21");
            this.button21.Name = "button21";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button21);
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.lblAction);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShadowType = MetroFramework.Forms.MetroForm.MetroFormShadowType.None;
            this.TransparencyKey = System.Drawing.Color.Thistle;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://andromeda5.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://forum.andromeda5.com/");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(string.Concat("http://andromeda5.com/?show=community&lang=", this.chosenLang));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(this.imgProgressBarFrame, new Point(20, 440 + this.correctHeight));
            e.Graphics.DrawImageUnscaled(this.imgMenuSeparator, new Point(589, 350));
            e.Graphics.DrawImageUnscaled(this.imgMenuSeparator, new Point(589, 320));
            e.Graphics.DrawImageUnscaled(this.imgMenuSeparator, new Point(589, 290));
            StringFormat stringFormat = new StringFormat();
            base.OnPaint(e);
        }

        public void RefreshBrowser()
        {
            this.webBrowser1.Refresh(WebBrowserRefreshOption.Completely);
        }

        private void RunSmallLangPackLoader()
        {
            try
            {
                try
                {
                    TcpClient tcpClient = new TcpClient()
                    {
                        ReceiveTimeout = 1500,
                        SendTimeout = 1500
                    };
                    tcpClient.Connect(new IPEndPoint(IPAddress.Parse(Program.loginServer), 13900));
                    GenericData genericDatum = new GenericData()
                    {
                        int1 = 164,
                        str1 = this.chosenLang
                    };
                    NetworkStream stream = tcpClient.GetStream();
                    TransferablesFramework.SerializeITransferable(stream, genericDatum, TransferContext.None);
                    StaticData.translations = ((InitialPack)TransferablesFramework.DeserializeITransferable(stream)).translations;
                    base.BeginInvoke(new MethodInvoker(this.SetLabels));
                    try
                    {
                        tcpClient.Close();
                    }
                    catch (Exception exception)
                    {
                    }
                }
                catch (Exception exception1)
                {
                }
            }
            finally
            {
            }
        }

        public void SetBrowserUrl()
        {
            this.webBrowser1.Url = new Uri(string.Concat("http://andromeda5.com/?show=news&lang=", this.chosenLang));
            this.webBrowser1.ScrollBarsEnabled = true;
        }

        public void SetError()
        {
            this.lblError.Text = this.error;
            this.lblError.Visible = true;
        }

        internal void SetLabels()
        {
            try
            {
                switch (this.statusNr)
                {
                    case 0:
                        {
                            this.lblAction.Text = StaticData.Translate("key_launcher_status_connecting");
                            break;
                        }
                    case 1:
                        {
                            this.lblAction.Text = StaticData.Translate("key_launcher_status_downloading");
                            break;
                        }
                    case 2:
                        {
                            this.lblAction.Text = StaticData.Translate("key_launcher_status_unzip");
                            break;
                        }
                    case 3:
                        {
                            this.lblAction.Text = StaticData.Translate("key_launcher_status_ready");
                            break;
                        }
                    case 4:
                        {
                            this.lblAction.Text = StaticData.Translate("key_launcher_status_retry");
                            break;
                        }
                }
                this.comboBox1.Items.Clear();
                int num = 0;
                for (int i = 0; i < StaticData.languages.Count; i++)
                {
                    if (StaticData.languages.Keys[i] == this.chosenLang)
                    {
                        num = i;
                    }
                    string item = StaticData.languages[StaticData.languages.Keys[i]];
                    this.comboBox1.Items.Add(item);
                }
                this.doNotReactOnDDLLangChange = true;
                this.comboBox1.SelectedIndex = num;
                this.ib.Text = StaticData.Translate("key_launcher_btn_play");
                this.linkLabel2.Text = StaticData.Translate("key_launcher_forums");
                this.linkLabel3.Text = StaticData.Translate("key_launcher_news");
                this.Text = StaticData.Translate("key_launcher_title");
                this.SetBrowserUrl();
                base.Invalidate();
            }
            catch { }
        }

        public void SetPercent()
        {
            Label label = this.lblPercent;
            int num = (int)((float)this.progress / (float)this.progressMax * 100f);
            label.Text = string.Concat(num.ToString(), "%");
        }

        public void SetStatus()
        {
            this.lblAction.Text = this.status;
        }

        private void TrySetLangInRegistry()
        {
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("XS-User").OpenSubKey("Andromeda5");
                string str = (
                    from w in registryKey.GetValueNames()
                    where w.StartsWith("Lang")
                    select w).First<string>();
                registryKey.SetValue(str, this.chosenLang);
            }
            catch (Exception exception)
            {
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            string directoryName = System.IO.Path.GetDirectoryName(Program.gamePath);
            System.Diagnostics.Process.Start("explorer", System.IO.Path.Combine(directoryName )); 
       

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
           
          new AuthAdim().ShowDialog();
        }
    }
}