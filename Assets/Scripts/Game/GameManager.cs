using System;
using Swindler.Multiplayer;
using Swindler.Player.Authoritative.Movement;
using Swindler.Utils;
using Swindler.World;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Swindler.Game
{
	public class GameManager : MonoBehaviour
	{

		public static GameManager Instance { get; private set; }
		public static GameServer Server { get; private set; }
		
		[Header("Prefabs")]
		public GameObject authoritativePlayer;
		public GameObject remotePlayer;

		[Header("Player prefab objects")]
		public Tilemap island;
		public WorldManager worldManager;
		public WaterRenderer waterRenderer;

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
			SpawnAuthoritativePlayer(5010, 5025);
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
			
			p.transform.position = new Vector3(x, y, 0);
		}
		
	}
}