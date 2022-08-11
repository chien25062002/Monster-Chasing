using System;
using UnityEngine;
using System.Globalization;

public class mSystem
{
    public static double currentTimeNanoSeconds()
	{
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return (DateTime.UtcNow.Ticks - dateTime.Ticks) / (10000 * Math.Pow(10, 9));
	}

	public static long ConvertSecondsToMilliseconds(float seconds) {
		return (long) seconds * 1000;
	}

	public static float ConvertMilisecondsToSeconds(long miliseconds) {
		return miliseconds / 1000;
	}

	public static float ParseFloat(string value) {
		return float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
	}

	public static Vector3 WorldToScreenPoint(Vector3 worldPoint) {
		//return Camera.main.WorldToScreenPoint(new Vector3(worldPoint.x, worldPoint.y, worldPoint.z));
		return RectTransformUtility.WorldToScreenPoint(Camera.main, worldPoint);
	}
} 
