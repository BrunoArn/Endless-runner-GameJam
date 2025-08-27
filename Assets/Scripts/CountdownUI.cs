using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI countdownText;

    [Header("timing")]
    [SerializeField] int startNumber = 3;
    [SerializeField] float holdTime = 0.5f;
    [SerializeField] float fadeDuration = 0.5f;
    [SerializeField] float holdZero = 0.25f;

    [Header("Gameplay")]
    [SerializeField] PlayerMovement playerMovement;

    void OnEnable()
    {
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        playerMovement.enabled = false;

        for (int n = startNumber; n >= 0; n--)
        {
            countdownText.text = n.ToString();
            SetAlpha(1f);
            yield return new WaitForSecondsRealtime(holdTime);

            float time = 0f;
            while (time < fadeDuration)
            {
                time += Time.unscaledDeltaTime;
                float alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
                SetAlpha(alpha);
                yield return null;
            }
            SetAlpha(0f);
        }
        //hold more no 0
        if (holdZero > 0f)
        {
            yield return new WaitForSecondsRealtime(holdZero);
        }

        playerMovement.enabled = true;

        countdownText.gameObject.SetActive(false);
    }

    private void SetAlpha(float alpha)
    {
        var color = countdownText.color;
        color.a = alpha;
        countdownText.color = color;
    }
}
