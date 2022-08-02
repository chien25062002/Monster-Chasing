using System;

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
}
