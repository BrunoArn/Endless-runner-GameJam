using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    [SerializeField] Vector3 rotationUp = new(0f, 0f, 45f);
    [SerializeField] Vector3 rotationDown = new(0f, 0f, 150f);

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
    
    private void Start() {
        transform.rotation = Quaternion.Euler(rotationUp);
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
            transform.rotation = Quaternion.Euler(rotationDown);
            changedDirection = true;
        }
        else
        {
            direction = Vector2.up + Vector2.right;
            transform.rotation = Quaternion.Euler(rotationUp);
            changedDirection = false;
        }
    }
}
