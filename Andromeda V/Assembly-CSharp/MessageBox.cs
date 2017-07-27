using System;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
	private static float y_OkCancelBtn;

	private static float x_OkCancelBtn;

	private static float w_idthOKCancelBtn;

	private static float h_OkCancelBtn;

	private static int winID;

	private static float y_TextArea;

	private static MessageBox.DialogResult _dlgRes;

	private static string _strPrompt;

	private static string _strRes;

	private static string _title;

	public static Action<MessageBox.DialogResult, string> dialogCallback;

	private static Rect dialogRect0;

	private static Rect dialogRect1;

	private static Rect dialogRect2;

	public static bool isOnScreen;

	static MessageBox()
	{
		MessageBox.y_OkCancelBtn = 50f;
		MessageBox.x_OkCancelBtn = 50f;
		MessageBox.w_idthOKCancelBtn = 100f;
		MessageBox.h_OkCancelBtn = 25f;
	}

	public MessageBox()
	{
	}

	private static void CallbackSimpleShow(MessageBox.DialogResult res, string textEntered)
	{
		MessageBox.isOnScreen = false;
	}

	private static void DoDialogResult(int windowID)
	{
		Rect rect = new Rect(10f, MessageBox.dialogRect0.get_height() - 30f, MessageBox.w_idthOKCancelBtn, MessageBox.h_OkCancelBtn);
		Rect rect1 = new Rect(MessageBox.dialogRect0.get_width() - 10f - MessageBox.w_idthOKCancelBtn, MessageBox.dialogRect0.get_height() - 30f, MessageBox.w_idthOKCancelBtn, MessageBox.h_OkCancelBtn);
		MessageBox._dlgRes = MessageBox.DialogResult.None;
		GUI.Label(new Rect(10f, 20f, 200f, 20f), MessageBox._strPrompt);
		MessageBox._strRes = GUI.TextField(new Rect(10f, 40f, MessageBox.dialogRect0.get_width() - 20f, 20f), MessageBox._strRes);
		if (GUI.Button(rect, "OK"))
		{
			MessageBox.dialogCallback.Invoke(MessageBox.DialogResult.OK, MessageBox._strRes);
		}
		else if (GUI.Button(rect1, "Cancel"))
		{
			MessageBox.dialogCallback.Invoke(MessageBox.DialogResult.Cancel, MessageBox._strRes);
		}
		GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));
	}

	private static void DoShowOk(int windowID)
	{
		Rect rect = new Rect(MessageBox.dialogRect2.get_height() / 2f + MessageBox.w_idthOKCancelBtn / 2f, MessageBox.dialogRect2.get_height() - 30f, MessageBox.w_idthOKCancelBtn, MessageBox.h_OkCancelBtn);
		MessageBox._dlgRes = MessageBox.DialogResult.None;
		GUI.Label(new Rect(30f, 40f, 200f, 20f), MessageBox._strPrompt);
		if (GUI.Button(rect, "OK"))
		{
			MessageBox.dialogCallback.Invoke(MessageBox.DialogResult.OK, MessageBox._strRes);
			MessageBox.isOnScreen = false;
		}
		GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));
	}

	private static void DoShowOkCancel(int windowID)
	{
		Rect rect = new Rect(10f, MessageBox.dialogRect1.get_height() - 30f, MessageBox.w_idthOKCancelBtn, MessageBox.h_OkCancelBtn);
		Rect rect1 = new Rect(MessageBox.dialogRect1.get_width() - 10f - MessageBox.w_idthOKCancelBtn, MessageBox.dialogRect1.get_height() - 30f, MessageBox.w_idthOKCancelBtn, MessageBox.h_OkCancelBtn);
		MessageBox._dlgRes = MessageBox.DialogResult.None;
		GUI.Label(new Rect(30f, 40f, 200f, 20f), MessageBox._strPrompt);
		if (GUI.Button(rect, "OK"))
		{
			MessageBox.dialogCallback.Invoke(MessageBox.DialogResult.OK, MessageBox._strRes);
			MessageBox.isOnScreen = false;
		}
		else if (GUI.Button(rect1, "Cancel"))
		{
			MessageBox.dialogCallback.Invoke(MessageBox.DialogResult.Cancel, MessageBox._strRes);
			MessageBox.isOnScreen = false;
		}
		GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));
	}

	public static MessageBox.DialogResult InputBox(string title, string promptText, string val)
	{
		MessageBox._title = title;
		MessageBox.dialogRect0 = new Rect((float)(Screen.get_width() / 2) - MessageBox.x_OkCancelBtn - MessageBox.w_idthOKCancelBtn, (float)(Screen.get_height() / 2) - MessageBox.y_OkCancelBtn, 2f * MessageBox.x_OkCancelBtn + 2f * MessageBox.w_idthOKCancelBtn, MessageBox.y_OkCancelBtn * 2f);
		MessageBox._dlgRes = MessageBox.DialogResult.None;
		MessageBox._strRes = val;
		MessageBox._strPrompt = promptText;
		MessageBox.isOnScreen = true;
		MessageBox.winID = 0;
		return MessageBox._dlgRes;
	}

	public static void OnGui()
	{
		if (!MessageBox.isOnScreen)
		{
			return;
		}
		switch (MessageBox.winID)
		{
			case 0:
			{
				MessageBox.dialogRect0 = GUI.Window(0, MessageBox.dialogRect0, new GUI.WindowFunction(null, MessageBox.DoDialogResult), MessageBox._title);
				break;
			}
			case 1:
			{
				MessageBox.dialogRect1 = GUI.Window(1, MessageBox.dialogRect1, new GUI.WindowFunction(null, MessageBox.DoShowOkCancel), MessageBox._title);
				break;
			}
			case 2:
			{
				MessageBox.dialogRect2 = GUI.Window(2, MessageBox.dialogRect2, new GUI.WindowFunction(null, MessageBox.DoShowOk), MessageBox._title);
				break;
			}
		}
	}

	public static MessageBox.DialogResult Show(string text, string caption)
	{
		MessageBox._title = text;
		MessageBox._strPrompt = caption;
		MessageBox.dialogRect2 = new Rect((float)(Screen.get_width() / 2) - MessageBox.x_OkCancelBtn - MessageBox.w_idthOKCancelBtn, (float)(Screen.get_height() / 2) - MessageBox.y_OkCancelBtn, 2f * MessageBox.x_OkCancelBtn + 2f * MessageBox.w_idthOKCancelBtn, MessageBox.y_OkCancelBtn * 2f);
		MessageBox.dialogCallback = new Action<MessageBox.DialogResult, string>(null, MessageBox.CallbackSimpleShow);
		MessageBox._dlgRes = MessageBox.DialogResult.None;
		MessageBox.isOnScreen = true;
		MessageBox.winID = 2;
		return MessageBox._dlgRes;
	}

	public static MessageBox.DialogResult Show(string text, string caption, MessageBox.MessageBoxButtons buttons)
	{
		MessageBox._title = text;
		MessageBox._strPrompt = caption;
		MessageBox.dialogRect1 = new Rect((float)(Screen.get_width() / 2) - MessageBox.x_OkCancelBtn - MessageBox.w_idthOKCancelBtn, (float)(Screen.get_height() / 2) - MessageBox.y_OkCancelBtn, 2f * MessageBox.x_OkCancelBtn + 2f * MessageBox.w_idthOKCancelBtn, MessageBox.y_OkCancelBtn * 2f);
		MessageBox._dlgRes = MessageBox.DialogResult.None;
		MessageBox.isOnScreen = true;
		MessageBox.winID = 1;
		return MessageBox._dlgRes;
	}

	public enum DialogResult
	{
		None,
		OK,
		Cancel,
		Abort,
		Retry,
		Ignore,
		Yes,
		No
	}

	public enum MessageBoxButtons
	{
		OK,
		OKCancel,
		AbortRetryIgnore,
		YesNoCancel,
		YesNo,
		RetryCancel
	}
}