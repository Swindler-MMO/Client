using System;
using Multiplayer.Packets.Server;
using Player.Authoritative;
using Swindler.Multiplayer;
using Swindler.Player.Authoritative.Inventory;
using Swindler.Player.Authoritative.Movement;
using Swindler.Utils;
using Swindler.World;
using Swindler.World.Renderers;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Swindler.Game
{
	public class GameManager : MonoBehaviour
	{

		public static GameManager Instance { get; private set; }
		public static GameServer Server { get; private set; }
		public static InventoryManager InventoryManager { get; private set; }

		[Header("Prefabs")]
		public GameObject authoritativePlayer;
		public GameObject remotePlayer;
		public GameObject inventoryPanel;
		public Text invText;

		[Header("Player prefab objects")]
		public Tilemap island;
		public WorldManager worldManager;
		public WaterRenderer waterRenderer;
		public Tilemap interactionMap;
		public Tilemap indicatorsMap;
		public AnimatedTile highlightTile;

		private bool inventoryOpen;
		
		private void Awake()
		{
			Server = CreateGameServer();
			Instance = this;
			InventoryManager = new InventoryManager();
		}

		private void Start()
		{
			Server.Connect();
			inventoryPanel.SetActive(inventoryOpen);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.I))
			{
				inventoryOpen = !inventoryOpen;
				inventoryPanel.SetActive(inventoryOpen);
			}
		}

		public void OnConnectedToGameServer()
		{
			"Connected".Log();
			
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

			InventoryUI iui = p.GetComponent<InventoryUI>();
			iui.SetInventory(InventoryManager, invText);

			p.transform.position = new Vector3(x, y, 0);
		}

		public void HandleResourceMined(ResourceMinedPacket p)
		{
			$"Server gave item {p.ItemId} x{p.Amount}".Log();
			worldManager.RemoveResourceNode(p.Position);
			InventoryManager.Add(new Item(p.ItemId, p.Amount));
		}

		public void HandleResourceRespawned(ResourceRespawnedPacket p)
		{
			"Received a resource respawn event".Log();
			worldManager.AddResourceNode(p.Position, p.Resource);
		}

		private void DebugInventory()
		{
			"---------- Player inventory debug ----------".Log();

			
			
			"--------------------".Log();
		}
	}
}