using System;
using System.Text;

namespace Swindler.Utils
{
	class Utils
	{

		private static readonly string CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		public static string RandomString(int length)
		{
			StringBuilder sb = new StringBuilder();
			Random rd = new Random();

			for (int i = 0; i < length; i++)
				sb.Append(CHARS[rd.Next(0, CHARS.Length)]);

			return sb.ToString();
		}

	}
}
