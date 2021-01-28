using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandle : MonoBehaviour
{
    [SerializeField] PlayerController movement;
    [SerializeField] Interact interact;

    PlayerInput inputActions;

    void Awake()
    {
        inputActions = new PlayerInput();

        inputActions.Player.Camera.performed += ctx => movement.Look(ctx.ReadValue<Vector2>());
        inputActions.Player.Camera.canceled += ctx => movement.Look(Vector2.zero);

        inputActions.Player.Movement.performed += ctx => movement.Movement(ctx.ReadValue<Vector2>());
        inputActions.Player.Movement.canceled += ctx => movement.Movement(Vector2.zero);

        inputActions.Player.Jump.performed += ctx => movement.Jump(true);
        inputActions.Player.Jump.canceled += ctx => movement.Jump(false);

        inputActions.Player.Interact.performed += ctx => interact.InteractWithItem(true);
        inputActions.Player.Interact.canceled += ctx => interact.InteractWithItem(false);

        inputActions.Player.Throw.performed += ctx => interact.Throw();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        inputActions.Disable();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
