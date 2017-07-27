using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpacaLabelManager
{
	private SortedList<uint, GameObjectMessages> collection = new SortedList<uint, GameObjectMessages>();

	private SystemInSpaceMessages systemMsg = new SystemInSpaceMessages();

	public SpacaLabelManager()
	{
	}

	public void AddMessage(Color color, string text, GameObjectPhysics gop)
	{
		GameObjectMessages item = null;
		if (this.collection.ContainsKey(gop.neighbourhoodId))
		{
			item = this.collection.get_Item(gop.neighbourhoodId);
		}
		else
		{
			item = new GameObjectMessages()
			{
				gop = gop
			};
			this.collection.Add(gop.neighbourhoodId, item);
		}
		item.AddLabel(color, text);
	}

	public void AddMessage(Color color, string text, GameObjectPhysics gop, int fontSize)
	{
		GameObjectMessages item = null;
		if (this.collection.ContainsKey(gop.neighbourhoodId))
		{
			item = this.collection.get_Item(gop.neighbourhoodId);
		}
		else
		{
			item = new GameObjectMessages()
			{
				gop = gop
			};
			this.collection.Add(gop.neighbourhoodId, item);
		}
		item.AddLabel(color, text, fontSize);
	}

	public void AddNewMessage(Color color, string text, GameObjectPhysics gop, int fontSize)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("NewSpaceLbl"));
		gameObject.get_transform().set_position(new Vector3(gop.x, 3f, gop.z));
		gameObject.GetComponent<SpaceLblScript>().fontSize = (float)fontSize;
		Renderer component = gameObject.GetComponent<MeshRenderer>();
		TextMesh textMesh = gameObject.GetComponent<TextMesh>();
		component.get_material().set_color(color);
		textMesh.set_text(text);
		gameObject.get_rigidbody().AddForce((float)(Random.Range(-8, 8) * 100), (float)(Random.Range(2, 10) * 50), (float)(Random.Range(-6, 6) * 100));
		textMesh.set_fontSize(fontSize);
	}

	public void AddSystemMessage(Color color, string text, int fontSize)
	{
		this.systemMsg.AddLabel(color, text, fontSize);
	}

	public void Updating()
	{
		int num = 0;
		GameObjectMessages[] array = Enumerable.ToArray<GameObjectMessages>(this.collection.get_Values());
		while (num < this.collection.get_Count())
		{
			GameObjectMessages gameObjectMessage = array[num];
			gameObjectMessage.Updating();
			if (gameObjectMessage.labels.get_Count() < 1)
			{
				this.collection.Remove(gameObjectMessage.gop.neighbourhoodId);
				array = Enumerable.ToArray<GameObjectMessages>(this.collection.get_Values());
				num--;
			}
			num++;
		}
		if (this.systemMsg.labels.get_Count() > 0)
		{
			this.systemMsg.Updating();
		}
	}
}