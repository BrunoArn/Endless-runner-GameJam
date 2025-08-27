using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    private PlayerInput playerInput;
    private Rigidbody2D rb;

    private Vector2 direction = Vector2.up + Vector2.right;
    private bool changedDirection = false;


    private void Awake()
    {
        playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();

        playerInput.Movement.ChangeDirection.performed += ctx => ChangeDirection();
    }

    void OnEnable() => playerInput.Enable();
    void OnDisable() => playerInput.Disable();

    private void FixedUpdate()
    {
        Vector2 moveDir = direction.normalized;

        rb.linearVelocity = moveDir * speed;
    }

    private void ChangeDirection()
    {

        if (!changedDirection)
        {
            direction = Vector2.down + Vector2.right;
            changedDirection = true;
        }
        else
        {
            direction = Vector2.up + Vector2.right;
            changedDirection = false;
        }
    }
}
