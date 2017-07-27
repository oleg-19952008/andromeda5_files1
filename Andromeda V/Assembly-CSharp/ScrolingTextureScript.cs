using System;
using UnityEngine;

public class ScrolingTextureScript : MonoBehaviour
{
	public float offsetXSpeed;

	public float offsetYSpeed;

	public Material scrollingMaterial;

	public ScrolingTextureScript()
	{
	}

	private void Update()
	{
		if (this.scrollingMaterial != null && this.offsetXSpeed != 0f || this.offsetYSpeed != 0f)
		{
			this.scrollingMaterial.set_mainTextureOffset(this.scrollingMaterial.get_mainTextureOffset() + new Vector2(this.offsetXSpeed * Time.get_deltaTime(), this.offsetYSpeed * Time.get_deltaTime()));
			if (this.scrollingMaterial.get_mainTextureOffset().x > 1f)
			{
				Material material = this.scrollingMaterial;
				Vector2 _mainTextureOffset = this.scrollingMaterial.get_mainTextureOffset();
				material.set_mainTextureOffset(new Vector2(0f, _mainTextureOffset.y));
			}
			if (this.scrollingMaterial.get_mainTextureOffset().y > 1f)
			{
				Material material1 = this.scrollingMaterial;
				Vector2 vector2 = this.scrollingMaterial.get_mainTextureOffset();
				material1.set_mainTextureOffset(new Vector2(vector2.x, 0f));
			}
		}
	}
}