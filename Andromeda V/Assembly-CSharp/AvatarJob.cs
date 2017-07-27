using System;
using UnityEngine;

public class AvatarJob
{
	public string key;

	public WWW job;

	public Action<AvatarJob> callback;

	public object token;

	public AvatarJob()
	{
	}
}