using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using UnityEngine;

public class NetworkManager : MonoBehaviour, INetEventListener
{

    private NetManager manager;
    
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
        manager = new NetManager(this);
        manager.Start();
    }

    private void Start()
    {
        manager.Connect("localhost", 2525, "testKey");
    }

    // Update is called once per frame
    void Update()
    {
        manager.PollEvents();
    }

    public void OnPeerConnected(NetPeer peer)
    {
    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
    }

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
    {
    }

    public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
    {
    }

    public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {
    }

    public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
    {
    }

    public void OnConnectionRequest(ConnectionRequest request)
    {
    }
}
