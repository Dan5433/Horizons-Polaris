using InputSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    static PlayerControls input;

    [SerializeField] float speed = 5f;

    new Rigidbody2D rigidbody;

    void Awake()
    {
        input = new();

        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 movement = input.Player.Move.ReadValue<Vector2>();
        movement *= speed;
        rigidbody.linearVelocity = movement;
    }

    void OnEnable()
    {
        input.Enable();
    }
    void OnDisable()
    {
        input.Disable();
    }
}
