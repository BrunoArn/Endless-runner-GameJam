using UnityEngine;

public class BiscuitAdd : MonoBehaviour
{
    [SerializeField] Score scoreInfo;
    [SerializeField] float scoreAmount;

    void OnTriggerEnter2D(Collider2D collision)
    {
        scoreInfo.AddScore(scoreAmount);
        Destroy(this.gameObject);
    }
}
