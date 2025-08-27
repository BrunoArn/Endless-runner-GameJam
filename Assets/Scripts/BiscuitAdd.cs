using UnityEngine;

public class BiscuitAdd : MonoBehaviour
{
    [SerializeField] Score scoreInfo;
    [SerializeField] float scoreAmount;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (scoreInfo != null)
            scoreInfo.AddScore(scoreAmount);
            
        Destroy(this.gameObject);
    }
}
