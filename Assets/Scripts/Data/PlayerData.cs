using UnityEngine;

namespace Data
{
    public class PlayerData
    {
        public readonly string Name;
        public readonly Color Color;
        public PlayerFigure Figure { get; set; }
        public PlayerStatistic Statistic { get; private set; }
        public int CellIndex { get; set; }

        public PlayerData(string name, Color color)
        {
            Name = name;
            Color = color;
            Statistic = new PlayerStatistic(name);
        }
    }
}