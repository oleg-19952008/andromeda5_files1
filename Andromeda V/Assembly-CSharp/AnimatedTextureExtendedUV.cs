using System;
using UnityEngine;

public class AnimatedTextureExtendedUV : MonoBehaviour
{
	public int colCount = 4;

	public int rowCount = 4;

	public int rowNumber;

	public int colNumber;

	public int totalCells = 4;

	public int fps = 10;

	private Vector2 offset;

	public AnimatedTextureExtendedUV()
	{
	}

	private void SetSpriteAnimation(int colCount, int rowCount, int rowNumber, int colNumber, int totalCells, int fps)
	{
		int _time = (int)(Time.get_time() * (float)fps);
		_time = _time % totalCells;
		float single = 1f / (float)colCount;
		Vector2 vector2 = new Vector2(single, 1f / (float)rowCount);
		int num = _time % colCount;
		int num1 = _time / colCount;
		float single1 = (float)(num + colNumber) * vector2.x;
		float single2 = 1f - vector2.y - (float)(num1 + rowNumber) * vector2.y;
		Vector2 vector21 = new Vector2(single1, single2);
		base.get_renderer().get_material().SetTextureOffset("_MainTex", vector21);
		base.get_renderer().get_material().SetTextureScale("_MainTex", vector2);
	}

	private void Update()
	{
		this.SetSpriteAnimation(this.colCount, this.rowCount, this.rowNumber, this.colNumber, this.totalCells, this.fps);
	}
}