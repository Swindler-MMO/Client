using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

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

		public static Vector3Int MouseToCell(Tilemap tm)
		{
			Camera cam = Camera.main;
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = Mathf.Abs(cam.transform.position.z);
			return tm.WorldToCell(cam.ScreenToWorldPoint(mousePos));
		}

	}
}
