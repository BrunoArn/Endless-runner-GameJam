using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Vector3 initialPosition;

    private void Start() {
        initialPosition = transform.position;
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
        transform.position = initialPosition;
    }
}
