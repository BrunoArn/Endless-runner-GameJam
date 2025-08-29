using UnityEngine;

public class BiscuitAdd : MonoBehaviour
{
    public Score scoreInfo;
    [SerializeField] float scoreAmount;
    [SerializeField] GameObject particle;

    private AudioSource audioSource;
    [SerializeField] AudioClip audioClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (scoreInfo != null)
            scoreInfo.AddScore(scoreAmount);

        if (collision.TryGetComponent<Animator>(out var playerAnimator))
        {
            playerAnimator.SetTrigger("Eating");
        }

        audioSource.PlayOneShot(audioClip);
        Instantiate(particle, transform.position, Quaternion.identity);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this.gameObject, audioClip.length);
    }
}
