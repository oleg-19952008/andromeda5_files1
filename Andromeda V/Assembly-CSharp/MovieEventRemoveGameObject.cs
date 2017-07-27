using System;
using UnityEngine;

public class MovieEventRemoveGameObject : MovieEvent
{
	public string gameObjectName;

	public Action<GameObject> RemoveCallback;

	public MovieEventRemoveGameObject()
	{
	}

	public override void Execute()
	{
		GameObject gameObject = GameObject.Find(this.gameObjectName);
		Object.Destroy(gameObject);
		if (this.RemoveCallback != null)
		{
			this.RemoveCallback.Invoke(gameObject);
		}
	}
}