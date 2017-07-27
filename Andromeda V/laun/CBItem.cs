using System;

namespace AndromedaLauncher
{
	public class CBItem
	{
		private string _name;

		private string _domain;

		private bool _localAccount;

		public string Domain
		{
			get
			{
				return this._domain;
			}
			set
			{
				this._domain = value;
			}
		}

		public bool LocalAccount
		{
			get
			{
				return this._localAccount;
			}
			set
			{
				this._localAccount = value;
			}
		}

		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		public CBItem()
		{
		}
	}
}