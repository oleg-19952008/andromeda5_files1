using System;
using System.ComponentModel;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Management;
using System.Windows.Forms;

namespace AndromedaLauncher
{
	public class AuthAdim : MetroFramework.Forms.MetroForm
    {
		public string userName;

		public string password;

		public string domain;

		private IContainer components = null;

		private Button btnProceed;

		private Button btnCancel;

		private Label label1;

		private ComboBox comboBox1;

		private Label label2;

		private Label label3;

		private TextBox textBox1;

		private Label lblError;

		public AuthAdim()
		{
			this.InitializeComponent();
			if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
			{
				foreach (ManagementObject managementObject in (new ManagementObjectSearcher(new SelectQuery("Win32_UserAccount"))).Get())
				{
					CBItem cBItem = new CBItem()
					{
						Name = managementObject["Name"].ToString(),
						Domain = managementObject["Domain"].ToString(),
						LocalAccount = bool.Parse(managementObject["LocalAccount"].ToString())
					};
					this.comboBox1.Items.Add(cBItem);
				}
				this.comboBox1.SelectedIndex = 0;
				this.comboBox1.Focus();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			base.Close();
		}

		private void btnProceed_Click(object sender, EventArgs e)
		{
			ContextType contextType;
			this.lblError.Text = "";
			CBItem selectedItem = (CBItem)this.comboBox1.SelectedItem;
			if (selectedItem != null)
			{
				this.userName = selectedItem.Name;
				this.password = this.textBox1.Text;
				this.domain = selectedItem.Domain;
				contextType = (!selectedItem.LocalAccount ? ContextType.Domain : ContextType.Machine);
				this.Cursor = Cursors.WaitCursor;
				PrincipalContext principalContext = new PrincipalContext(contextType, this.domain);
				this.Cursor = Cursors.Default;
				bool flag = false;
				bool flag1 = false;
				Exception exception = null;
				try
				{
					flag = principalContext.ValidateCredentials(this.userName, this.password);
				}
				catch (Exception exception2)
				{
					Exception exception1 = exception2;
					flag1 = true;
					exception = exception1;
				}
				if (flag)
				{
					base.DialogResult = System.Windows.Forms.DialogResult.OK;
					base.Close();
				}
				else if (!flag1)
				{
					this.lblError.Text = "Cannot validate this user!";
				}
				else
				{
					this.lblError.Text = string.Concat("Cannot use account because of this error: ", exception.Message);
				}
			}
			else
			{
				MessageBox.Show("xexexeuehehe");
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

		public void InitializeComponent()
		{
			this.btnProceed = new Button();
			this.btnCancel = new Button();
			this.label1 = new Label();
			this.comboBox1 = new ComboBox();
			this.label2 = new Label();
			this.label3 = new Label();
			this.textBox1 = new TextBox();
			this.lblError = new Label();
			base.SuspendLayout();
			this.btnProceed.Location = new Point(348, 210);
			this.btnProceed.Name = "btnProceed";
			this.btnProceed.Size = new System.Drawing.Size(75, 23);
			this.btnProceed.TabIndex = 0;
			this.btnProceed.Text = "Proceed";
			this.btnProceed.UseVisualStyleBackColor = true;
			this.btnProceed.Click += new EventHandler(this.btnProceed_Click);
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new Point(267, 210);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(304, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Andromeda 5 needs an administrator privileges to finish update!";
			this.comboBox1.DisplayMember = "Name";
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new Point(143, 46);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 21);
			this.comboBox1.TabIndex = 3;
			this.comboBox1.ValueMember = "Name";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(13, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(110, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Administrator Account";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(13, 84);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Password";
			this.textBox1.Location = new Point(143, 84);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(121, 20);
			this.textBox1.TabIndex = 6;
			this.textBox1.UseSystemPasswordChar = true;
			this.lblError.AutoSize = true;
			this.lblError.ForeColor = Color.Red;
			this.lblError.Location = new Point(270, 84);
			this.lblError.Name = "lblError";
			this.lblError.Size = new System.Drawing.Size(0, 13);
			this.lblError.TabIndex = 7;
			base.AcceptButton = this.btnProceed;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new System.Drawing.Size(435, 245);
			base.ControlBox = false;
			base.Controls.Add(this.lblError);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.comboBox1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnProceed);
			base.Name = "AuthAdim";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Authorize as Administrator";
			base.TopMost = true;
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}