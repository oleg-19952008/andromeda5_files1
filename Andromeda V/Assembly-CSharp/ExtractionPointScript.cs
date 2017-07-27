using System;
using UnityEngine;

public class ExtractionPointScript : MonoBehaviour
{
	public ExtractionPoint extractionPoint;

	public ExtractionPointScript()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (this.extractionPoint == null)
		{
			return;
		}
		if (this.extractionPoint.isRemoved)
		{
			return;
		}
		float _deltaTime = Time.get_deltaTime();
		float single = 0f;
		float single1 = 0f;
		float single2 = 0f;
		this.extractionPoint.CalculateObjectMovement(_deltaTime, ref single, ref single1, ref single2);
		ExtractionPoint extractionPoint = this.extractionPoint;
		extractionPoint.x = extractionPoint.x + single;
		ExtractionPoint extractionPoint1 = this.extractionPoint;
		extractionPoint1.y = extractionPoint1.y + single1;
		ExtractionPoint extractionPoint2 = this.extractionPoint;
		extractionPoint2.z = extractionPoint2.z + single2;
		NetworkScript.ApplyPhysicsToGameObject(this.extractionPoint, base.get_gameObject());
	}
}