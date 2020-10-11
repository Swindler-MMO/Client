using Swindler.Game;
using Swindler.World.Renderers;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Swindler.Utilities.Extensions
{
	public static class GameObjectExtensions
	{

		public static Health AddHealh(this GameObject o, float maxHealth, Vector3 pos, bool modify = false)
		{
			Health h = o.AddComponent<Health>();
			h.SetMaxHealth(maxHealth);
			
			if(modify)
				h.ModifyHealth(-1);
			
			o.transform.position = pos;

			return h;
		}

		public static IslandRenderer AddIslandRenderer(this GameObject o, TileBase[] tiles, SerializableStringTilemap tilemaps, TileBase treeTile, TileBase rockTile, int x, int y)
		{
			IslandRenderer islandRenderer = o.AddComponent<IslandRenderer>();
			islandRenderer.SetRenderData(tiles, tilemaps, treeTile, rockTile);
			islandRenderer.SetIsland(x, y);
			return islandRenderer;
		}
		
	}
}