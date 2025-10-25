using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float WalkSpeed = 6f;
    public float SprintSpeed = 10f;
    private float CurrentSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentSpeed = WalkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
