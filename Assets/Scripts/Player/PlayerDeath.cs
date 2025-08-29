using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    
    [SerializeField] GameEvent playerDeath;
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnDisable()
    {
        if (this.gameObject.TryGetComponent<TrailRenderer>(out var trail))
        {
            trail.enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Die();
        }
    }

    private void Die()
    {
        playerDeath.Raise();
        audioSource.Play();
        if (this.gameObject.TryGetComponent<PlayerMovement>(out var movement))
        {
            movement.ZeroMovement();
            movement.enabled = false;
        }
    }
}
