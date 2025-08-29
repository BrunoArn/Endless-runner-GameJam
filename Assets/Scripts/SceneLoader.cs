using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] int sceneIndex;
    [SerializeField] AudioClip clickAudio;
    private AudioSource audioSource;

    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeScene()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clickAudio);
            float delay = clickAudio.length / 2;
            Invoke(nameof(LoadScene), delay);
        }
        else
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
