using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float threasholdLa = 0.1f;

    private PlayerInput playerInput;
    private Rigidbody2D rb;

    private Vector2 direction = Vector2.up + Vector2.right;
    private bool changedDirection = false;

    private bool grounded = false;
    private Vector2 lastContactNormal;

    //Debug features
    private Vector3 initialPosition;

    private void Awake()
    {
        playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();

        playerInput.Movement.ChangeDirection.performed += ctx => ChangeDirection();


        //debug carai
        initialPosition = this.transform.position;
    }

    void OnEnable() => playerInput.Enable();
    void OnDisable() => playerInput.Disable();

    private void FixedUpdate()
    {
        Vector2 moveDir = direction.normalized;

        if (grounded)
        {
            Vector2 surfaceTangent = new(-lastContactNormal.y, lastContactNormal.x);
            moveDir = Vector2.Dot(moveDir, surfaceTangent) * surfaceTangent;
        }
        rb.linearVelocity = moveDir * speed;

        if (rb.linearVelocity.magnitude < 0.5f)
        {
            Death();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);

        if (Mathf.Abs(contact.normal.x) > threasholdLa)
        {
            Death();
            return;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            ContactPoint2D contact = collision.GetContact(0);
            lastContactNormal = contact.normal;
            grounded = true;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    private void ChangeDirection()
    {
        grounded = false;
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

    private void Death()
    {
        this.transform.position = initialPosition;
    }
}
