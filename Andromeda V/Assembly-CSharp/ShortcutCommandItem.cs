using System;
using UnityEngine;

public class ShortcutCommandItem
{
	public KeyCode keyCodeOne;

	public KeyCode keyCodeTwo;

	public bool isKeyOneUsingShift;

	public bool isKeyTwoUsingShift;

	public bool isKeyOneUsingCtrl;

	public bool isKeyTwoUsingCtrl;

	public long dbKey;

	public string commandStringKey;

	public string tooltipKey;

	public KeyboardCommand command;

	public bool isInBase;

	public virtual bool IsPressed
	{
		get
		{
			bool flag;
			bool flag1;
			bool flag2;
			bool flag3;
			if (this.keyCodeOne != null && this.keyCodeTwo != null)
			{
				if (this.isKeyOneUsingShift && !this.isKeyOneUsingCtrl)
				{
					flag = (Input.GetKey(303) || Input.GetKey(304) ? Input.GetKeyDown(this.keyCodeOne) : false);
				}
				else if (this.isKeyOneUsingCtrl && !this.isKeyOneUsingShift)
				{
					flag = (Input.GetKey(305) || Input.GetKey(306) ? Input.GetKeyDown(this.keyCodeOne) : false);
				}
				else if (!this.isKeyOneUsingShift || !this.isKeyOneUsingCtrl)
				{
					flag = (Input.GetKey(304) || Input.GetKey(303) || Input.GetKey(305) || Input.GetKey(306) ? false : Input.GetKeyDown(this.keyCodeOne));
				}
				else
				{
					flag = ((Input.GetKey(303) || Input.GetKey(304)) && (Input.GetKey(305) || Input.GetKey(306)) ? Input.GetKeyDown(this.keyCodeOne) : false);
				}
				if (this.isKeyTwoUsingShift && !this.isKeyTwoUsingCtrl)
				{
					flag1 = (Input.GetKey(303) || Input.GetKey(304) ? Input.GetKeyDown(this.keyCodeTwo) : false);
				}
				else if (this.isKeyTwoUsingCtrl && !this.isKeyTwoUsingShift)
				{
					flag1 = (Input.GetKey(305) || Input.GetKey(306) ? Input.GetKeyDown(this.keyCodeTwo) : false);
				}
				else if (!this.isKeyTwoUsingShift || !this.isKeyTwoUsingCtrl)
				{
					flag1 = (Input.GetKey(304) || Input.GetKey(303) || Input.GetKey(305) || Input.GetKey(306) ? false : Input.GetKeyDown(this.keyCodeTwo));
				}
				else
				{
					flag1 = ((Input.GetKey(303) || Input.GetKey(304)) && (Input.GetKey(305) || Input.GetKey(306)) ? Input.GetKeyDown(this.keyCodeTwo) : false);
				}
				return (flag ? true : flag1);
			}
			if (this.keyCodeOne != null && this.keyCodeTwo == null)
			{
				if (this.isKeyOneUsingShift && !this.isKeyOneUsingCtrl)
				{
					flag2 = (Input.GetKey(303) || Input.GetKey(304) ? Input.GetKeyDown(this.keyCodeOne) : false);
				}
				else if (this.isKeyOneUsingCtrl && !this.isKeyOneUsingShift)
				{
					flag2 = (Input.GetKey(305) || Input.GetKey(306) ? Input.GetKeyDown(this.keyCodeOne) : false);
				}
				else if (!this.isKeyOneUsingShift || !this.isKeyOneUsingCtrl)
				{
					flag2 = (Input.GetKey(304) || Input.GetKey(303) || Input.GetKey(305) || Input.GetKey(306) ? false : Input.GetKeyDown(this.keyCodeOne));
				}
				else
				{
					flag2 = ((Input.GetKey(303) || Input.GetKey(304)) && (Input.GetKey(305) || Input.GetKey(306)) ? Input.GetKeyDown(this.keyCodeOne) : false);
				}
				return flag2;
			}
			if (this.keyCodeTwo == null || this.keyCodeOne != null)
			{
				return false;
			}
			if (this.isKeyTwoUsingShift && !this.isKeyTwoUsingCtrl)
			{
				flag3 = (Input.GetKey(303) || Input.GetKey(304) ? Input.GetKeyDown(this.keyCodeTwo) : false);
			}
			else if (this.isKeyTwoUsingCtrl && !this.isKeyTwoUsingShift)
			{
				flag3 = (Input.GetKey(305) || Input.GetKey(306) ? Input.GetKeyDown(this.keyCodeTwo) : false);
			}
			else if (!this.isKeyTwoUsingShift || !this.isKeyTwoUsingCtrl)
			{
				flag3 = (Input.GetKey(304) || Input.GetKey(303) || Input.GetKey(305) || Input.GetKey(306) ? false : Input.GetKeyDown(this.keyCodeTwo));
			}
			else
			{
				flag3 = ((Input.GetKey(303) || Input.GetKey(304)) && (Input.GetKey(305) || Input.GetKey(306)) ? Input.GetKeyDown(this.keyCodeTwo) : false);
			}
			return flag3;
		}
	}

