using System;
using Multiplayer.Packets.Server;
using Player.Authoritative;
using Swindler.Multiplayer;
using Swindler.Player.Authoritative.Movement;
using Swindler.Utils;
using Swindler.World;
using Swindler.World.Renderers;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Swindler.Game
{
	public class GameManager : MonoBehaviour
	{

		public static GameManager Instance { get; private set; }
		public static GameServer Server { get; private set; }
		
		public static Inventory Inventory { get; private set; }
		
		[Header("Prefabs")]
		public GameObject authoritativePlayer;
		public GameObject remotePlayer;

		[Header("Player prefab objects")]
		public Tilemap island;
		public WorldManager worldManager;
		public WaterRenderer waterRenderer;
		public Tilemap interactionMap;
		public Tilemap indicatorsMap;
		public AnimatedTile highlightTile;

		private void Awake()
		{
			Server = CreateGameServer();
			Instance = this;
		}

		private void Start()
		{
			Server.Connect();
		}

		public void OnConnectedToGameServer()
		{
			"Connected".Log();
			
			//TODO: Instantiate player
			SpawnAuthoritativePlayer(5021, 5034);
		}

		private GameServer CreateGameServer()
		{
			GameObject obj = new GameObject("GameServer");
			obj.transform.parent = transform;
			
			GameServer gs = obj.AddComponent<GameServer>();
			gs.game = this;
			return gs;
		}

		private void SpawnAuthoritativePlayer(float x, float y)
		{
			//TODO: Maybe use static method in a component ? 
			GameObject p = Instantiate(authoritativePlayer);

			WorldUpdater wu = p.GetComponent<WorldUpdater>();
			wu.waterRenderer = waterRenderer;
			wu.worldManager = worldManager;

			StayOnGround sog = p.GetComponent<StayOnGround>();
			sog.island = island;

			InteractionsManager im = p.GetComponent<InteractionsManager>();
			im.interactionMap = interactionMap;
			im.highlightTile = highlightTile;
			im.indicatorsMap = indicatorsMap;

			Inventory = p.GetComponent<Inventory>();

			p.transform.position = new Vector3(x, y, 0);
		}

		public void HandleResourceMined(ResourceMinedPacket p)
		{
			$"Server gave item {p.ItemId} x{p.Amount}".Log();

			worldManager.RemoveResourceNode(p.Position);
			Inventory.Add(p.ItemId, p.Amount);
			DebugInventory();
		}

		public void HandleResourceRespawned(ResourceRespawnedPacket p)
		{
			"Received a resource respawn event".Log();
			worldManager.AddResourceNode(p.Position, p.Resource);
		}

		private void DebugInventory()
		{
			"---------- Player inventory debug ----------".Log();

			foreach (var item in Inventory.items)
			{
				$"    - {item.Key} - x{item.Value}".Log();
			}
			
			"--------------------".Log();
		}
	}
}