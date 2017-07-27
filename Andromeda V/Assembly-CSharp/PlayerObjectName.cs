using System;
using UnityEngine;

public class PlayerObjectName
{
	public PlayerObjectPhysics pop;

	public SpaceLabelItem name;

	public PlayerObjectName()
	{
	}

	public void AddName(Color color, string text)
	{
		SpaceLabelItem spaceLabelItem = new SpaceLabelItem()
		{
			color = color,
			text = text,
			requestTime = DateTime.get_Now()
		};
		this.name = spaceLabelItem;
	}

	private void CreateLabel(SpaceLabelItem item)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("OutlineSpaceLbl"));
		gameObject.get_transform().set_position(new Vector3(this.pop.x, 2f, this.pop.z));
		gameObject.GetComponent<MeshRenderer>();
		item.theLabel = gameObject;
		EasyFontTextMesh component = gameObject.GetComponent<EasyFontTextMesh>();
		component.Text = item.text;
		component.FontColorTop = item.color;
		component.FontColorBottom = item.color;
	}

	public void Destroy()
	{
		Object.Destroy(this.name.theLabel);
		this.name = null;
		this.pop = null;
	}

	public void RemoveName()
	{
		if (this.name.theLabel != null)
		{
			Object.Destroy(this.name.theLabel);
			this.name = null;
			this.pop = null;
		}
	}

	public void Updating()
	{
		if (this.name.theLabel == null)
		{
			this.CreateLabel(this.name);
		}
		if (!this.pop.isAlive || !this.pop.get_IsPve() && this.pop.playerData.state == 80 || this.pop.isRemoved)
		{
			Object.Destroy(this.name.theLabel);
			this.name = null;
		}
		else
		{
			this.name.theLabel.get_transform().set_position(new Vector3(this.pop.x, 0f, this.pop.z + 3.6f));
		}
	}
}