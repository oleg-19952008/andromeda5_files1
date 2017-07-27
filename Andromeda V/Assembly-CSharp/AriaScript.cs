using System;
using UnityEngine;

public class AriaScript : MonoBehaviour
{
	public AriaScript()
	{
	}

	private void OnDestroy()
	{
		__TutorialScript _TutorialScript = (__TutorialScript)Object.FindObjectOfType(typeof(__TutorialScript));
		if (_TutorialScript != null)
		{
			_TutorialScript.isAriaDied = true;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}