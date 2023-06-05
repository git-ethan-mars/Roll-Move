using Data;

public interface ITurnOrder
{
    public PlayerData GetNextPlayer();
    public void RemovePlayer(PlayerData player);
}