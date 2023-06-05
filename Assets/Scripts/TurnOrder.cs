using System.Collections.Generic;
using System.Linq;
using Data;

public class TurnOrder : ITurnOrder
{
    private readonly List<PlayerData> _players;
    public TurnOrder(PlayerData[] players)
    {
        _players = new List<PlayerData>(players);
    }


    public PlayerData GetNextPlayer()
    {
        var player = _players.First();
        _players.Remove(player);
        _players.Add(player);
        return player;
    }

    public void RemovePlayer(PlayerData player)
    {
        _players.Remove(player);
    }
}