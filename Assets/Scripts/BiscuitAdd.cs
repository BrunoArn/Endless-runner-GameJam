using UnityEngine;

public class BiscuitAdd : MonoBehaviour
{
    public Score scoreInfo;
    [SerializeField] float scoreAmount;
    [SerializeField] GameObject particle;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (scoreInfo != null)
            scoreInfo.AddScore(scoreAmount);

        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
