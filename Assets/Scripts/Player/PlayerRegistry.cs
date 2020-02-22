using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRegistry
{
	public List<Player> Players { get; } = new List<Player>();
	public List<ControlledPlayer> ControlledPlayers { get; } = new List<ControlledPlayer>();

	public void RegisterPlayer(Player player)
	{
		Players.Add(player);
		player.PlayerId = Players.Count;
	}

	public void RegisterPlayer(ControlledPlayer player)
	{
		RegisterPlayer(player as Player);
		ControlledPlayers.Add(player);
	}

	public Player RegisterPlayer(Player player, int id)
	{
		Players.Add(player);
		player.PlayerId = id;

		return player;
	}

	public ControlledPlayer RegisterPlayer(ControlledPlayer player, int id)
	{
		Players.Add(player);
		ControlledPlayers.Add(player);
		player.PlayerId = id;

		return player;
	}

	public Player GetPlayerById(int id)
	{
		return Players.Find(p => p.PlayerId == id);
	}

	public ControlledPlayer GetControlledPlayerById(int id)
	{
		return ControlledPlayers.Find(p => p.PlayerId == id);
	}
}
