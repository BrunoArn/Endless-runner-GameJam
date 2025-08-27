using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Score : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float scoreMultiplier;
    private TextMeshProUGUI text;

    private Vector3 lastPosition;
    private float score = 0;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (playerTransform == null) playerTransform = transform;
        lastPosition = playerTransform.position;
    }

    void Update()
    {
        float distanceMoved = Vector3.Distance(playerTransform.position, lastPosition);
        score += distanceMoved * scoreMultiplier;
        lastPosition = playerTransform.position;

        text.text = Mathf.FloorToInt(score).ToString();
    }

    public void AddScore(float amount)
    {
        score += amount;
    }
}
