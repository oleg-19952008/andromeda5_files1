using System;
using UnityEngine;

[ExecuteInEditMode]
public class RotatableGuiItem : MonoBehaviour
{
	public Texture2D texture;

	public float angle;

	public Vector2 size = new Vector2(20f, 20f);

	private Vector2 pos = new Vector2(0f, 0f);

	private Rect rect;

	private Vector2 pivot;

	public RotatableGuiItem()
	{
	}

	private void OnGUI()
	{
		if (Application.get_isEditor())
		{
			this.UpdateSettings();
		}
		Matrix4x4 _matrix = GUI.get_matrix();
		GUIUtility.RotateAroundPivot(this.angle, this.pivot);
		GUI.DrawTexture(this.rect, this.texture);
		GUI.set_matrix(_matrix);
	}

	private void Start()
	{
		this.UpdateSettings();
	}

	private void UpdateSettings()
	{
		float _localPosition = base.get_transform().get_localPosition().x;
		Vector3 vector3 = base.get_transform().get_localPosition();
		this.pos = new Vector2(_localPosition, vector3.y);
		this.rect = new Rect(this.pos.x - this.size.x * 0.5f, this.pos.y - this.size.y * 0.5f, this.size.x, this.size.y);
		this.pivot = new Vector2(this.rect.get_xMin() + this.rect.get_width() * 0.5f, this.rect.get_yMin() + this.rect.get_height() * 0.5f);
	}
}