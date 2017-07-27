using System;

public struct Mathf
{
	public const float PI = 3.141593f;

	public const float Infinity = float.PositiveInfinity;

	public const float NegativeInfinity = float.NegativeInfinity;

	public const float Deg2Rad = 0.01745329f;

	public const float Rad2Deg = 57.29578f;

	public const float Epsilon = 1.401298E-45f;

	public static float Abs(float f)
	{
		return Math.Abs(f);
	}

	public static int Abs(int value)
	{
		return Math.Abs(value);
	}

	public static float Acos(float f)
	{
		return (float)Math.Acos((double)f);
	}

	public static bool Approximately(float a, float b)
	{
		bool flag = Mathf.Abs((float)(b - a)) < Mathf.Max((float)(1E-06f * Mathf.Max(Mathf.Abs(a), Mathf.Abs(b))), 1.121039E-44f);
		return flag;
	}

	public static float Asin(float f)
	{
		return (float)Math.Asin((double)f);
	}

	public static float Atan(float f)
	{
		return (float)Math.Atan((double)f);
	}

	public static float Atan2(float y, float x)
	{
		return (float)Math.Atan2((double)y, (double)x);
	}

	public static float Ceil(float f)
	{
		return (float)Math.Ceiling((double)f);
	}

	public static int CeilToInt(float f)
	{
		return (int)Math.Ceiling((double)f);
	}

	public static float Clamp(float value, float min, float max)
	{
		float single;
		if (value >= min)
		{
			if (value > max)
			{
				value = max;
			}
			single = value;
		}
		else
		{
			value = min;
			single = value;
		}
		return single;
	}

	public static int Clamp(int value, int min, int max)
	{
		int num;
		if (value >= min)
		{
			if (value > max)
			{
				value = max;
			}
			num = value;
		}
		else
		{
			value = min;
			num = value;
		}
		return num;
	}

	public static float Clamp01(float value)
	{
		float single;
		if (value >= 0f)
		{
			single = (value <= 1f ? value : 1f);
		}
		else
		{
			single = 0f;
		}
		return single;
	}

	public static float Cos(float f)
	{
		return (float)Math.Cos((double)f);
	}

	public static float DeltaAngle(float current, float target)
	{
		float single = Mathf.Repeat(target - current, 360f);
		if (single > 180f)
		{
			single -= 360f;
		}
		return single;
	}

	public static float Exp(float power)
	{
		return (float)Math.Exp((double)power);
	}

	public static float Floor(float f)
	{
		return (float)Math.Floor((double)f);
	}

	public static int FloorToInt(float f)
	{
		return (int)Math.Floor((double)f);
	}

	public static float Gamma(float value, float absmax, float gamma)
	{
		float single;
		bool flag = false;
		if (value < 0f)
		{
			flag = true;
		}
		float single1 = Mathf.Abs(value);
		if (single1 <= absmax)
		{
			float single2 = Mathf.Pow(single1 / absmax, gamma) * absmax;
			single = (!flag ? single2 : -single2);
		}
		else
		{
			single = (!flag ? single1 : -single1);
		}
		return single;
	}

	public static float InverseLerp(float from, float to, float value)
	{
		float single;
		if (from < to)
		{
			if (value < from)
			{
				single = 0f;
			}
			else if (value <= to)
			{
				value -= from;
				value = value / (to - from);
				single = value;
			}
			else
			{
				single = 1f;
			}
		}
		else if (from <= to)
		{
			single = 0f;
		}
		else if (value >= to)
		{
			single = (value <= from ? 1f - (value - to) / (from - to) : 0f);
		}
		else
		{
			single = 1f;
		}
		return single;
	}

	public static float Lerp(float from, float to, float t)
	{
		float single = from + (to - from) * Mathf.Clamp01(t);
		return single;
	}

	public static float LerpAngle(float a, float b, float t)
	{
		float single = Mathf.Repeat(b - a, 360f);
		if (single > 180f)
		{
			single -= 360f;
		}
		return a + single * Mathf.Clamp01(t);
	}

	public static float Log(float f, float p)
	{
		return (float)Math.Log((double)f, (double)p);
	}

	public static float Log(float f)
	{
		return (float)Math.Log((double)f);
	}

	public static float Log10(float f)
	{
		return (float)Math.Log10((double)f);
	}

