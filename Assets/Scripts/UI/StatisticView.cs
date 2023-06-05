using Data;
using UnityEngine;

namespace UI
{
    public class StatisticView : MonoBehaviour
    {
        [SerializeField] private PlayerStatisticView[] playerStatisticViews;
        public void Construct(PlayerStatistic[] statistics)
        {
            for (var i = 0; i < statistics.Length; i++)
            {
                playerStatisticViews[i].place.SetText(statistics[i].Place!.Value.ToString());
                playerStatisticViews[i].playerName.SetText(statistics[i].Name);
                playerStatisticViews[i].moveNumber.SetText(statistics[i].MoveNumber.ToString());
                playerStatisticViews[i].bonusNumber.SetText(statistics[i].BonusNumber.ToString());
                playerStatisticViews[i].penaltyNumber.SetText(statistics[i].PenaltyNumber.ToString());
            }
        }
    }
}