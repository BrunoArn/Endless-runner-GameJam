using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] GameObject ui;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Score scoreInfo;

    public void ActivateGameOver()
    {
        this.ui.SetActive(true);
        scoreText.text = $"Cookies Gathered [{scoreInfo.GetCookiesAmount()}] \t cookies Score [{scoreInfo.GetCookiesScore()}]\nDistance traveled [{scoreInfo.GetDistanceMoved()}] \t distance score[{(int)scoreInfo.GetDistanceScore()}]";
    }
}
