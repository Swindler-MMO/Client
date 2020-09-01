using UnityEngine;

namespace Swindler.Utils
{
	public static class StringExtensions
	{

		public static void Log(this string value)
		{
			Debug.Log("[Renderer] " + value);
		}

	}
}
