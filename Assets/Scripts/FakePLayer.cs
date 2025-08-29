
using UnityEngine;

public class FakePLayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction = Vector2.right;
    [SerializeField] float speed = 9f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 moveDir = direction.normalized;
        rb.linearVelocity = moveDir * speed;
    }
}
