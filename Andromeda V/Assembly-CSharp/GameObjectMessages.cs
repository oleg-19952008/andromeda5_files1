using System;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectMessages
{
	public GameObjectPhysics gop;

	public DateTime lastMessageTime;

	public List<SpaceLabelItem> labels = new List<SpaceLabelItem>();

	public GameObjectMessages()
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
		gameObject.get_transform().set_position(new Vector3(this.gop.x, 1f, this.gop.z + 1.5f));
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
				if (this.lastMessageTime.AddMilliseconds(150) < now)
				{
					this.CreateLabel(item);
					this.lastMessageTime = now;
					item.startTime = now;
				}
			}
			else if (item.startTime.AddMilliseconds(1500) <= now)
			{
				Object.Destroy(item.theLabel);
				this.labels.RemoveAt(i);
				i--;
			}
			else
			{
				TimeSpan timeSpan = now - item.startTime;
				float totalSeconds = (float)timeSpan.get_TotalSeconds() * 3.5f;
				item.theLabel.get_transform().set_position(new Vector3(this.gop.x, 2f, this.gop.z + 1.5f + totalSeconds));
				if (totalSeconds > 3.5f)
				{
					item.theLabel.get_renderer().get_material().set_color(new Color(item.color.r, item.color.g, item.color.b, (100f - 15f * totalSeconds) / 100f));
				}
			}
		}
	}
}