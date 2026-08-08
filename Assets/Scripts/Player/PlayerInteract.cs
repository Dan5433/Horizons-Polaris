using InputSystem;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    static PlayerControls input;

    [SerializeField] float reachDistance = 1f;

    void Awake()
    {
        input = new();
    }

    void Update()
    {
        RotatePlayerToMouse();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, reachDistance);
        if (!hit)
            return;

        if (!input.Player.Interact.WasPressedThisFrame())
            return;

        if (!hit.transform.TryGetComponent<IInteractable>(out var interactable))
            return;

        interactable.Interact();
    }

    void RotatePlayerToMouse()
    {
        Vector2 mousePos = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        transform.up = direction;
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