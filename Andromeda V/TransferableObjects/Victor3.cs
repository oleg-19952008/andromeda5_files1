using System;
using System.IO;
using System.Reflection;

public struct Victor3 : ITransferable
{
	public const float kEpsilon = 1E-05f;

	public float x;

	public float y;

	public float z;

	public static Victor3 back
	{
		get
		{
			return new Victor3(0f, 0f, -1f);
		}
	}

	public static Victor3 down
	{
		get
		{
			return new Victor3(0f, -1f, 0f);
		}
	}

	public static Victor3 forward
	{
		get
		{
			return new Victor3(0f, 0f, 1f);
		}
	}

	[Obsolete("Use Victor3.forward instead.")]
	public static Victor3 fwd
	{
		get
		{
			return new Victor3(0f, 0f, 1f);
		}
	}

	public float this[int index]
	{
		get
		{
			float single;
			switch (index)
			{
				case 0:
				{
					single = this.x;
					break;
				}
				case 1:
				{
					single = this.y;
					break;
				}
				case 2:
				{
					single = this.z;
					break;
				}
				default:
				{
					throw new IndexOutOfRangeException("Invalid Victor3 index!");
				}
			}
			return single;
		}
		set
		{
			switch (index)
			{
				case 0:
				{
					this.x = value;
					break;
				}
				case 1:
				{
					this.y = value;
					break;
				}
				case 2:
				{
					this.z = value;
					break;
				}
				default:
				{
					throw new IndexOutOfRangeException("Invalid Victor3 index!");
				}
			}
		}
	}

	public static Victor3 left
	{
		get
		{
			return new Victor3(-1f, 0f, 0f);
		}
	}

	public float magnitude
	{
		get
		{
			float single = Mathf.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
			return single;
		}
	}

	public Victor3 normalized
	{
		get
		{
			return Victor3.Normalize(this);
		}
	}

	public static Victor3 one
	{
		get
		{
			return new Victor3(1f, 1f, 1f);
		}
	}

	public static Victor3 right
	{
		get
		{
			return new Victor3(1f, 0f, 0f);
		}
	}

	public float sqrMagnitude
	{
		get
		{
			float single = this.x * this.x + this.y * this.y + this.z * this.z;
			return single;
		}
	}

	public static Victor3 up
	{
		get
		{
			return new Victor3(0f, 1f, 0f);
		}
	}

	public static Victor3 zero
	{
		get
		{
			return new Victor3(0f, 0f, 0f);
		}
	}

