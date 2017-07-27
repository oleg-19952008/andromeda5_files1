using System;
using UnityEngine;

public class MovieEventAddGameObject : MovieEvent
{
	public string prefabName;

	public string gameObjectName;

	public Vector3 position;

	public Vector3 rotation;

	public Action<GameObject> CreateCallback;

	public MovieEventAddGameObject()
	{
	}

	public override void Execute()
	{
		Object prefab = playWebGame.assets.GetPrefab(this.prefabName);
		GameObject gameObject = (GameObject)Object.Instantiate(prefab);
		gameObject.set_name(this.gameObjectName);
		gameObject.get_transform().set_position(this.position);
		gameObject.get_transform().set_eulerAngles(this.rotation);
		if (this.CreateCallback != null)
		{
			this.CreateCallback.Invoke(gameObject);
		}
	}
}