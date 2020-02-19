using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInputSystem : IInitializable, IUpdatable
{
	private RemoteClient _client;
	private GameLoop _loop;

	public PlayerInputSystem(RemoteClient client, GameLoop loop)
	{
		_client = client;
		_loop = loop;
	}

	public void Initialize()
	{
		_loop.Subscribe(this);
	}

	public void Simulate(uint tickIndex)
	{
		if (_client.IsConnected)
		{
			bool up = Input.GetKey(KeyCode.W);
			bool right = Input.GetKey(KeyCode.D);
			bool down = Input.GetKey(KeyCode.S);
			bool left = Input.GetKey(KeyCode.A);

			if (up || right || down || left)
			{
				var inputMessage = new PlayerInputMessage(up, right, down, left);
				_client.SendMessage(inputMessage);
			}
		}
	}
}