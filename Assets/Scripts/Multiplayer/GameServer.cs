﻿using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using Multiplayer.Packets;
using Multiplayer.Packets.Server;
using Swindler.Game;
using Swindler.Utils;
using UnityEngine;

namespace Swindler.Multiplayer
{
    public class GameServer : MonoBehaviour, INetEventListener
    {
    
        [Header("Server details")]
        public string serverHost = "localhost";
        public int serverPort = 2525;
    
        [Header("Game manager")] public GameManager game;
        
        private NetManager manager;
        private NetPeer server;
        
        // Start is called before the first frame update
        private void Awake()
        {
            manager = new NetManager(this);
            manager.Start();
        }
    
        public void Connect()
        {
            new PlayerPositionPacket(Vector3.one).Serialize();
            manager.Connect(serverHost, serverPort, new PlayerAuthPacket("Aayyi").Serialize());
        }
    
        private void Update()
        {
            manager.PollEvents();
        }
    
        public void OnPeerConnected(NetPeer peer)
        {
            server = peer;
            game.OnConnectedToGameServer();
        }
    
        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            $"Disconnected for: {disconnectInfo.AdditionalData.GetString()}".Log();
        }
    
        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {
            "An error occured".Log();
        }
    
        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            "Got packet".Log();
            short packetId = reader.GetShort();

            switch (packetId)
            {
                case 1:
                    GameManager.Instance.HandleResourceMined(new ResourceMinedPacket(reader));
                    break;
                case 2:
                    GameManager.Instance.HandleResourceRespawned(new ResourceRespawnedPacket(reader));
                    break;
            }
        }
    
        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
        }
    
        public void OnConnectionRequest(ConnectionRequest request)
        {
            
        }
        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {
        }
    
        public void Send(SwindlerPacket packet, DeliveryMethod method)
        {
            server.Send(packet.Serialize(), method);
        }
    }

}