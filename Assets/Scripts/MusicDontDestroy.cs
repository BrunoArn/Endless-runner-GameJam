using UnityEngine;

public class MusicDontDestroy : MonoBehaviour
{
    private static bool musicExists = false;

    void Awake()
    {
        if (!musicExists)
        {
            musicExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
