using System.Collections.Generic;

namespace BattleTeam.Shared
{
	internal static class Utilities
	{
		internal static T Bound<T>(T toBound, T lowerBound, T upperBound)
		{
			int compareWithLower = Comparer<T>.Default.Compare(toBound, lowerBound);

			if (compareWithLower < 0)
			{
				return lowerBound;
			}

			int compareWithUpper = Comparer<T>.Default.Compare(toBound, upperBound);
			if (compareWithUpper > 0)
			{
				return upperBound;
			}

			return toBound;
		}

		internal static double WrapDouble(double i, double lower, double upper)
		{
			if (i > upper)
			{
				return i - upper + lower;
			}
			else if (i < lower)
			{
				return upper + i - lower;
			}

			return i;
		}

		internal static float WrapFloat(double i, double upper, double lower)
		{
			return (float)WrapDouble(i, upper, lower);
		}

		internal static int WrapInt(int i, int upper, int lower)
		{
			return (int)WrapDouble(i, upper, lower);
		}
	}
}
