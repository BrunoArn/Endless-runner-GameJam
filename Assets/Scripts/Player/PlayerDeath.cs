using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    
    [SerializeField] GameEvent playerDeath;

    private void Start() {
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
        if (this.gameObject.TryGetComponent<PlayerMovement>(out var movement))
        {
            movement.ZeroMovement();
            movement.enabled = false;
        }
    }
}
