using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] float duration;

    public void MusicDeath()
    {
        StartCoroutine(FadeOutMusicRoutine());
    }

    private IEnumerator FadeOutMusicRoutine()
    {
        float startPitch = musicSource.pitch;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            musicSource.pitch = Mathf.Lerp(startPitch, 0f, time / duration);
            yield return null;
        }

        musicSource.Stop();
    }
}
