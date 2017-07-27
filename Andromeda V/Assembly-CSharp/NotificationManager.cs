using System;
using System.Collections.Generic;

public static class NotificationManager
{
	private static DateTime lastNotificationTime;

	private static bool notificationOnScreen;

	private static List<Notification> notifications;

	private static bool isPaused;

	static NotificationManager()
	{
		NotificationManager.notificationOnScreen = false;
		NotificationManager.notifications = new List<Notification>();
		NotificationManager.isPaused = false;
	}

	public static void AddLevelUpNotification(int playerLevel)
	{
		List<Notification> list = NotificationManager.notifications;
		Notification notification = new Notification()
		{
			requestTime = StaticData.now,
			level = (byte)playerLevel,
			notificationType = 3
		};
		list.Add(notification);
	}

	public static void AddNotification(string theTitle, string theDescription, byte theLevel, string theAssetName, Action<EventHandlerParam> action)
	{
		List<Notification> list = NotificationManager.notifications;
		Notification notification = new Notification()
		{
			title = theTitle,
			text = theDescription,
			level = theLevel,
			assetName = theAssetName,
			requestTime = StaticData.now,
			onClickAction = action,
			notificationType = 1
		};
		list.Add(notification);
	}

	public static void AddNovaNotification(int novaAmount)
	{
		List<Notification> list = NotificationManager.notifications;
		Notification notification = new Notification()
		{
			requestTime = StaticData.now,
			title = StaticData.Translate("key_social_reward_title"),
			text = string.Format(StaticData.Translate("key_social_reward"), novaAmount),
			amount = novaAmount,
			assetName = "Aristocrate",
			notificationType = 5
		};
		list.Add(notification);
	}

	public static void AddObjectiveDoneNotification(NewQuestObjective obj)
	{
		List<Notification> list = NotificationManager.notifications;
		Notification notification = new Notification()
		{
			requestTime = StaticData.now,
			questObjective = obj,
			notificationType = 4
		};
		list.Add(notification);
	}

	public static void AddSystemNotification(string title, string info, string assetName)
	{
		List<Notification> list = NotificationManager.notifications;
		Notification notification = new Notification()
		{
			requestTime = StaticData.now,
			title = title,
			text = info,
			assetName = assetName,
			notificationType = 6
		};
		list.Add(notification);
	}

	public static void AddTransformerReward(ushort rewardType, int rewardAmount)
	{
		List<Notification> list = NotificationManager.notifications;
		Notification notification = new Notification()
		{
			requestTime = StaticData.now,
			itemType = rewardType,
			amount = rewardAmount,
			notificationType = 2
		};
		list.Add(notification);
	}

	private static void CreateWindow(Notification item)
	{
		switch (item.notificationType)
		{
			case 1:
			{
				item.theWindow = NotificationWindow.CreateNotificationWindow(item.title, item.text, item.level, item.assetName, new Action<EventHandlerParam>(null, NotificationManager.OnNotificationClick), item, playWebGame.GAME_TYPE != "ru");
				break;
			}
			case 2:
			{
				item.theWindow = NotificationWindow.CreateTransformerNotification(item.itemType, item.amount, new Action<EventHandlerParam>(null, NotificationManager.OnNotificationClick), item);
				break;
			}
			case 3:
			{
				item.theWindow = NotificationWindow.CreateLevelUpNotification((int)item.level, new Action<EventHandlerParam>(null, NotificationManager.OnNotificationClick), item);
				break;
			}
			case 4:
			{
				item.theWindow = NotificationWindow.CreateObjectiveDoneNotification(item.questObjective, new Action<EventHandlerParam>(null, NotificationManager.OnNotificationClick), item);
				break;
			}
			case 5:
			{
				item.theWindow = NotificationWindow.CreateNotificationWindow(item.title, item.text, 0, item.assetName, null, null, false);
				break;
			}
			case 6:
			{
				item.theWindow = NotificationWindow.CreateSystemNotification(item.title, item.text, item.assetName, new Action<EventHandlerParam>(null, NotificationManager.OnNotificationClick), item);
				break;
			}
			default:
			{
				return;
			}
		}
		AndromedaGui.gui.AddWindow(item.theWindow);
		item.theWindow.StartMoveBy(0f, -196f, 0.3f, true);
	}

	private static void OnNotificationClick(EventHandlerParam p)
	{
		Notification notification = (Notification)p.customData;
		if (notification.notificationType != 6)
		{
			notification.startTime = StaticData.now.AddMilliseconds((double)(-NotificationWindow.NOTIFICATION_TIME_ON_SCREEN));
		}
		else
		{
			notification.startTime = StaticData.now.AddMilliseconds((double)(-NotificationWindow.SYSTEM_NOTIFICATION_TIME_ON_SCREEN));
		}
		NotificationManager.lastNotificationTime = StaticData.now;
	}

	private static void OpenLevelUpScreen(EventHandlerParam p)
	{
		NetworkScript.player.shipScript.OpenMainMenuWondow(2);
	}

	public static void Pause()
	{
		NotificationManager.isPaused = true;
	}

	public static void Start()
	{
		NotificationManager.isPaused = false;
		NotificationManager.lastNotificationTime = StaticData.now;
		foreach (Notification notification in NotificationManager.notifications)
		{
			if (notification.theWindow != null)
			{
				AndromedaGui.gui.RemoveWindow(notification.theWindow.handler);
			}
			notification.requestTime = StaticData.now;
			notification.theWindow = null;
		}
		NotificationManager.notificationOnScreen = false;
	}

	public static void Updating()
	{
		if (NotificationManager.isPaused)
		{
			return;
		}
		DateTime dateTime = StaticData.now;
		for (int i = 0; i < NotificationManager.notifications.get_Count(); i++)
		{
			Notification item = NotificationManager.notifications.get_Item(i);
			if (item.theWindow == null)
			{
				if (!NotificationManager.notificationOnScreen && NotificationManager.lastNotificationTime.AddMilliseconds(500) < dateTime)
				{
					NotificationManager.CreateWindow(item);
					item.startTime = dateTime;
					NotificationManager.notificationOnScreen = true;
				}
			}
			else if ((item.notificationType != 6 ? item.startTime.AddMilliseconds((double)NotificationWindow.NOTIFICATION_TIME_ON_SCREEN) < dateTime : item.startTime.AddMilliseconds((double)NotificationWindow.SYSTEM_NOTIFICATION_TIME_ON_SCREEN) < dateTime))
			{
				NotificationManager.lastNotificationTime = dateTime;
				NotificationManager.notificationOnScreen = false;
				AndromedaGui.gui.RemoveWindow(item.theWindow.handler);
				NotificationManager.notifications.RemoveAt(i);
				i--;
			}
		}
	}
}