using System;
using UnityEngine;

public class CameraInBase : MonoBehaviour
{
	public CameraInBase()
	{
	}

	private void Start()
	{
		if (QualitySettings.GetQualityLevel() < 5)
		{
			base.get_gameObject().GetComponent<Vignetting>().enabled = false;
			base.get_gameObject().GetComponent<SSAOEffect>().enabled = false;
			base.get_gameObject().GetComponent<Bloom>().enabled = false;
		}
	}

	private void Update()
	{
	}
}