	public static float Max(float a, float b)
	{
		return (a <= b ? b : a);
	}

	public static float Max(params float[] values)
	{
		float single;
		int length = (int)values.Length;
		if (length != 0)
		{
			float single1 = values[0];
			for (int i = 1; i < length; i++)
			{
				if (values[i] > single1)
				{
					single1 = values[i];
				}
			}
			single = single1;
		}
		else
		{
			single = 0f;
		}
		return single;
	}

	public static int Max(int a, int b)
	{
		return (a <= b ? b : a);
	}

	public static int Max(params int[] values)
	{
		int num;
		int length = (int)values.Length;
		if (length != 0)
		{
			int num1 = values[0];
			for (int i = 1; i < length; i++)
			{
				if (values[i] > num1)
				{
					num1 = values[i];
				}
			}
			num = num1;
		}
		else
		{
			num = 0;
		}
		return num;
	}

	public static float Min(float a, float b)
	{
		return (a >= b ? b : a);
	}

	public static float Min(params float[] values)
	{
		float single;
		int length = (int)values.Length;
		if (length != 0)
		{
			float single1 = values[0];
			for (int i = 1; i < length; i++)
			{
				if (values[i] < single1)
				{
					single1 = values[i];
				}
			}
			single = single1;
		}
		else
		{
			single = 0f;
		}
		return single;
	}

	public static int Min(int a, int b)
	{
		return (a >= b ? b : a);
	}

	public static int Min(params int[] values)
	{
		int num;
		int length = (int)values.Length;
		if (length != 0)
		{
			int num1 = values[0];
			for (int i = 1; i < length; i++)
			{
				if (values[i] < num1)
				{
					num1 = values[i];
				}
			}
			num = num1;
		}
		else
		{
			num = 0;
		}
		return num;
	}

	public static float MoveTowards(float current, float target, float maxDelta)
	{
		float single;
		single = (Mathf.Abs((float)(target - current)) > maxDelta ? current + Mathf.Sign(target - current) * maxDelta : target);
		return single;
	}

	public static float MoveTowardsAngle(float current, float target, float maxDelta)
	{
		target = current + Mathf.DeltaAngle(current, target);
		return Mathf.MoveTowards(current, target, maxDelta);
	}

	public static float PingPong(float t, float length)
	{
		t = Mathf.Repeat(t, length * 2f);
		float single = length - Mathf.Abs((float)(t - length));
		return single;
	}

	public static float Pow(float f, float p)
	{
		return (float)Math.Pow((double)f, (double)p);
	}

	public static float Repeat(float t, float length)
	{
		float single = t - Mathf.Floor(t / length) * length;
		return single;
	}

	public static float Round(float f)
	{
		return (float)Math.Round((double)f);
	}

	public static int RoundToInt(float f)
	{
		return (int)Math.Round((double)f);
	}

	public static float Sign(float f)
	{
		return (f < 0f ? -1f : 1f);
	}

	public static float Sin(float f)
	{
		return (float)Math.Sin((double)f);
	}

	public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
	{
		smoothTime = Mathf.Max(0.0001f, smoothTime);
		float single = 2f / smoothTime;
		float single1 = single * deltaTime;
		float single2 = 1f / (1f + single1 + 0.48f * single1 * single1 + 0.235f * single1 * single1 * single1);
		float single3 = current - target;
		float single4 = target;
		float single5 = maxSpeed * smoothTime;
		single3 = Mathf.Clamp(single3, -single5, single5);
		target = current - single3;
		float single6 = (currentVelocity + single * single3) * deltaTime;
		currentVelocity = (currentVelocity - single * single6) * single2;
		float single7 = target + (single3 + single6) * single2;
		if (single4 - current > 0f == single7 > single4)
		{
			single7 = single4;
			currentVelocity = (single7 - single4) / deltaTime;
		}
		return single7;
	}

	public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
	{
		target = current + Mathf.DeltaAngle(current, target);
		float single = Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		return single;
	}

	public static float SmoothStep(float from, float to, float t)
	{
		t = Mathf.Clamp01(t);
		t = -2f * t * t * t + 3f * t * t;
		float single = to * t + from * (1f - t);
		return single;
	}

	public static float Sqrt(float f)
	{
		return (float)Math.Sqrt((double)f);
	}

	public static float Tan(float f)
	{
		return (float)Math.Tan((double)f);
	}
}