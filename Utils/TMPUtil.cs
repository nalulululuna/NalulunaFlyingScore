using System;
using TMPro;

namespace NalulunaFlyingScore
{
	public static class TMPUtil
	{
		private static void AddCharToArray(in char[] c)
		{
			for (int i = c.Length - 1; i >= 0; i--)
			{
				TMPUtil._buffer[TMPUtil._current++] = c[i];
			}
		}

		private static void AddUIntToArray(uint number)
		{
			bool flag = number == 0U;
			if (flag)
			{
				TMPUtil._buffer[TMPUtil._current++] = '0';
			}
			else
			{
				while (number > 0U)
				{
					TMPUtil._buffer[TMPUtil._current++] = (char)(48U + number % 10U);
					number /= 10U;
				}
			}
		}

		private static void ReverseArray()
		{
			for (int i = TMPUtil._current / 2 - 1; i >= 0; i--)
			{
				int j = TMPUtil._current - 1 - i;
				char temp = TMPUtil._buffer[i];
				TMPUtil._buffer[i] = TMPUtil._buffer[j];
				TMPUtil._buffer[j] = temp;
			}
		}

		public static void SetText(this TMP_Text text, in uint number)
		{
			TMPUtil.AddUIntToArray(number);
			TMPUtil.ReverseArray();
			text.SetCharArray(TMPUtil._buffer, 0, TMPUtil._current);
			TMPUtil._current = 0;
		}

		public static void SetText(this TMP_Text text, in char[] prefix1, in char[] prefix2, in uint number, in char[] postfix1)
		{
			TMPUtil.AddCharToArray(postfix1);
			TMPUtil.AddUIntToArray(number);
			TMPUtil.AddCharToArray(prefix2);
			TMPUtil.AddCharToArray(prefix1);
			TMPUtil.ReverseArray();
			text.SetCharArray(TMPUtil._buffer, 0, TMPUtil._current);
			TMPUtil._current = 0;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static TMPUtil()
		{
		}

		private static char[] _buffer = new char[128];

		private static int _current = 0;
	}
}
