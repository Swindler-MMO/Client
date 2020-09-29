using Swindler.World;
using UnityEngine;

namespace Swindler.Utilities.Extensions
{
	public static class LogExtensions
	{

		public const string PREFIX = "[Swindler] ";

		public static void Log(this string value, string name = null)
		{
			Print(value, name);
		}
		
		public static void Log(this int value, string name = null)
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
		
		public static void Log(this Vector2Int value, string name = null)
		{
			Print(value, name);
		}
		
		public static void Log(this Vector3Int value, string name = null)
		{
			Print(value, name);
		}

		public static void Log(this float value, string name = null)
		{
			Print(value, name);
		}
		
		public static void Log(this long value, string name = null)
		{
			Print(value, name);
		}
		
		public static void Log(this bool value, string name = null)
		{
			Print(value, name);
		}

		private static void Print(object value, string name)
		{
			if(string.IsNullOrEmpty(name))
			{
				Debug.Log(PREFIX + value);
				return;
			}

			Debug.Log($"{PREFIX}{name} :>> {value}");
		}

	}

}
