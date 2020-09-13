using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Swindler.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Utils
{
	public class PoissonDisplay : MonoBehaviour
	{
		public float radius;
		public float scale; 
		public Vector2 sampleRegionSize = new Vector2(1, 1);
		private int samplesBeforeRejection = 30;
		public Vector3 cubeScale = new Vector3(0.25f, 0.25f, 0.25f);
		private List<Vector2> points;
		public Tilemap map;
		public TileBase tile;

		private void Start()
		{
			points = PoissonDiscSampling.GeneratePoints(radius, sampleRegionSize, samplesBeforeRejection);
			var pointsList = points.Select(p => new Vector2Int(Mathf.RoundToInt(p.x), Mathf.RoundToInt(p.y))).ToList();
			pointsList.Count.Log("Points count");
			File.WriteAllText(Application.persistentDataPath + "/islands.json", JsonConvert.SerializeObject(pointsList));
			(Application.persistentDataPath + "/islands.json").Log("Islands path");
			// foreach (Vector2 point in points)
			// {
			// 	// GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cube);
			// 	// o.transform.position = new Vector2(Mathf.Round(point.x * scale), Mathf.Round(point.y * scale));
			// 	// o.transform.localScale = cubeScale;
			//
			// 	int minX = Mathf.RoundToInt(point.x);
			// 	int maxX = minX + 40;
			// 	int minY = Mathf.RoundToInt(point.y);
			// 	int maxY = minY + 40;
			// 	for(int x = minX; x < maxX; x++)
			// 		for(int y = minY; y < maxY; y++)
			// 			map.SetTile(new Vector3Int(x, y, 0), tile);
			// 	
			// }
		}
	}
}