using System;
using System.Collections;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
	public GameObject ShootingStarPref;

	public float ShootingStarSpeed = 10f;

	public int ShootingStarRpm = 60;

	public GameObject StarPrefab_1;

	public GameObject StarPrefab_2;

	public GameObject StarPrefab_3;

	public GameObject StarPrefab_4;

	public GameObject StarPrefab_5;

	public GameObject PilonPrefab;

	public int level_1 = -10;

	public int cnt_1_1 = 15;

	public int cnt_1_2 = 15;

	public int cnt_1_3 = 15;

	public int cnt_1_4 = 15;

	public int cnt_1_5 = 15;

	public int level_2 = -5;

	public int cnt_2_1 = 15;

	public int cnt_2_2 = 15;

	public int cnt_2_3 = 15;

	public int cnt_2_4 = 15;

	public int cnt_2_5 = 15;

	private ArrayList FieldList = new ArrayList();

	private ArrayList PilonList;

	private GameObject _edgeA;

	private GameObject _edgeB;

	private GameObject _edgeC;

	private GameObject _edgeD;

	private static int Xdiv;

	private static int Zdiv;

	private int Xmin;

	private int Zmin;

	private GameObject InstantiatedStar;

	private float nextShootingStar;

	static StarGenerator()
	{
		StarGenerator.Xdiv = 23;
		StarGenerator.Zdiv = 23;
	}

	public StarGenerator()
	{
	}

	private void Awake()
	{
		this.PilonList = new ArrayList();
		this.SetCenterRect(base.get_transform().get_position());
		this.SpawnStars();
		GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Edge");
		for (int i = 0; i < (int)gameObjectArray.Length; i++)
		{
			GameObject gameObject = gameObjectArray[i];
			if (gameObject.get_name().EndsWith("A"))
			{
				this._edgeA = gameObject;
			}
			else if (gameObject.get_name().EndsWith("B"))
			{
				this._edgeB = gameObject;
			}
			else if (gameObject.get_name().EndsWith("C"))
			{
				this._edgeC = gameObject;
			}
			else if (gameObject.get_name().EndsWith("D"))
			{
				this._edgeD = gameObject;
			}
		}
	}

	private void ClearUselessStars()
	{
		GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Star");
		for (int i = 0; i < (int)gameObjectArray.Length; i++)
		{
			GameObject gameObject = gameObjectArray[i];
			if (gameObject.get_transform().get_position().x < (float)(this.Xmin - 2 * StarGenerator.Xdiv) || gameObject.get_transform().get_position().x >= (float)(this.Xmin + 3 * StarGenerator.Xdiv) || gameObject.get_transform().get_position().z < (float)(this.Zmin - 2 * StarGenerator.Zdiv) || gameObject.get_transform().get_position().z >= (float)(this.Zmin + 3 * StarGenerator.Zdiv))
			{
				string[] strArray = gameObject.get_name().Split(new char[] { '(', ',', ')' });
				this.FieldList.Remove(new Vector2(float.Parse(strArray[1]), float.Parse(strArray[2])));
				Object.Destroy(gameObject);
			}
		}
	}

	private void SetCenterRect(Vector3 pos)
	{
		this.Xmin = (int)pos.x / StarGenerator.Xdiv;
		StarGenerator xmin = this;
		xmin.Xmin = xmin.Xmin * StarGenerator.Xdiv;
		this.Zmin = (int)pos.z / StarGenerator.Zdiv;
		StarGenerator zmin = this;
		zmin.Zmin = zmin.Zmin * StarGenerator.Zdiv;
	}

	private void SpawnInField(Vector2 v)
	{
		if (this.StarPrefab_1 != null)
		{
			this.SpawnOneTypeStar(v, Random.Range(-20, 0) + this.level_2, this.cnt_1_1, this.StarPrefab_1);
			this.SpawnOneTypeStar(v, Random.Range(0, -40) + this.level_1, this.cnt_2_1, this.StarPrefab_1);
		}
		if (this.StarPrefab_2 != null)
		{
			this.SpawnOneTypeStar(v, Random.Range(-20, 0) + this.level_2, this.cnt_1_2, this.StarPrefab_2);
			this.SpawnOneTypeStar(v, Random.Range(0, -40) + this.level_1, this.cnt_2_2, this.StarPrefab_2);
		}
		if (this.StarPrefab_3 != null)
		{
			this.SpawnOneTypeStar(v, Random.Range(-20, 0) + this.level_2, this.cnt_1_3, this.StarPrefab_3);
			this.SpawnOneTypeStar(v, Random.Range(0, -40) + this.level_1, this.cnt_2_3, this.StarPrefab_3);
		}
		if (this.StarPrefab_4 != null)
		{
			this.SpawnOneTypeStar(v, Random.Range(-20, 0) + this.level_2, this.cnt_1_4, this.StarPrefab_4);
			this.SpawnOneTypeStar(v, Random.Range(0, -40) + this.level_1, this.cnt_2_4, this.StarPrefab_4);
		}
		if (this.StarPrefab_5 != null)
		{
			this.SpawnOneTypeStar(v, Random.Range(-20, 0) + this.level_2, this.cnt_1_5, this.StarPrefab_5);
			this.SpawnOneTypeStar(v, Random.Range(0, -40) + this.level_1, this.cnt_2_5, this.StarPrefab_5);
		}
	}

	private void SpawnOneTypeStar(Vector2 v, int Alevel, int cnt, GameObject go)
	{
		if (cnt == 0)
		{
			return;
		}
		if (Random.Range(1, cnt + 1) != 1)
		{
			return;
		}
		Vector3 vector3 = new Vector3(Random.Range(v.x, v.x + (float)StarGenerator.Xdiv), (float)Alevel, Random.Range(v.y, v.y + (float)StarGenerator.Zdiv));
		this.InstantiatedStar = Object.Instantiate(go, vector3, base.get_transform().get_rotation()) as GameObject;
		this.InstantiatedStar.set_name(string.Concat(new string[] { "Star(", v.x.ToString(), ",", v.y.ToString(), ")" }));
	}

	private void SpawnPilons()
	{
		Vector2 vector2;
		Vector3 vector3;
		if (Mathf.Abs(base.get_transform().get_position().x - this._edgeA.get_transform().get_position().x) < 40f)
		{
			for (int i = 0; i < 5; i++)
			{
				Vector3 _position = this._edgeA.get_transform().get_position();
				vector2 = new Vector2(_position.x, (float)(this.Zmin + (-2 + i) * StarGenerator.Zdiv));
				if (this.PilonList.IndexOf(vector2) == -1)
				{
					float single = vector2.x;
					Vector3 _position1 = this._edgeA.get_transform().get_position();
					vector3 = new Vector3(single, _position1.y, vector2.y);
					this.PilonList.Add(vector2);
					Object.Instantiate(this.PilonPrefab, vector3, this._edgeA.get_transform().get_rotation());
				}
			}
		}
		else if (Mathf.Abs(base.get_transform().get_position().x - this._edgeC.get_transform().get_position().x) < 40f)
		{
			for (int j = 0; j < 5; j++)
			{
				Vector3 vector31 = this._edgeC.get_transform().get_position();
				vector2 = new Vector2(vector31.x, (float)(this.Zmin + (-2 + j) * StarGenerator.Zdiv));
				if (this.PilonList.IndexOf(vector2) == -1)
				{
					float single1 = vector2.x;
					Vector3 _position2 = this._edgeC.get_transform().get_position();
					vector3 = new Vector3(single1, _position2.y, vector2.y);
					this.PilonList.Add(vector2);
					Object.Instantiate(this.PilonPrefab, vector3, this._edgeC.get_transform().get_rotation());
				}
			}
		}
		else if (Mathf.Abs(base.get_transform().get_position().z - this._edgeD.get_transform().get_position().z) < 40f)
		{
			for (int k = 0; k < 5; k++)
			{
				float xmin = (float)(this.Xmin + (-2 + k) * StarGenerator.Xdiv);
				Vector3 vector32 = this._edgeD.get_transform().get_position();
				vector2 = new Vector2(xmin, vector32.z);
				if (this.PilonList.IndexOf(vector2) == -1)
				{
					float single2 = vector2.x;
					Vector3 _position3 = this._edgeD.get_transform().get_position();
					vector3 = new Vector3(single2, _position3.y, vector2.y);
					this.PilonList.Add(vector2);
					Object.Instantiate(this.PilonPrefab, vector3, this._edgeD.get_transform().get_rotation());
				}
			}
		}
		else if (Mathf.Abs(base.get_transform().get_position().z - this._edgeB.get_transform().get_position().z) >= 40f)
		{
			GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Pilon");
			for (int l = 0; l < (int)gameObjectArray.Length; l++)
			{
				GameObject gameObject = gameObjectArray[l];
				ArrayList pilonList = this.PilonList;
				float single3 = gameObject.get_transform().get_position().x;
				Vector3 vector33 = gameObject.get_transform().get_position();
				pilonList.Remove(new Vector2(single3, vector33.z));
				Object.Destroy(gameObject);
			}
		}
		else
		{
			for (int m = 0; m < 5; m++)
			{
				float xmin1 = (float)(this.Xmin + (-2 + m) * StarGenerator.Xdiv);
				Vector3 _position4 = this._edgeB.get_transform().get_position();
				vector2 = new Vector2(xmin1, _position4.z);
				if (this.PilonList.IndexOf(vector2) == -1)
				{
					vector3 = new Vector3(vector2.x, 0f, vector2.y);
					this.PilonList.Add(vector2);
					Object.Instantiate(this.PilonPrefab, vector3, this._edgeB.get_transform().get_rotation());
				}
			}
		}
	}

	private void SpawnShootingStar()
	{
		if (this.ShootingStarPref == null)
		{
			return;
		}
		if (Time.get_time() < this.nextShootingStar)
		{
			return;
		}
		int num = Random.Range(-1, 1);
		int num1 = Random.Range(-1, 1);
		if (num == 0)
		{
			num = 1;
		}
		if (num1 == 0)
		{
			num1 = 1;
		}
		Vector3 _position = base.get_transform().get_position();
		float single = _position.x + (float)(45 * num);
		float single1 = (float)Random.Range(-80, -40);
		Vector3 vector3 = base.get_transform().get_position();
		float single2 = vector3.z + (float)(45 * num1);
		Vector3 vector31 = new Vector3(single, single1, single2);
		Quaternion _rotation = base.get_transform().get_rotation();
		_rotation.set_eulerAngles(new Vector3(0f, Random.Range(0f, 360f), 0f));
		Rigidbody rigidbody = Object.Instantiate(this.ShootingStarPref, vector31, _rotation) as Rigidbody;
		rigidbody.get_transform().get_rigidbody().set_velocity(rigidbody.get_transform().TransformDirection(new Vector3(this.ShootingStarSpeed * Time.get_deltaTime(), 0f, 0f)));
		this.nextShootingStar = Time.get_time() + 5f;
	}

	private void SpawnStars()
	{
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				Vector2 vector2 = new Vector2((float)(this.Xmin + (-2 + i) * StarGenerator.Xdiv), (float)(this.Zmin + (-2 + j) * StarGenerator.Zdiv));
				if (this.FieldList.IndexOf(vector2) == -1)
				{
					this.FieldList.Add(vector2);
					this.SpawnInField(vector2);
				}
			}
		}
	}

	private void Update()
	{
	}
}