	public Victor3(float x, float y, float z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public Victor3(float x, float y)
	{
		this.x = x;
		this.y = y;
		this.z = 0f;
	}

	public static float Angle(Victor3 from, Victor3 to)
	{
		float single = Mathf.Acos(Mathf.Clamp(Victor3.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
		return single;
	}

	[Obsolete("Use Victor3.Angle instead. AngleBetween uses radians instead of degrees and was deprecated for this reason")]
	public static float AngleBetween(Victor3 from, Victor3 to)
	{
		float single = Mathf.Acos(Mathf.Clamp(Victor3.Dot(from.normalized, to.normalized), -1f, 1f));
		return single;
	}

	public static Victor3 ClampMagnitude(Victor3 vector, float maxLength)
	{
		Victor3 victor3;
		victor3 = (vector.sqrMagnitude <= maxLength * maxLength ? vector : vector.normalized * maxLength);
		return victor3;
	}

	public static Victor3 Cross(Victor3 lhs, Victor3 rhs)
	{
		Victor3 victor3 = new Victor3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
		return victor3;
	}

	public void Deserialize(BinaryReader br)
	{
		this.x = br.ReadSingle();
		this.y = br.ReadSingle();
		this.z = br.ReadSingle();
	}

	public static float Distance(Victor3 a, Victor3 b)
	{
		Victor3 victor3 = new Victor3(a.x - b.x, a.y - b.y, a.z - b.z);
		float single = Mathf.Sqrt(victor3.x * victor3.x + victor3.y * victor3.y + victor3.z * victor3.z);
		return single;
	}

	public static float Dot(Victor3 lhs, Victor3 rhs)
	{
		float single = lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		return single;
	}

	public override bool Equals(object other)
	{
		bool flag;
		if (other is Victor3)
		{
			Victor3 victor3 = (Victor3)other;
			flag = (!this.x.Equals(victor3.x) || !this.y.Equals(victor3.y) ? false : this.z.Equals(victor3.z));
		}
		else
		{
			flag = false;
		}
		return flag;
	}

	public static Victor3 Exclude(Victor3 excludeThis, Victor3 fromThat)
	{
		return fromThat - Victor3.Project(fromThat, excludeThis);
	}

	public override int GetHashCode()
	{
		int hashCode = this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
		return hashCode;
	}

	public static Victor3 Lerp(Victor3 from, Victor3 to, float t)
	{
		t = Mathf.Clamp01(t);
		Victor3 victor3 = new Victor3(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t, from.z + (to.z - from.z) * t);
		return victor3;
	}

	public static float Magnitude(Victor3 a)
	{
		float single = Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
		return single;
	}

	public static Victor3 Max(Victor3 lhs, Victor3 rhs)
	{
		Victor3 victor3 = new Victor3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		return victor3;
	}

	public static Victor3 Min(Victor3 lhs, Victor3 rhs)
	{
		Victor3 victor3 = new Victor3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		return victor3;
	}

	public static Victor3 MoveTowards(Victor3 current, Victor3 target, float maxDistanceDelta)
	{
		Victor3 victor3;
		Victor3 victor31 = target - current;
		float single = victor31.magnitude;
		victor3 = ((single <= maxDistanceDelta ? true : single == 0f) ? target : current + ((victor31 / single) * maxDistanceDelta));
		return victor3;
	}

	public static Victor3 Normalize(Victor3 value)
	{
		Victor3 victor3;
		float single = Victor3.Magnitude(value);
		victor3 = (single <= 1E-05f ? Victor3.zero : value / single);
		return victor3;
	}

	public void Normalize()
	{
		float single = Victor3.Magnitude(this);
		if (single <= 1E-05f)
		{
			this = Victor3.zero;
		}
		else
		{
			this = this / single;
		}
	}

	public static Victor3 operator +(Victor3 a, Victor3 b)
	{
		Victor3 victor3 = new Victor3(a.x + b.x, a.y + b.y, a.z + b.z);
		return victor3;
	}

	public static Victor3 operator /(Victor3 a, float d)
	{
		Victor3 victor3 = new Victor3(a.x / d, a.y / d, a.z / d);
		return victor3;
	}

	public static bool operator ==(Victor3 lhs, Victor3 rhs)
	{
		return Victor3.SqrMagnitude(lhs - rhs) < 9.999999E-11f;
	}

	public static bool operator !=(Victor3 lhs, Victor3 rhs)
	{
		return Victor3.SqrMagnitude(lhs - rhs) >= 9.999999E-11f;
	}

	public static Victor3 operator *(Victor3 a, float d)
	{
		Victor3 victor3 = new Victor3(a.x * d, a.y * d, a.z * d);
		return victor3;
	}

	public static Victor3 operator *(float d, Victor3 a)
	{
		Victor3 victor3 = new Victor3(a.x * d, a.y * d, a.z * d);
		return victor3;
	}

	public static Victor3 operator -(Victor3 a, Victor3 b)
	{
		Victor3 victor3 = new Victor3(a.x - b.x, a.y - b.y, a.z - b.z);
		return victor3;
	}

	public static Victor3 operator -(Victor3 a)
	{
		Victor3 victor3 = new Victor3(-a.x, -a.y, -a.z);
		return victor3;
	}

	public static Victor3 Project(Victor3 vector, Victor3 onNormal)
	{
		Victor3 victor3;
		float single = Victor3.Dot(onNormal, onNormal);
		victor3 = (single >= 1.401298E-45f ? (onNormal * Victor3.Dot(vector, onNormal)) / single : Victor3.zero);
		return victor3;
	}

	public static Victor3 Reflect(Victor3 inDirection, Victor3 inNormal)
	{
		Victor3 victor3 = (-2f * Victor3.Dot(inNormal, inDirection) * inNormal) + inDirection;
		return victor3;
	}

	public static Victor3 Scale(Victor3 a, Victor3 b)
	{
		Victor3 victor3 = new Victor3(a.x * b.x, a.y * b.y, a.z * b.z);
		return victor3;
	}

	public void Scale(Victor3 scale)
	{
		Victor3 victor3 = this;
		victor3.x = victor3.x * scale.x;
		Victor3 victor31 = this;
		victor31.y = victor31.y * scale.y;
		Victor3 victor32 = this;
		victor32.z = victor32.z * scale.z;
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.x);
		bw.Write(this.y);
		bw.Write(this.z);
	}

	public static Victor3 SmoothDamp(Victor3 current, Victor3 target, ref Victor3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
	{
		smoothTime = Mathf.Max(0.0001f, smoothTime);
		float single = 2f / smoothTime;
		float single1 = single * deltaTime;
		float single2 = 1f / (1f + single1 + 0.48f * single1 * single1 + 0.235f * single1 * single1 * single1);
		Victor3 victor3 = current - target;
		Victor3 victor31 = target;
		victor3 = Victor3.ClampMagnitude(victor3, maxSpeed * smoothTime);
		target = current - victor3;
		Victor3 victor32 = (currentVelocity + (single * victor3)) * deltaTime;
		currentVelocity = (currentVelocity - (single * victor32)) * single2;
		Victor3 victor33 = target + ((victor3 + victor32) * single2);
		if (Victor3.Dot(victor31 - current, victor33 - victor31) > 0f)
		{
			victor33 = victor31;
			currentVelocity = (victor33 - victor31) / deltaTime;
		}
		return victor33;
	}

	public static float SqrMagnitude(Victor3 a)
	{
		float single = a.x * a.x + a.y * a.y + a.z * a.z;
		return single;
	}

	public override string ToString()
	{
		string str = string.Format("({0:F1}, {1:F1}, {2:F1})", this.x, this.y, this.z);
		return str;
	}

	public string ToString(string format)
	{
		string str = string.Format("({0}, {1}, {2})", this.x.ToString(format), this.y.ToString(format), this.z.ToString(format));
		return str;
	}
}