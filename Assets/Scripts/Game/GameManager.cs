using System;
using System.Collections.Generic;
using Multiplayer.Packets.Server;
using Player.Authoritative;
using Player.Remote;
using Swindler.API;
using Swindler.Multiplayer;
using Swindler.Player.Authoritative.Inventory;
using Swindler.Player.Authoritative.Movement;
using Swindler.Utilities;
using Swindler.Utilities.Extensions;
using Swindler.World;
using Swindler.World.Renderers;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Swindler.Game
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance { get; private set; }
		public static GameServer Server { get; private set; }
		private static InventoryManager InventoryManager { get; set; }
		
		public static WorldManager WorldManager {get; private set; }
		
		[Header("Prefabs")] public GameObject authoritativePlayer;
		public GameObject remotePlayer;
		public GameObject inventoryPanel;
		public Text invText;
		public TMP_Text ping;

		[Header("Player prefab objects")] public Tilemap island;
		public WorldManager worldManager;
		public WaterRenderer waterRenderer;
		public Tilemap interactionMap;
		public Tilemap indicatorsMap;
		public AnimatedTile highlightTile;
		public AudioSource audioSource;

		private Dictionary<int, RemotePlayer> remotePlayers;
		private bool inventoryOpen;
		private bool canReceiveSnapshot;
		private int playerId;

		private void Awake()
		{
			Server = CreateGameServer();
			Instance = this;
			InventoryManager = new InventoryManager();
			WorldManager = worldManager;
			remotePlayers = new Dictionary<int, RemotePlayer>();
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

		public void OnDisconnectedFromGameServer()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
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
			im.audioSource = audioSource;

			InventoryUI iui = p.GetComponent<InventoryUI>();
			iui.SetInventory(InventoryManager, invText);

			p.transform.position = new Vector3(x, y, 0);
		}

		private void SpawnRemotePlayer(NetPlayer np)
		{
			GameObject p = Instantiate(remotePlayer);

			RemotePlayer rp = p.GetComponent<RemotePlayer>();
			rp.SetNetPlayer(np);
			
			remotePlayers.Add(np.Id, rp);
		}
		
		public void HandleInitialSetup(InitialSetupPacket p)
		{
			"Received an initialization packet".Log();

			playerId = p.PlayerId;
			
			foreach (NetPlayer np in p.Players)
			{
				//$"Player #{np.Id} - ({np.Name}) is at ({np.Position.x};{np.Position.y})".Log();
				SpawnRemotePlayer(np);
			}
			
			canReceiveSnapshot = true;
		}

		public void HandleResourceMined(ResourceMinedPacket p)
		{
			$"Server gave {p.ItemStack}".Log();
			InventoryManager.Add(p.ItemStack);
		}

		public void HandleResourceRespawned(ResourceRespawnedPacket p)
		{
			"Received a resource respawn event".Log();
			worldManager.AddResourceNode(p.Position, p.Resource);
		}

		public void HandlePlayerJoined(PlayerJoinedPacket p)
		{

			if (p.Player.Id == playerId)
				return;
			
			SpawnRemotePlayer(p.Player);
		}

		public void HandleGameSnapshot(GameSnapshot s)
		{
			if (!canReceiveSnapshot)
				return;
			
			foreach (var kv in s.Positions)
			{
				if(kv.Key == playerId)
					continue;

				remotePlayers[kv.Key].SetPosition(kv.Value);

			}
		}

		public void HandlePlayerDisconnect(PlayerLeftPacket p)
		{
			$"Player #{p.Id} left".Log();
			remotePlayers[p.Id].OnDisconnect();
			remotePlayers.Remove(p.Id);
		}

		public void HandleResourceRemoved(ResourceRemovedPacket p)
		{
			$"Resource {p.Position} got removed".Log();
			worldManager.RemoveResourceNode(p.Position);
		}

		public void ClearInventory()
		{
			InventoryManager.Clear();
		}

		public void DisplayPing(int latency)
		{
			ping.text = "Ping: " + latency + "ms";
		}
	}
}