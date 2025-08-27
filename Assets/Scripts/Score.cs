using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float scoreMultiplier;
    [SerializeField] TextMeshProUGUI text;

    private Vector3 lastPosition;
    private float distanceScore = 0f;
    private float distanceTraveled = 0f;
    private float cookieScore = 0f;
    private int cookiesGathered = 0;

    void Start()
    {
        lastPosition = playerTransform.position;
    }

    void Update()
    {
        float distanceMoved = Vector3.Distance(playerTransform.position, lastPosition);
        distanceTraveled += distanceMoved;
        distanceScore += distanceMoved * scoreMultiplier;
        lastPosition = playerTransform.position;

        text.text = Mathf.FloorToInt(distanceScore + cookieScore).ToString();
    }

    public void AddScore(float amount)
    {
        cookieScore += amount;
        cookiesGathered++;
    }

    public float GetCookiesScore()
    {
        return cookieScore;
    }

    public int GetCookiesAmount()
    {
        return cookiesGathered;
    }

    public float GetDistanceScore()
    {
        return distanceScore;
    }
    public float GetDistanceMoved()
    {
        return distanceTraveled;
    }
}