	public virtual bool IsPressedDown
	{
		get
		{
			bool key;
			bool flag;
			bool key1;
			bool flag1;
			if (this.keyCodeOne != null && this.keyCodeTwo != null)
			{
				if (this.isKeyOneUsingShift && !this.isKeyOneUsingCtrl)
				{
					key = (Input.GetKey(303) || Input.GetKey(304) ? Input.GetKey(this.keyCodeOne) : false);
				}
				else if (this.isKeyOneUsingCtrl && !this.isKeyOneUsingShift)
				{
					key = (Input.GetKey(305) || Input.GetKey(306) ? Input.GetKey(this.keyCodeOne) : false);
				}
				else if (!this.isKeyOneUsingShift || !this.isKeyOneUsingCtrl)
				{
					key = Input.GetKey(this.keyCodeOne);
				}
				else
				{
					key = ((Input.GetKey(303) || Input.GetKey(304)) && (Input.GetKey(305) || Input.GetKey(306)) ? Input.GetKey(this.keyCodeOne) : false);
				}
				if (this.isKeyTwoUsingShift && !this.isKeyTwoUsingCtrl)
				{
					flag = (Input.GetKey(303) || Input.GetKey(304) ? Input.GetKey(this.keyCodeTwo) : false);
				}
				else if (this.isKeyTwoUsingCtrl && !this.isKeyTwoUsingShift)
				{
					flag = (Input.GetKey(305) || Input.GetKey(306) ? Input.GetKey(this.keyCodeTwo) : false);
				}
				else if (!this.isKeyTwoUsingShift || !this.isKeyTwoUsingCtrl)
				{
					flag = Input.GetKey(this.keyCodeTwo);
				}
				else
				{
					flag = ((Input.GetKey(303) || Input.GetKey(304)) && (Input.GetKey(305) || Input.GetKey(306)) ? Input.GetKey(this.keyCodeTwo) : false);
				}
				return (key ? true : flag);
			}
			if (this.keyCodeOne != null && this.keyCodeTwo == null)
			{
				if (this.isKeyOneUsingShift && !this.isKeyOneUsingCtrl)
				{
					key1 = (Input.GetKey(303) || Input.GetKey(304) ? Input.GetKey(this.keyCodeOne) : false);
				}
				else if (this.isKeyOneUsingCtrl && !this.isKeyOneUsingShift)
				{
					key1 = (Input.GetKey(305) || Input.GetKey(306) ? Input.GetKey(this.keyCodeOne) : false);
				}
				else if (!this.isKeyOneUsingShift || !this.isKeyOneUsingCtrl)
				{
					key1 = Input.GetKey(this.keyCodeOne);
				}
				else
				{
					key1 = ((Input.GetKey(303) || Input.GetKey(304)) && (Input.GetKey(305) || Input.GetKey(306)) ? Input.GetKey(this.keyCodeOne) : false);
				}
				return key1;
			}
			if (this.keyCodeTwo == null || this.keyCodeOne != null)
			{
				return false;
			}
			if (this.isKeyTwoUsingShift && !this.isKeyTwoUsingCtrl)
			{
				flag1 = (Input.GetKey(303) || Input.GetKey(304) ? Input.GetKey(this.keyCodeTwo) : false);
			}
			else if (this.isKeyTwoUsingCtrl && !this.isKeyTwoUsingShift)
			{
				flag1 = (Input.GetKey(305) || Input.GetKey(306) ? Input.GetKey(this.keyCodeTwo) : false);
			}
			else if (!this.isKeyTwoUsingShift || !this.isKeyTwoUsingCtrl)
			{
				flag1 = Input.GetKey(this.keyCodeTwo);
			}
			else
			{
				flag1 = ((Input.GetKey(303) || Input.GetKey(304)) && (Input.GetKey(305) || Input.GetKey(306)) ? Input.GetKey(this.keyCodeTwo) : false);
			}
			return flag1;
		}
	}

