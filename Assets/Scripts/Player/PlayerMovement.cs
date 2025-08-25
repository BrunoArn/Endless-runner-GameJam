using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    private Vector2 direction = Vector2.up + Vector2.right;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Movement.ChangeDirection.performed += ChangeDirection;

    }

    void OnEnable()
    {
        playerInput.Enable();
    }
    void OnDisable()
    {
        playerInput.Disable();
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * direction.normalized);

    }
    private void ChangeDirection(InputAction.CallbackContext context)
    {
        if (direction == Vector2.up + Vector2.right)
        {
            direction = Vector2.up + Vector2.left;
        }
        else
        {
            direction = Vector2.up + Vector2.right;
        }
    }
}
