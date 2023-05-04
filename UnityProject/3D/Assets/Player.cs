using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public FixedJoystick joystick;
    public Renderer meshRenderer;
    public GameObject bullet, gun;
    public GameLogic gameLogic;

    private float speed = 6.0f;
    private float jumpForce = 10.0f;
    private float transparency = 0.5f; // alpha value for transparency
    private float fireRate = 10f;
    private float nextfire = 0.0f;

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
            // moveDirection = Vector3.zero;
            animator.SetBool("Run", false);

            // Keyboard
            Vector3 moveDirectionKey = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            // Joystick
            Vector3 moveDirectionJoy = new Vector3(joystick.Horizontal,0.0f,joystick.Vertical);

            moveDirection = moveDirectionKey + moveDirectionJoy;
            // moveDirection = moveDirectionKey;
            
            if (moveDirection.x != 0 || moveDirection.z != 0) {
                animator.SetBool("Run", true);
                // moveDirection = transform.TransformDirection(moveDirection);
                float rotation = 180/Mathf.PI * Mathf.Atan2(moveDirection.x, moveDirection.z);
                this.transform.rotation = Quaternion.Euler(0, rotation, 0);
            }

            // Debug.Log($"rotation {rotation}");
            // Debug.Log($"this.transform.rotation {this.transform.rotation}");
            // if (Input.inputString != "") {
            //     Debug.Log($"Input.inputString {Input.inputString}");
            // }

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
        if (this.transform.position.x > 24f) {
            characterController.Move(new Vector3(-24f * 2f, moveDirection.y * Time.deltaTime, moveDirection.z * Time.deltaTime));
        } else if (this.transform.position.x < -24f) {
            characterController.Move(new Vector3(24f * 2f, moveDirection.y * Time.deltaTime, moveDirection.z * Time.deltaTime));
        }
        if (this.transform.position.z > 24f) {
            characterController.Move(new Vector3(moveDirection.x * Time.deltaTime, moveDirection.y * Time.deltaTime, -24f * 2f));
        } else if (this.transform.position.z < -24f) {
            characterController.Move(new Vector3(moveDirection.x * Time.deltaTime, moveDirection.y * Time.deltaTime, 24f * 2f));
        }

        if (Input.GetMouseButton(0) && Time.time > nextfire) {
            nextfire = Time.time + fireRate;
            Fire();
        }

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

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"OnTriggerEnter");
        if (other.gameObject.tag.StartsWith("Checkpoint")) {
            gameLogic.GetCheckPoint(other.gameObject.name);
        }
        if (other.gameObject.tag.StartsWith("Item"))
        {
            gameLogic.GetItemScore(1);
            GameObject.Destroy(other.gameObject);
        }
    }

    private void Fire()
    {
        animator.SetBool("Attack", true);
        StartCoroutine(returnState());

        Instantiate(bullet, gun.transform.position, gun.transform.rotation);
    }

    private IEnumerator returnState()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Attack", false);
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
