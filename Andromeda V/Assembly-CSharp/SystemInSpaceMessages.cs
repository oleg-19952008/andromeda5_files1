using System;
using System.Collections.Generic;
using UnityEngine;

public class SystemInSpaceMessages
{
	private static int LifeTime;

	private static int MessageInterval;

	public GameObjectPhysics gop;

	public DateTime lastMessageTime;

	public List<SpaceLabelItem> labels = new List<SpaceLabelItem>();

	static SystemInSpaceMessages()
	{
		SystemInSpaceMessages.LifeTime = 4000;
		SystemInSpaceMessages.MessageInterval = 1200;
	}

	public SystemInSpaceMessages()
	{
	}

	public void AddLabel(Color color, string text)
	{
		List<SpaceLabelItem> list = this.labels;
		SpaceLabelItem spaceLabelItem = new SpaceLabelItem()
		{
			color = color,
			text = text,
			requestTime = DateTime.get_Now()
		};
		list.Add(spaceLabelItem);
	}

	public void AddLabel(Color color, string text, int textSize)
	{
		List<SpaceLabelItem> list = this.labels;
		SpaceLabelItem spaceLabelItem = new SpaceLabelItem()
		{
			color = color,
			text = text,
			fontSize = textSize,
			requestTime = DateTime.get_Now()
		};
		list.Add(spaceLabelItem);
	}

	private void CreateLabel(SpaceLabelItem item)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("SpaceLbl"));
		Transform _transform = gameObject.get_transform();
		Vector3 _position = Camera.get_main().get_transform().get_position();
		float single = _position.x;
		Vector3 vector3 = Camera.get_main().get_transform().get_position();
		Vector3 _position1 = Camera.get_main().get_transform().get_position();
		_transform.set_position(new Vector3(single, vector3.y - 25f, _position1.z + 14f));
		Renderer component = gameObject.GetComponent<MeshRenderer>();
		item.theLabel = gameObject;
		TextMesh textMesh = gameObject.GetComponent<TextMesh>();
		component.get_material().set_color(item.color);
		textMesh.set_text(item.text);
		textMesh.set_fontSize(item.fontSize);
	}

	public void Updating()
	{
		DateTime now = DateTime.get_Now();
		for (int i = 0; i < this.labels.get_Count(); i++)
		{
			SpaceLabelItem item = this.labels.get_Item(i);
			if (item.theLabel == null)
			{
				if (this.lastMessageTime.AddMilliseconds((double)SystemInSpaceMessages.MessageInterval) < now)
				{
					this.CreateLabel(item);
					this.lastMessageTime = now;
					item.startTime = now;
				}
			}
			else if (item.startTime.AddMilliseconds((double)SystemInSpaceMessages.LifeTime) <= now)
			{
				Object.Destroy(item.theLabel);
				this.labels.RemoveAt(i);
				i--;
			}
			else
			{
				TimeSpan timeSpan = now - item.startTime;
				float totalSeconds = (float)timeSpan.get_TotalSeconds() * 1.2f;
				Transform _transform = item.theLabel.get_transform();
				Vector3 _position = Camera.get_main().get_transform().get_position();
				float single = _position.x;
				Vector3 vector3 = Camera.get_main().get_transform().get_position();
				Vector3 _position1 = Camera.get_main().get_transform().get_position();
				_transform.set_position(new Vector3(single, vector3.y - 25f, _position1.z + 14f + totalSeconds));
				if ((double)totalSeconds > 2.8)
				{
					item.theLabel.get_renderer().get_material().set_color(new Color(item.color.r, item.color.g, item.color.b, (100f - 20f * totalSeconds) / 100f));
				}
			}
		}
	}
}