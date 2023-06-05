namespace Data
{
    public class PlayerStatistic
    {
        public int? Place { get; set; }
        public string Name { get; set; }
        public int MoveNumber { get; set; }
        public int BonusNumber { get; set; }
        public int PenaltyNumber { get; set; }

        public PlayerStatistic(string name)
        {
            Name = name;
        }
    }
}