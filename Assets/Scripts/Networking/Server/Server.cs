using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Zenject;

public class Server : IInitializable
{
    private UdpConnection _connection;
    private MessageProcessor _messageProcessor;
    private MessageSerializer _serializer;
    private Settings _settings;
	private GameLoop _loop;

    private Dictionary<string, ConnectedClient> _connectedClients = new Dictionary<string, ConnectedClient>();

    public Server(
		MessageProcessor messageHandler,
		MessageSerializer serializer,
		Settings settings,
		GameLoop loop
	)
    {
        _messageProcessor = messageHandler;
        _serializer = serializer;
        _settings = settings;
		_loop = loop;
	}

    public void Initialize()
    {
        _connection = new UdpConnection(_settings.port);
        _connection.Listen(OnMessageReceived);
    }

    public void RegisterClient(string clientId, IPEndPoint remote)
    {
        if (!_connectedClients.ContainsKey(clientId))
        {
            _connectedClients.Add(clientId, new ConnectedClient(remote));
        }
    }

    public bool HasClient(string clientId)
    {
        return _connectedClients.ContainsKey(clientId);
    }

	public void SendMessage(IUdpMessage message, ConnectedClient client)
	{
		var bytes = _serializer.SerializeMessage(message);
		_connection.Send(client.Remote, bytes);
	}

	public void SendMessage(IUdpMessage message, IPEndPoint remote)
	{
		var bytes = _serializer.SerializeMessage(message);
		_connection.Send(remote, bytes);
	}

    public void SendToAll(IUdpMessage message)
    {
        var bytes = _serializer.SerializeMessage(message);

        foreach (KeyValuePair<string, ConnectedClient> entry in _connectedClients)
        {
            _connection.Send(entry.Value.Remote, bytes);
        }
    }

    private void OnMessageReceived(IPEndPoint endpoint, byte[] bytes)
    {
        var message = _serializer.ParseMessage(endpoint, bytes);

        if (message != null)
        {
            _messageProcessor.AddMessage(message);
        }

        _connection.Listen(OnMessageReceived);
    }

    [Serializable]
    public class Settings
    {
        public int port = 54123;
    }
}
