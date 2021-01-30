using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    float speed = 8f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    [HideInInspector] public Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;

    [HideInInspector]
    public bool canMove = true;

    Interact interact;
    Vector2 cameraValue;
    Vector2 movement;
    bool jump;
    bool crouch;

    void Start()
    {
        interact = GetComponent<Interact>();
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
    }

    float GetSpeed()
    {
        if (interact.HoldingItem != null)
        {
            return speed - interact.HoldingItem.Rb.mass / 2;
        }
        else
        {
            return speed;
        }
    }


    public void Movement(Vector2 move)
    {
        movement = move;
    }
    public void Look(Vector2 look)
    {
        cameraValue = look;
    }
    public void Jump(bool value)
    {
        jump = value;
    }
    public void Crouch(bool value)
    {
        crouch = value;
    }


    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = canMove ? GetSpeed() * movement.y : 0;
            float curSpeedY = canMove ? GetSpeed() * movement.x : 0;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (jump)
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotation.y += cameraValue.x * lookSpeed;
            rotation.x -= cameraValue.y * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
        }

        if (crouch)
        {
            playerCamera.transform.parent.localPosition = Vector3.Lerp(playerCamera.transform.parent.localPosition, new Vector3(0, 1f, 0), 5 * Time.deltaTime);
        }
        else
        {
            playerCamera.transform.parent.localPosition = Vector3.Lerp(playerCamera.transform.parent.localPosition, new Vector3(0, 1.8f, 0), 5 * Time.deltaTime);
        }
    }
}