	public virtual bool IsPressedUp
	{
		get
		{
			bool flag;
			bool flag1;
			bool flag2;
			bool flag3;
			if (this.keyCodeOne != null && this.keyCodeTwo != null)
			{
				if (this.isKeyOneUsingShift && !this.isKeyOneUsingCtrl)
				{
					flag = (Input.GetKey(303) || Input.GetKey(304) ? Input.GetKeyUp(this.keyCodeOne) : false);
				}
				else if (this.isKeyOneUsingCtrl && !this.isKeyOneUsingShift)
				{
					flag = (Input.GetKey(305) || Input.GetKey(306) ? Input.GetKeyUp(this.keyCodeOne) : false);
				}
				else if (!this.isKeyOneUsingShift || !this.isKeyOneUsingCtrl)
				{
					flag = (Input.GetKey(304) || Input.GetKey(303) || Input.GetKey(305) || Input.GetKey(306) ? false : Input.GetKeyUp(this.keyCodeOne));
				}
				else
				{
					flag = ((Input.GetKey(303) || Input.GetKey(304)) && (Input.GetKey(305) || Input.GetKey(306)) ? Input.GetKeyUp(this.keyCodeOne) : false);
				}
				if (this.isKeyTwoUsingShift && !this.isKeyTwoUsingCtrl)
				{
					flag1 = (Input.GetKey(303) || Input.GetKey(304) ? Input.GetKeyUp(this.keyCodeTwo) : false);
				}
				else if (this.isKeyTwoUsingCtrl && !this.isKeyTwoUsingShift)
				{
					flag1 = (Input.GetKey(305) || Input.GetKey(306) ? Input.GetKeyUp(this.keyCodeTwo) : false);
				}
				else if (!this.isKeyTwoUsingShift || !this.isKeyTwoUsingCtrl)
				{
					flag1 = (Input.GetKey(304) || Input.GetKey(303) || Input.GetKey(305) || Input.GetKey(306) ? false : Input.GetKeyUp(this.keyCodeTwo));
				}
				else
				{
					flag1 = ((Input.GetKey(303) || Input.GetKey(304)) && (Input.GetKey(305) || Input.GetKey(306)) ? Input.GetKeyUp(this.keyCodeTwo) : false);
				}
				return (flag ? true : flag1);
			}
			if (this.keyCodeOne != null && this.keyCodeTwo == null)
			{
				if (this.isKeyOneUsingShift)
				{
					flag2 = (Input.GetKey(303) || Input.GetKey(304) ? Input.GetKeyUp(this.keyCodeOne) : false);
				}
				else if (this.isKeyOneUsingCtrl)
				{
					flag2 = (Input.GetKey(305) || Input.GetKey(306) ? Input.GetKeyUp(this.keyCodeOne) : false);
				}
				else if (!this.isKeyOneUsingShift || !this.isKeyOneUsingCtrl)
				{
					flag2 = (Input.GetKey(304) || Input.GetKey(303) || Input.GetKey(305) || Input.GetKey(306) ? false : Input.GetKeyUp(this.keyCodeOne));
				}
				else
				{
					flag2 = ((Input.GetKey(303) || Input.GetKey(304)) && (Input.GetKey(305) || Input.GetKey(306)) ? Input.GetKeyUp(this.keyCodeOne) : false);
				}
				return flag2;
			}
			if (this.keyCodeTwo == null || this.keyCodeOne != null)
			{
				return false;
			}
			if (this.isKeyTwoUsingShift)
			{
				flag3 = (Input.GetKey(303) || Input.GetKey(304) ? Input.GetKeyUp(this.keyCodeTwo) : false);
			}
			else if (this.isKeyTwoUsingCtrl)
			{
				flag3 = (Input.GetKey(305) || Input.GetKey(306) ? Input.GetKeyUp(this.keyCodeTwo) : false);
			}
			else if (!this.isKeyTwoUsingShift || !this.isKeyTwoUsingCtrl)
			{
				flag3 = (Input.GetKey(304) || Input.GetKey(303) || Input.GetKey(305) || Input.GetKey(306) ? false : Input.GetKeyUp(this.keyCodeTwo));
			}
			else
			{
				flag3 = ((Input.GetKey(303) || Input.GetKey(304)) && (Input.GetKey(305) || Input.GetKey(306)) ? Input.GetKeyUp(this.keyCodeTwo) : false);
			}
			return flag3;
		}
	}

	public ShortcutCommandItem()
	{
		this.keyCodeOne = 0;
		this.keyCodeTwo = 0;
	}
}