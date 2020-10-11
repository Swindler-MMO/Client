using LiteNetLib;
using Multiplayer.Packets;
using Swindler.Utilities;
using Swindler.Utilities.Extensions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Swindler.Game.Structures.Tiles
{
	[CreateAssetMenu(fileName = "Resource Name", menuName = "Swindler/Resource Tile")]
	public class ResourceTile : InteractableTile
	{
		public string resourceName;
		public byte resourceId;
		public AudioClip impactSound;

		public override void OnInteract(Vector3Int position, Tilemap map, AudioSource audioSource)
		{
			audioSource.PlayOneShot(impactSound, 0.2f);
			AddHealthBar(position, map);
			GameManager.Server.Send(
				new PlayerInteractResourcePacket(resourceId, new Vector2Int(position.x, position.y)),
				DeliveryMethod.Sequenced);
		}

		public override bool CanInteract(Vector3Int position, Tilemap map)
		{
			return IsTopTile(position, map);
		}

		public bool IsTopTile(Vector3Int position, Tilemap map)
		{
			return map.GetTile(new Vector3Int(position.x, position.y - 1, position.z)) == null;
		}

		private void AddHealthBar(Vector3Int vector3Int, Tilemap tilemap)
		{
			Vector3 barPos = tilemap.GetCellCenterWorld(vector3Int);

			HealthBarController controller = HealthBarController.Instance;

			if (controller.ExistHealthBar(barPos))
			{
				controller.GetHealthBar(barPos).ModifyHealth(-1);
				return;
			}

			new GameObject($"Health {vector3Int}")
				.AddHealh(9f, barPos, true);
		}
	}
}