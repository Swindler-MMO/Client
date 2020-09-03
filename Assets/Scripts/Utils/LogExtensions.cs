using UnityEngine;

namespace Swindler.Utils
{
	public static class LogExtensions
	{

		public const string PREFIX = "[Swindler] ";

		public static void Log(this string value, string name = null)
		{
			Print(value, name);
		}

		public static void Log(this Vector2 value, string name = null)
		{
			Print(value, name);
		}

		public static void Log(this Vector3 value, string name = null)
		{
			Print(value, name);
		}

		private static void Print(object value, string name)
		{

			if(string.IsNullOrEmpty(name))
			{
				Debug.Log(PREFIX + name);
			}

			Debug.Log($"{PREFIX}{name} :>> {value}");
		}

	}

}
