using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float WalkSpeed = 6f;
    public float SprintSpeed = 10f;
    private float CurrentSpeed;
    Vector3 velocity;
    bool isGrounded;
    bool IsWalking = false;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    void Start()
    {
        CurrentSpeed = WalkSpeed;
    }

    void Update()
    {
    // Grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }
        //Walking Check

        IsWalking = CurrentSpeed > 0f;


    // Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            CurrentSpeed = SprintSpeed;
        }
        else
        {
            CurrentSpeed = WalkSpeed;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * CurrentSpeed * Time.deltaTime);

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
