using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using UnityEngine;

public class GameEventListenner : INetEventListener
{
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
