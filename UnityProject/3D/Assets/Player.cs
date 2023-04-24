using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public FixedJoystick joystick;
    public Renderer meshRenderer;
    public float speed = 6.0f;
    public float jumpForce = 10.0f;
    public float transparency = 0.1f; // alpha value for transparency

    private Vector3 moveDirection = Vector3.zero;

    // CharacterController controller;
    // public Vector3 playerVelocity;
    // public bool groundedPlayer;
    // public float playerSpeed = 2.0f;
    // public float jumpHeight = 1.0f;
    // public float gravityValue = -9.81f;

    // public float jumpForce = 10.0f;
    // private Rigidbody rb;

    CharacterController characterController;
    Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        // controller = gameObject.AddComponent<CharacterController>();
        // rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (characterController.isGrounded) {
            moveDirection = Vector3.zero;
            animator.SetBool("Run", false);

            // Ketboard
            Vector3 moveDirectionKey = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            // Joystick
            Vector3 moveDirectionJoy = new Vector3(joystick.Horizontal,0.0f,joystick.Vertical);

            moveDirection = moveDirectionKey + moveDirectionJoy;
            // moveDirection = moveDirectionKey;
            
            if (moveDirection.x != 0 || moveDirection.z != 0) {
                animator.SetBool("Run", true);
            }

            // moveDirection = transform.TransformDirection(moveDirection);
            float rotation = 180/Mathf.PI * Mathf.Atan2(moveDirection.x, moveDirection.z);
            this.transform.rotation = Quaternion.Euler(0, rotation, 0);
            // Debug.Log($"rotation {rotation}");
            // Debug.Log($"this.transform.rotation {this.transform.rotation}");
            if (Input.inputString != "") {
                Debug.Log($"Input.inputString {Input.inputString}");
            }

            moveDirection *= speed;
            
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                Color color = meshRenderer.material.color;
                color.a = transparency;
                meshRenderer.material.color = color;
                Debug.Log($"meshRenderer.material.color.a {meshRenderer.material.color.a}");
            } else if (Input.GetKey(KeyCode.R))
            {
                Color color = meshRenderer.material.color;
                color.a = 1.0f;
                meshRenderer.material.color = color;
                Debug.Log($"meshRenderer.material.color.a {meshRenderer.material.color.a}");
            }
        // }
        characterController.Move(moveDirection * Time.deltaTime);

        // groundedPlayer = controller.isGrounded;
        // if (groundedPlayer && playerVelocity.y < 0)
        // {
        //     playerVelocity.y = 0f;
        // }

        // Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // controller.Move(move * Time.deltaTime * playerSpeed);

        // if (move != Vector3.zero)
        // {
        //     gameObject.transform.forward = move;
        // }

        // // Changes the height position of the player..
        // if (Input.GetButtonDown("Jump") && groundedPlayer)
        // {
        //     playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        // }

        // playerVelocity.y += gravityValue * Time.deltaTime;
        // controller.Move(playerVelocity * Time.deltaTime);
    }

    // void FixedUpdate()
    // {
    //     float moveHorizontal = Input.GetAxis("Horizontal");
    //     float moveVertical = Input.GetAxis("Vertical");

    //     Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

    //     rb.AddForce(movement * speed);

    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //     }
    // }
}
