using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Player Control Settings")]
    [SerializeField] private float walkSpeed = 8f;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float gravityModifier = 0.95f;
    [SerializeField] private float jumpPower = 0.25f;

    [Header("Mouse Control Options")]
    [SerializeField] float mouseSensitivity = 1f;
    [SerializeField] float maxViewAngle = 60f;


    private CharacterController characterController;

    private float currentSpeed = 8f;
    private float horizontalInput;
    private float verticalInput;

    private Vector3 heightMovement;

    private bool jump = false;

    private Transform mainCameraTransform;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (Camera.main.GetComponent<CharacterController>()==null)
        {
            Camera.main.gameObject.AddComponent<CameraController>();
        }
        mainCameraTransform = GameObject.FindWithTag("CameraPoint").transform;
    }



    void Update()
    {
        KeyboardInput();
    }


    private void FixedUpdate()
    {
        Move();

        Rotate();

    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + MouseInput().x, transform.eulerAngles.z);
        
                

        if (mainCameraTransform.eulerAngles.x>maxViewAngle && mainCameraTransform.eulerAngles.x<180f)
        {
            mainCameraTransform.rotation = Quaternion.Euler(maxViewAngle, mainCameraTransform.eulerAngles.y, mainCameraTransform.eulerAngles.z);

        }

        else if (mainCameraTransform.eulerAngles.x>180f&&mainCameraTransform.eulerAngles.x<360f-maxViewAngle)
        {
            mainCameraTransform.rotation = Quaternion.Euler(360f - maxViewAngle, mainCameraTransform.eulerAngles.y, mainCameraTransform.eulerAngles.z);
        }

        else
        {
            mainCameraTransform.rotation = Quaternion.Euler(mainCameraTransform.rotation.eulerAngles + new Vector3(-MouseInput().y, 0f, 0f));
        }
    
    
    }

    private void Move()
    {
        if (jump)
        {
            heightMovement.y = jumpPower;
            jump = false;
        }

        heightMovement.y -= gravityModifier * Time.deltaTime;

        Vector3 localVerticalVector = transform.forward * verticalInput;
        Vector3 localHorizontalVector = transform.right * horizontalInput;

        Vector3 movementVector = localHorizontalVector + localVerticalVector;
        movementVector.Normalize();
        movementVector *= currentSpeed * Time.deltaTime;
        characterController.Move(movementVector+heightMovement);

        if (characterController.isGrounded)
        {
            heightMovement.y = 0;
        }
    }

    private void KeyboardInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space)&&characterController.isGrounded)
        {
            jump = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        }

        else
        {
            currentSpeed = walkSpeed;
        }
    }

    public Vector2 MouseInput()
    {
        return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"))*mouseSensitivity;
    }

}
