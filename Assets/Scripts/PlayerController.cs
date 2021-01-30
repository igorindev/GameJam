using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    float speed = 8f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    [HideInInspector] public Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;

    [HideInInspector]
    public bool canMove = true;

    [SerializeField] AudioClip[] audioClip;
    [SerializeField] float s = 3.2f;
    AudioSource audioSource;
    Interact interact;
    Vector2 cameraValue;
    Vector2 movement;
    bool jump;
    bool crouch;

    float delayTime;

    public Interact Interact { get => interact; set => interact = value; }
    public float LookSpeed { get => Sensibility.instance.SensibilityValue * Time.deltaTime; }

    void Start()
    {
        Interact = GetComponent<Interact>();
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
    }

    public float GetSpeed()
    {
        if (Interact.HoldingItem != null)
        {
            return speed - Interact.HoldingItem.Rb.mass / 2;
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

            if (moveDirection != Vector3.zero)
            {
                if (delayTime < s)
                {
                    delayTime += GetSpeed() * Time.deltaTime;
                }
                else
                {
                    delayTime = 0;
                    audioSource.PlayOneShot(audioClip[Random.Range(0, audioClip.Length)]);
                }
            }

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
            rotation.y += cameraValue.x * LookSpeed;
            rotation.x -= cameraValue.y * LookSpeed;
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