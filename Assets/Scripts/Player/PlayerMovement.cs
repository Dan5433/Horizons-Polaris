using InputSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    static PlayerControls input;

    [SerializeField] float speed = 5f;

    new Rigidbody rigidbody;

    void Awake()
    {
        input = new();

        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector2 movement = input.Player.Move.ReadValue<Vector2>();
        movement *= speed;
        rigidbody.linearVelocity = new(movement.x, movement.y, 0);
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
