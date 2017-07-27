using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class EasyFontTextMesh : MonoBehaviour
{
	[HideInInspector]
	public EasyFontTextMesh.TextProperties _privateProperties;

	public bool updateAlwaysOnEnable;

	public bool dontOverrideMaterials;

	private Mesh textMesh;

	private MeshFilter textMeshFilter;

	private Material fontMaterial;

	private Renderer textRenderer;

	private char[] textChars;

	private bool isDirty;

	private int currentLineBreak;

	private float heightSum;

	private List<int> lineBreakCharCounter = new List<int>();

	private List<float> lineBreakAccumulatedDistance = new List<float>();

	private Vector3[] vertices;

	private int[] triangles;

	private Vector2[] uv;

	private Vector2[] uv2;

	private Color[] colors;

	[HideInInspector]
	public bool GUIChanged;

	private char LINE_BREAK = Convert.ToChar(10);

	public Material CustomFillMaterial
	{
		get
		{
			return this._privateProperties.customFillMaterial;
		}
		set
		{
			this._privateProperties.customFillMaterial = value;
			this.isDirty = true;
		}
	}

	public bool EnableOutline
	{
		get
		{
			return this._privateProperties.enableOutline;
		}
		set
		{
			this._privateProperties.enableOutline = value;
			this.isDirty = true;
		}
	}

	public bool EnableShadow
	{
		get
		{
			return this._privateProperties.enableShadow;
		}
		set
		{
			this._privateProperties.enableShadow = value;
			this.isDirty = true;
		}
	}

	public Color FontColorBottom
	{
		get
		{
			return this._privateProperties.fontColorBottom;
		}
		set
		{
			this._privateProperties.fontColorBottom = value;
			this.SetColor(this._privateProperties.fontColorTop, this._privateProperties.fontColorBottom);
		}
	}

	public Color FontColorTop
	{
		get
		{
			return this._privateProperties.fontColorTop;
		}
		set
		{
			this._privateProperties.fontColorTop = value;
			this.SetColor(this._privateProperties.fontColorTop, this._privateProperties.fontColorBottom);
		}
	}

	public int FontSize
	{
		get
		{
			return this._privateProperties.fontSize;
		}
		set
		{
			this._privateProperties.fontSize = value;
			this.isDirty = true;
		}
	}

	public Font FontType
	{
		get
		{
			return this._privateProperties.font;
		}
		set
		{
			this._privateProperties.font = value;
			this.ChangeFont();
		}
	}

	public bool HighQualityOutline
	{
		get
		{
			return this._privateProperties.highQualityOutline;
		}
		set
		{
			this._privateProperties.highQualityOutline = value;
			this.isDirty = true;
		}
	}

	public float LineSpacing
	{
		get
		{
			return this._privateProperties.lineSpacing;
		}
		set
		{
			this._privateProperties.lineSpacing = value;
			this.isDirty = true;
		}
	}

	public Color OutlineColor
	{
		get
		{
			return this._privateProperties.outlineColor;
		}
		set
		{
			this._privateProperties.outlineColor = value;
			this.SetOutlineColor(this._privateProperties.outlineColor);
		}
	}

	public float OutLineWidth
	{
		get
		{
			return this._privateProperties.outLineWidth;
		}
		set
		{
			this._privateProperties.outLineWidth = value;
			this.isDirty = true;
		}
	}

	public Color ShadowColor
	{
		get
		{
			return this._privateProperties.shadowColor;
		}
		set
		{
			this._privateProperties.shadowColor = value;
			this.SetShadowColor(this._privateProperties.shadowColor);
		}
	}

	public Vector3 ShadowDistance
	{
		get
		{
			return this._privateProperties.shadowDistance;
		}
		set
		{
			this._privateProperties.shadowDistance = value;
			this.isDirty = true;
		}
	}

	public float Size
	{
		get
		{
			return this._privateProperties.size;
		}
		set
		{
			this._privateProperties.size = value;
			this.isDirty = true;
		}
	}

	public string Text
	{
		get
		{
			return this._privateProperties.text;
		}
		set
		{
			this._privateProperties.text = value;
			this.isDirty = true;
		}
	}

	public EasyFontTextMesh.TEXT_ALIGNMENT Textalignment
	{
		get
		{
			return this._privateProperties.textAlignment;
		}
		set
		{
			this._privateProperties.textAlignment = value;
			this.isDirty = true;
		}
	}

	public EasyFontTextMesh.TEXT_ANCHOR Textanchor
	{
		get
		{
			return this._privateProperties.textAnchor;
		}
		set
		{
			this._privateProperties.textAnchor = value;
			this.isDirty = true;
		}
	}

	public EasyFontTextMesh()
	{
	}

	private void AnalizeText()
	{
		bool flag = true;
	Label0:
		while (flag)
		{
			flag = false;
			int num = 0;
			while (num < (int)this.textChars.Length)
			{
				if (this.textChars[num] != '\\' || num + 1 >= (int)this.textChars.Length || this.textChars[num + 1] != 'n')
				{
					num++;
				}
				else
				{
					char[] lINEBREAK = new char[(int)this.textChars.Length - 1];
					int num1 = 0;
					for (int i = 0; i < (int)this.textChars.Length; i++)
					{
						if (i != num)
						{
							if (i == num + 1)
							{
								i++;
								if (i < (int)this.textChars.Length)
								{
									goto Label2;
								}
								goto Label1;
							}
						Label2:
							lINEBREAK[num1] = this.textChars[i];
							num1++;
						}
						else
						{
							lINEBREAK[num1] = this.LINE_BREAK;
							num1++;
						}
					Label1:
					}
					this.textChars = lINEBREAK;
					flag = true;
					goto Label0;
				}
			}
		}
	}

	private void Awake()
	{
		this.CacheTextVars();
		this.RefreshMesh(true);
	}

	public void CacheTextVars()
	{
		this.textMeshFilter = base.GetComponent<MeshFilter>();
		if (this.textMeshFilter == null)
		{
			this.textMeshFilter = base.get_gameObject().AddComponent<MeshFilter>();
		}
		this.textMesh = this.textMeshFilter.get_sharedMesh();
		if (this.textMesh == null)
		{
			this.textMesh = new Mesh();
			Mesh mesh = this.textMesh;
			string _name = base.get_gameObject().get_name();
			int instanceID = base.GetInstanceID();
			mesh.set_name(string.Concat(_name, instanceID.ToString()));
			this.textMeshFilter.set_sharedMesh(this.textMesh);
		}
		this.textRenderer = base.get_renderer();
		if (this.textRenderer == null)
		{
			this.textRenderer = base.get_gameObject().AddComponent<MeshRenderer>();
		}
		if (!this.dontOverrideMaterials)
		{
			if (this._privateProperties.customFillMaterial != null)
			{
				if (this._privateProperties.enableShadow || this._privateProperties.enableOutline)
				{
					if ((int)this.textRenderer.get_sharedMaterials().Length < 2)
					{
						this.textRenderer.set_sharedMaterials(new Material[] { this._privateProperties.font.get_material(), this._privateProperties.customFillMaterial });
					}
					this._privateProperties.customFillMaterial.set_mainTexture(this._privateProperties.font.get_material().get_mainTexture());
					this.textRenderer.set_sharedMaterial(this._privateProperties.font.get_material());
				}
				else
				{
					this._privateProperties.customFillMaterial.set_mainTexture(this._privateProperties.font.get_material().get_mainTexture());
					this.textRenderer.set_sharedMaterial(this._privateProperties.customFillMaterial);
				}
			}
			else if (this.textRenderer.get_sharedMaterials() != null)
			{
				this.textRenderer.set_sharedMaterials(new Material[] { this.textRenderer.get_sharedMaterial() });
			}
			else
			{
				this.textRenderer.set_sharedMaterials(new Material[] { this._privateProperties.font.get_material() });
			}
		}
	}

	private void ChangeFont()
	{
		if (!this.dontOverrideMaterials && this._privateProperties.customFillMaterial == null)
		{
			this.textRenderer.set_sharedMaterial(this._privateProperties.font.get_material());
		}
		this.isDirty = true;
	}

	private void CreateCharacter(char _character, int _arrayPosition, Vector3 _offset, Color _colorTop, Color _colorBottom)
	{
		if (this.lineBreakAccumulatedDistance.get_Count() == 0)
		{
			this.lineBreakAccumulatedDistance.Add(0f);
		}
		if (this.lineBreakCharCounter.get_Count() == 0)
		{
			this.lineBreakCharCounter.Add(0);
		}
		CharacterInfo characterInfo = new CharacterInfo();
		if (!this._privateProperties.font.GetCharacterInfo(_character, ref characterInfo, this._privateProperties.fontSize))
		{
			this.lineBreakCharCounter.Add(this.lineBreakCharCounter.get_Item(this.currentLineBreak));
			this.lineBreakAccumulatedDistance.Add(0f);
			EasyFontTextMesh easyFontTextMesh = this;
			easyFontTextMesh.currentLineBreak = easyFontTextMesh.currentLineBreak + 1;
			return;
		}
		List<int> list = this.lineBreakCharCounter;
		List<int> list1 = list;
		int num = this.currentLineBreak;
		list.set_Item(num, list1.get_Item(num) + 1);
		float single = this._privateProperties.size / (float)this._privateProperties.fontSize;
		_offset = _offset * (this._privateProperties.size * 0.1f);
		float _width = characterInfo.vert.get_width() * single;
		float _height = characterInfo.vert.get_height() * single;
		Vector2 vector2 = new Vector2(characterInfo.vert.get_x(), characterInfo.vert.get_y()) * single;
		if (_character != ' ')
		{
			EasyFontTextMesh _y = this;
			_y.heightSum = _y.heightSum + (characterInfo.vert.get_y() + characterInfo.vert.get_height() * 0.5f) * single;
		}
		Vector3 vector3 = new Vector3(this.lineBreakAccumulatedDistance.get_Item(this.currentLineBreak) * single, -this._privateProperties.size * (float)this.currentLineBreak * this._privateProperties.lineSpacing, 0f);
		if (!characterInfo.flipped)
		{
			this.vertices[4 * _arrayPosition] = (new Vector3(vector2.x + _width, _height + vector2.y, 0f) + _offset) + vector3;
			this.vertices[4 * _arrayPosition + 1] = (new Vector3(vector2.x, _height + vector2.y, 0f) + _offset) + vector3;
			this.vertices[4 * _arrayPosition + 2] = (new Vector3(vector2.x, vector2.y, 0f) + _offset) + vector3;
			this.vertices[4 * _arrayPosition + 3] = (new Vector3(vector2.x + _width, vector2.y, 0f) + _offset) + vector3;
		}
		else
		{
			this.vertices[4 * _arrayPosition] = (new Vector3(vector2.x + _width, _height + vector2.y, 0f) + _offset) + vector3;
			this.vertices[4 * _arrayPosition + 1] = (new Vector3(vector2.x, _height + vector2.y, 0f) + _offset) + vector3;
			this.vertices[4 * _arrayPosition + 2] = (new Vector3(vector2.x, vector2.y, 0f) + _offset) + vector3;
			this.vertices[4 * _arrayPosition + 3] = (new Vector3(vector2.x + _width, vector2.y, 0f) + _offset) + vector3;
		}
		List<float> list2 = this.lineBreakAccumulatedDistance;
		List<float> list3 = list2;
		int num1 = this.currentLineBreak;
		float item = list3.get_Item(num1);
		list2.set_Item(num1, item + characterInfo.width);
		this.triangles[6 * _arrayPosition] = _arrayPosition * 4;
		this.triangles[6 * _arrayPosition + 1] = _arrayPosition * 4 + 1;
		this.triangles[6 * _arrayPosition + 2] = _arrayPosition * 4 + 2;
		this.triangles[6 * _arrayPosition + 3] = _arrayPosition * 4;
		this.triangles[6 * _arrayPosition + 4] = _arrayPosition * 4 + 2;
		this.triangles[6 * _arrayPosition + 5] = _arrayPosition * 4 + 3;
		if (!characterInfo.flipped)
		{
			this.uv[4 * _arrayPosition] = new Vector2(characterInfo.uv.get_x() + characterInfo.uv.get_width(), characterInfo.uv.get_y());
			this.uv[4 * _arrayPosition + 1] = new Vector2(characterInfo.uv.get_x(), characterInfo.uv.get_y());
			this.uv[4 * _arrayPosition + 2] = new Vector2(characterInfo.uv.get_x(), characterInfo.uv.get_y() + characterInfo.uv.get_height());
			this.uv[4 * _arrayPosition + 3] = new Vector2(characterInfo.uv.get_x() + characterInfo.uv.get_width(), characterInfo.uv.get_y() + characterInfo.uv.get_height());
		}
		else
		{
			this.uv[4 * _arrayPosition] = new Vector2(characterInfo.uv.get_x(), characterInfo.uv.get_y() + characterInfo.uv.get_height());
			this.uv[4 * _arrayPosition + 1] = new Vector2(characterInfo.uv.get_x(), characterInfo.uv.get_y());
			this.uv[4 * _arrayPosition + 2] = new Vector2(characterInfo.uv.get_x() + characterInfo.uv.get_width(), characterInfo.uv.get_y());
			this.uv[4 * _arrayPosition + 3] = new Vector2(characterInfo.uv.get_x() + characterInfo.uv.get_width(), characterInfo.uv.get_y() + characterInfo.uv.get_height());
		}
		if (this._privateProperties.customFillMaterial != null)
		{
			Vector2 vector21 = new Vector2(_offset.x, _offset.y);
			Vector2 vector22 = new Vector2(vector3.x, vector3.y);
			this.uv2[4 * _arrayPosition] = (new Vector2(vector2.x + _width, _height + vector2.y) + vector21) + vector22;
			this.uv2[4 * _arrayPosition + 1] = (new Vector2(vector2.x, _height + vector2.y) + vector21) + vector22;
			this.uv2[4 * _arrayPosition + 2] = (new Vector2(vector2.x, vector2.y) + vector21) + vector22;
			this.uv2[4 * _arrayPosition + 3] = (new Vector2(vector2.x + _width, vector2.y) + vector21) + vector22;
		}
		this.colors[4 * _arrayPosition] = _colorBottom;
		this.colors[4 * _arrayPosition + 1] = _colorBottom;
		this.colors[4 * _arrayPosition + 2] = _colorTop;
		this.colors[4 * _arrayPosition + 3] = _colorTop;
	}

	private void FontTexureRebuild()
	{
		this.RefreshMesh(true);
	}

	private int GetFinalVertexToColorize(EasyFontTextMesh.TEXT_COMPONENT _textComponent)
	{
		if (this.textChars == null)
		{
			this.textChars = this._privateProperties.text.ToCharArray();
		}
		int length = 0;
		int num = 0;
		switch (_textComponent)
		{
			case EasyFontTextMesh.TEXT_COMPONENT.Main:
			{
				if (this._privateProperties.enableShadow && this._privateProperties.enableOutline)
				{
					num = 6;
				}
				else if (this._privateProperties.enableOutline)
				{
					num = 5;
				}
				else if (this._privateProperties.enableShadow)
				{
					num = 2;
				}
				length = (int)this.textChars.Length * 4 * num;
				break;
			}
			case EasyFontTextMesh.TEXT_COMPONENT.Shadow:
			{
				length = (int)this.textChars.Length * 4;
				break;
			}
			case EasyFontTextMesh.TEXT_COMPONENT.Outline:
			{
				num = (!this._privateProperties.enableShadow ? 0 : 1);
				length = (int)this.textChars.Length * 4 * (num + 4);
				break;
			}
		}
		return length;
	}

	private int GetInitialVertexToColorize(EasyFontTextMesh.TEXT_COMPONENT _textComponent)
	{
		if (this.textChars == null)
		{
			this.textChars = this._privateProperties.text.ToCharArray();
		}
		int num = 0;
		switch (_textComponent)
		{
			case EasyFontTextMesh.TEXT_COMPONENT.Main:
			{
				if (this._privateProperties.enableShadow && this._privateProperties.enableOutline)
				{
					num = 5;
				}
				else if (this._privateProperties.enableOutline)
				{
					num = 4;
				}
				else if (this._privateProperties.enableShadow)
				{
					num = 1;
				}
				break;
			}
			case EasyFontTextMesh.TEXT_COMPONENT.Shadow:
			{
				num = 0;
				break;
			}
			case EasyFontTextMesh.TEXT_COMPONENT.Outline:
			{
				num = (!this._privateProperties.enableShadow ? 0 : 1);
				break;
			}
		}
		return (int)this.textChars.Length * 4 * num;
	}

	public int GetVertexCount()
	{
		if (this.vertices == null)
		{
			return 0;
		}
		return (int)this.vertices.Length;
	}

	private void LateUpdate()
	{
		if (this.isDirty)
		{
			this.isDirty = false;
			this.RefreshMesh(true);
		}
	}

	private void OnDisable()
	{
		Font font = this._privateProperties.font;
		font.set_textureRebuildCallback((Font.FontTextureRebuildCallback)Delegate.Remove(font.get_textureRebuildCallback(), new Font.FontTextureRebuildCallback(this, EasyFontTextMesh.FontTexureRebuild)));
	}

	private void OnEnable()
	{
		Font font = this._privateProperties.font;
		font.set_textureRebuildCallback((Font.FontTextureRebuildCallback)Delegate.Combine(font.get_textureRebuildCallback(), new Font.FontTextureRebuildCallback(this, EasyFontTextMesh.FontTexureRebuild)));
		if (this.updateAlwaysOnEnable)
		{
			this.RefreshMesh(true);
		}
	}

	private void RefreshMesh(bool _updateTexureInfo)
	{
		if (_updateTexureInfo)
		{
			this._privateProperties.font.RequestCharactersInTexture(this._privateProperties.text, this._privateProperties.fontSize);
		}
		this.textChars = null;
		this.textChars = this._privateProperties.text.ToCharArray();
		this.AnalizeText();
		int num = 0;
		if (this._privateProperties.highQualityOutline)
		{
			num = 4;
		}
		int num1 = 1;
		if (this._privateProperties.enableShadow && this._privateProperties.enableOutline)
		{
			num1 = 6 + num;
		}
		else if (this._privateProperties.enableOutline)
		{
			num1 = 5 + num;
		}
		else if (this._privateProperties.enableShadow)
		{
			num1 = 2;
		}
		this.vertices = new Vector3[(int)this.textChars.Length * 4 * num1];
		this.triangles = new int[(int)this.textChars.Length * 6 * num1];
		this.uv = new Vector2[(int)this.textChars.Length * 4 * num1];
		this.uv2 = new Vector2[(int)this.textChars.Length * 4 * num1];
		this.colors = new Color[(int)this.textChars.Length * 4 * num1];
		int num2 = 0;
		int num3 = 0;
		if (this._privateProperties.enableShadow)
		{
			this.ResetHelperVariables();
			char[] chrArray = this.textChars;
			for (int i = 0; i < (int)chrArray.Length; i++)
			{
				char chr = chrArray[i];
				this.CreateCharacter(chr, num2, this._privateProperties.shadowDistance, this._privateProperties.shadowColor, this._privateProperties.shadowColor);
				num2++;
			}
			int num4 = num3;
			num3 = num4 + 1;
			this.SetAlignment(num4);
		}
		if (this._privateProperties.enableOutline)
		{
			float single = 90f;
			if (this._privateProperties.highQualityOutline)
			{
				single = 45f;
			}
			for (float j = 0f; j < 360f; j = j + single)
			{
				Vector3 _right = Vector3.get_right();
				_right.x = Mathf.Cos(j * 0.01745329f);
				_right.y = Mathf.Sin(j * 0.01745329f);
				this.ResetHelperVariables();
				char[] chrArray1 = this.textChars;
				for (int k = 0; k < (int)chrArray1.Length; k++)
				{
					char chr1 = chrArray1[k];
					this.CreateCharacter(chr1, num2, _right * this._privateProperties.outLineWidth, this._privateProperties.outlineColor, this._privateProperties.outlineColor);
					num2++;
				}
				int num5 = num3;
				num3 = num5 + 1;
				this.SetAlignment(num5);
			}
		}
		this.ResetHelperVariables();
		char[] chrArray2 = this.textChars;
		for (int l = 0; l < (int)chrArray2.Length; l++)
		{
			char chr2 = chrArray2[l];
			this.CreateCharacter(chr2, num2, Vector3.get_zero(), this._privateProperties.fontColorTop, this._privateProperties.fontColorBottom);
			num2++;
		}
		int num6 = num3;
		num3 = num6 + 1;
		this.SetAlignment(num6);
		if (this.textMesh != null)
		{
			this.textMesh.Clear(true);
			this.SetAnchor();
			this.textMesh.set_vertices(this.vertices);
			this.textMesh.set_uv(this.uv);
			this.textMesh.set_uv2(this.uv2);
			if (!(this._privateProperties.customFillMaterial != null) || !this._privateProperties.enableShadow && !this._privateProperties.enableOutline)
			{
				this.textMesh.set_triangles(this.triangles);
			}
			else
			{
				this.SetTrianglesForMultimesh();
			}
			this.textMesh.set_colors(this.colors);
		}
	}

	public void RefreshMeshEditor()
	{
		this.CacheTextVars();
		Object.DestroyImmediate(this.textMesh);
		this.textMesh = new Mesh();
		this.textMesh.set_name(base.GetInstanceID().ToString());
		MeshFilter component = base.GetComponent<MeshFilter>();
		if (component != null)
		{
			component.set_sharedMesh(this.textMesh);
			if (base.get_renderer().get_sharedMaterial() == null)
			{
				base.get_renderer().set_sharedMaterial(this._privateProperties.font.get_material());
			}
			this.RefreshMesh(true);
		}
	}

	private void ResetHelperVariables()
	{
		this.lineBreakAccumulatedDistance.Clear();
		this.lineBreakCharCounter.Clear();
		this.currentLineBreak = 0;
		this.heightSum = 0f;
	}

	private void SetAlignment(int _pass)
	{
		int num;
		int num1 = _pass * (int)this.textChars.Length * 4;
		float item = 0f;
		for (int i = 0; i < this.lineBreakCharCounter.get_Count(); i++)
		{
			switch (this._privateProperties.textAlignment)
			{
				case EasyFontTextMesh.TEXT_ALIGNMENT.right:
				{
					item = -this.lineBreakAccumulatedDistance.get_Item(i) * this._privateProperties.size / (float)this._privateProperties.fontSize;
					break;
				}
				case EasyFontTextMesh.TEXT_ALIGNMENT.center:
				{
					item = -this.lineBreakAccumulatedDistance.get_Item(i) * 0.5f * this._privateProperties.size / (float)this._privateProperties.fontSize;
					break;
				}
			}
			num = (i != 0 ? this.lineBreakCharCounter.get_Item(i - 1) * 4 : 0);
			int item1 = this.lineBreakCharCounter.get_Item(i) * 4 - 1;
			for (int j = num + i * 4 + num1; j <= item1 + i * 4 + num1; j++)
			{
				this.vertices[j].x = this.vertices[j].x + item;
			}
		}
	}

	private void SetAnchor()
	{
		Vector2 _zero = Vector2.get_zero();
		float item = 0f;
		for (int i = 0; i < this.lineBreakAccumulatedDistance.get_Count(); i++)
		{
			if (this.lineBreakAccumulatedDistance.get_Item(i) > item)
			{
				item = this.lineBreakAccumulatedDistance.get_Item(i);
			}
		}
		switch (this._privateProperties.textAnchor)
		{
			case EasyFontTextMesh.TEXT_ANCHOR.UpperLeft:
			case EasyFontTextMesh.TEXT_ANCHOR.MiddleLeft:
			case EasyFontTextMesh.TEXT_ANCHOR.LowerLeft:
			{
				switch (this._privateProperties.textAlignment)
				{
					case EasyFontTextMesh.TEXT_ALIGNMENT.left:
					{
						_zero.x = 0f;
						break;
					}
					case EasyFontTextMesh.TEXT_ALIGNMENT.right:
					{
						_zero.x = item * this._privateProperties.size / (float)this._privateProperties.fontSize;
						break;
					}
					case EasyFontTextMesh.TEXT_ALIGNMENT.center:
					{
						_zero.x = _zero.x + item * 0.5f * this._privateProperties.size / (float)this._privateProperties.fontSize;
						break;
					}
				}
				break;
			}
			case EasyFontTextMesh.TEXT_ANCHOR.UpperRight:
			case EasyFontTextMesh.TEXT_ANCHOR.MiddleRight:
			case EasyFontTextMesh.TEXT_ANCHOR.LowerRight:
			{
				switch (this._privateProperties.textAlignment)
				{
					case EasyFontTextMesh.TEXT_ALIGNMENT.left:
					{
						_zero.x = _zero.x - item * this._privateProperties.size / (float)this._privateProperties.fontSize;
						break;
					}
					case EasyFontTextMesh.TEXT_ALIGNMENT.right:
					{
						_zero.x = 0f;
						break;
					}
					case EasyFontTextMesh.TEXT_ALIGNMENT.center:
					{
						_zero.x = _zero.x - item * 0.5f * this._privateProperties.size / (float)this._privateProperties.fontSize;
						break;
					}
				}
				break;
			}
			case EasyFontTextMesh.TEXT_ANCHOR.UpperCenter:
			case EasyFontTextMesh.TEXT_ANCHOR.MiddleCenter:
			case EasyFontTextMesh.TEXT_ANCHOR.LowerCenter:
			{
				switch (this._privateProperties.textAlignment)
				{
					case EasyFontTextMesh.TEXT_ALIGNMENT.left:
					{
						_zero.x = _zero.x - item * this._privateProperties.size * 0.5f / (float)this._privateProperties.fontSize;
						break;
					}
					case EasyFontTextMesh.TEXT_ALIGNMENT.right:
					{
						_zero.x = item * 0.5f * this._privateProperties.size / (float)this._privateProperties.fontSize;
						break;
					}
					case EasyFontTextMesh.TEXT_ALIGNMENT.center:
					{
						_zero.x = 0f;
						break;
					}
				}
				break;
			}
		}
		if (this._privateProperties.textAnchor == EasyFontTextMesh.TEXT_ANCHOR.UpperLeft || this._privateProperties.textAnchor == EasyFontTextMesh.TEXT_ANCHOR.UpperRight || this._privateProperties.textAnchor == EasyFontTextMesh.TEXT_ANCHOR.UpperCenter)
		{
			_zero.y = -this.heightSum / (float)((int)this.textChars.Length);
		}
		else if (this._privateProperties.textAnchor == EasyFontTextMesh.TEXT_ANCHOR.MiddleCenter || this._privateProperties.textAnchor == EasyFontTextMesh.TEXT_ANCHOR.MiddleLeft || this._privateProperties.textAnchor == EasyFontTextMesh.TEXT_ANCHOR.MiddleRight)
		{
			_zero.y = -(this.heightSum / (float)((int)this.textChars.Length)) + this._privateProperties.size * (float)this.currentLineBreak * this._privateProperties.lineSpacing * 0.5f;
		}
		else if (this._privateProperties.textAnchor == EasyFontTextMesh.TEXT_ANCHOR.LowerLeft || this._privateProperties.textAnchor == EasyFontTextMesh.TEXT_ANCHOR.LowerRight || this._privateProperties.textAnchor == EasyFontTextMesh.TEXT_ANCHOR.LowerCenter)
		{
			_zero.y = -this.heightSum / (float)((int)this.textChars.Length) + this._privateProperties.size * (float)this.currentLineBreak * this._privateProperties.lineSpacing;
		}
		for (int j = 0; j < (int)this.vertices.Length; j++)
		{
			this.vertices[j].x = this.vertices[j].x + _zero.x;
			this.vertices[j].y = this.vertices[j].y + _zero.y;
		}
	}

	private void SetColor(Color _topColor, Color _bottomColor)
	{
		if (this.colors == null || this.textMesh == null)
		{
			return;
		}
		int initialVertexToColorize = this.GetInitialVertexToColorize(EasyFontTextMesh.TEXT_COMPONENT.Main);
		int num = 0;
		for (int i = initialVertexToColorize; i < this.GetFinalVertexToColorize(EasyFontTextMesh.TEXT_COMPONENT.Main); i++)
		{
			if (num == 0 || num == 1)
			{
				this.colors[i] = _bottomColor;
			}
			else
			{
				this.colors[i] = _topColor;
			}
			num++;
			if (num > 3)
			{
				num = 0;
			}
		}
		this.textMesh.set_colors(this.colors);
	}

	public void SetColor(Color _color)
	{
		if (this.colors == null || this.textMesh == null)
		{
			return;
		}
		for (int i = this.GetInitialVertexToColorize(EasyFontTextMesh.TEXT_COMPONENT.Main); i < this.GetFinalVertexToColorize(EasyFontTextMesh.TEXT_COMPONENT.Main); i++)
		{
			this.colors[i] = _color;
		}
		this.textMesh.set_colors(this.colors);
	}

	private void SetOutlineColor(Color _color)
	{
		if (this.colors == null || this.textMesh == null)
		{
			return;
		}
		for (int i = this.GetInitialVertexToColorize(EasyFontTextMesh.TEXT_COMPONENT.Outline); i < this.GetFinalVertexToColorize(EasyFontTextMesh.TEXT_COMPONENT.Outline); i++)
		{
			this.colors[i] = _color;
		}
		this.textMesh.set_colors(this.colors);
	}

	private void SetShadowColor(Color _color)
	{
		if (this.colors == null || this.textMesh == null)
		{
			return;
		}
		for (int i = this.GetInitialVertexToColorize(EasyFontTextMesh.TEXT_COMPONENT.Shadow); i < this.GetFinalVertexToColorize(EasyFontTextMesh.TEXT_COMPONENT.Shadow); i++)
		{
			this.colors[i] = _color;
		}
		this.textMesh.set_colors(this.colors);
	}

	private void SetTrianglesForMultimesh()
	{
		int num = 0;
		if (this._privateProperties.enableOutline && this._privateProperties.enableShadow)
		{
			num = 5;
		}
		else if (this._privateProperties.enableOutline)
		{
			num = 4;
		}
		else if (this._privateProperties.enableShadow)
		{
			num = 1;
		}
		int length = num * 6 * (int)this.textChars.Length;
		int[] numArray = new int[(int)this.textChars.Length * 6];
		int num1 = 0;
		for (int i = length; i < (int)this.triangles.Length; i++)
		{
			numArray[num1] = this.triangles[i];
			num1++;
		}
		num1 = 0;
		int length1 = (int)this.textChars.Length * num * 6;
		int[] numArray1 = new int[length1];
		for (int j = 0; j < length1; j++)
		{
			numArray1[num1] = this.triangles[j];
			num1++;
		}
		this.textMeshFilter.get_sharedMesh().set_subMeshCount(2);
		this.textMeshFilter.get_sharedMesh().SetTriangles(numArray, 1);
		this.textMeshFilter.get_sharedMesh().SetTriangles(numArray1, 0);
	}

	public enum TEXT_ALIGNMENT
	{
		left,
		right,
		center
	}

	public enum TEXT_ANCHOR
	{
		UpperLeft,
		UpperRight,
		UpperCenter,
		MiddleLeft,
		MiddleRight,
		MiddleCenter,
		LowerLeft,
		LowerRight,
		LowerCenter
	}

	private enum TEXT_COMPONENT
	{
		Main,
		Shadow,
		Outline
	}

	[Serializable]
	public class TextProperties
	{
		public string text;

		public Font font;

		public Material customFillMaterial;

		public int fontSize;

		public float size;

		public EasyFontTextMesh.TEXT_ANCHOR textAnchor;

		public EasyFontTextMesh.TEXT_ALIGNMENT textAlignment;

		public float lineSpacing;

		public Color fontColorTop;

		public Color fontColorBottom;

		public bool enableShadow;

		public Color shadowColor;

		public Vector3 shadowDistance;

		public bool enableOutline;

		public Color outlineColor;

		public float outLineWidth;

		public bool highQualityOutline;

		public TextProperties()
		{
		}
	}
}