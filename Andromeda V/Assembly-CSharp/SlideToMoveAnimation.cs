using System;
using UnityEngine;

public class SlideToMoveAnimation : MonoBehaviour
{
	public float countDown = 2f;

	public GameObject theShip;

	public Vector3 toLookAt;

	private __TutorialScript tutorialScript;

	public SlideToMoveAnimation()
	{
	}

	private void OnDestroy()
	{
		if (this.tutorialScript != null)
		{
			this.tutorialScript.HideBoostHint();
		}
	}

	private void Start()
	{
		if (Application.get_loadedLevelName().StartsWith("Tutorial"))
		{
			this.tutorialScript = GameObject.Find("TheTutorialScript").GetComponent<__TutorialScript>();
		}
		if (this.tutorialScript != null)
		{
			this.tutorialScript.ShowBoostHint();
		}
	}

	private void Update()
	{
		if (this.theShip == null)
		{
			if (this.tutorialScript != null)
			{
				this.tutorialScript.HideBoostHint();
			}
			Object.Destroy(base.get_gameObject());
			return;
		}
		SlideToMoveAnimation _deltaTime = this;
		_deltaTime.countDown = _deltaTime.countDown - Time.get_deltaTime();
		if (this.countDown <= 0f)
		{
			if (this.tutorialScript != null)
			{
				this.tutorialScript.HideBoostHint();
			}
			Object.Destroy(base.get_gameObject());
		}
		base.get_gameObject().get_transform().set_position(this.theShip.get_transform().get_position());
	}
}