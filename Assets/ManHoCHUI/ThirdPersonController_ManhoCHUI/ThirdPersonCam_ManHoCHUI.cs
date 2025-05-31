using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam_ManHoCHUI : MonoBehaviour
{

    [Header("Reference")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    //public Rigidbody rb;
    public CharacterController characterController;
    public Animator animator;

    PlayerMovement_ManHoCHUI playerMovement;

    public float rotationSpeed;

    private Vector3 inputDir;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerMovement = player.GetComponent<PlayerMovement_ManHoCHUI>();
    }

    // Update is called once per frame
    void Update()
    {
 

        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (playerMovement.aimMode)
        {
            float angle = Vector3.SignedAngle(playerObj.forward, orientation.forward, Vector3.up);
            //Debug.Log(angle);
            animator.SetFloat("TurnAngle", angle);
            
            if (angle > 0)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, orientation.forward, Time.deltaTime * rotationSpeed);
            } else if (angle < 0)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, orientation.forward, Time.deltaTime * rotationSpeed);
            }
            return;
        }
        if (inputDir != Vector3.zero)
        {

            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            
        }

    }



}
