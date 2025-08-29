using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] GameObject ui;
    [SerializeField] TextMeshProUGUI DistanceTraveled;
    [SerializeField] TextMeshProUGUI DistanceScore;
    [SerializeField] TextMeshProUGUI CookiesGathered;
    [SerializeField] TextMeshProUGUI CookiesScore;
    [SerializeField] Score scoreInfo;

    public void ActivateGameOver()
    {
        this.ui.SetActive(true);
        DistanceTraveled.text = $"Distance traveled\n\n[{(int)scoreInfo.GetDistanceMoved()}]";
        DistanceScore.text = $"Distance score\n\n[{(int)scoreInfo.GetDistanceScore()}]";
        CookiesGathered.text = $"Cookies Gathered\n\n[{scoreInfo.GetCookiesAmount()}]";
        CookiesScore.text = $"Cookies Score\n\n[{scoreInfo.GetCookiesScore()}]";
    }
}
