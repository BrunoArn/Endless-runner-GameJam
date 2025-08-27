using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] int sceneIndex;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
