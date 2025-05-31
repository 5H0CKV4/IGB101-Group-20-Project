using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_ManHoCHUI : MonoBehaviour
{
    public Transform orientation;
    public Transform thirdPersonCam;
    public Transform playerObj;
    public CharacterController characterController;
    public Animator animator;

    float horizontalInput;
    float verticalInput;
    Vector3 inputDir;
    Vector3 targetVelocity;
    Vector3 velocity;

    float moveSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float playerHeight;
    public LayerMask groundLayer;
    public bool isGrounded;
    public float jumpPower;

    float idleBreakTime = 10;
    float idleTimer;

    public bool aimMode;
    public bool isDrawingArrow;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Check if player is Grounded
        isGrounded = Physics.Raycast(transform.position, -transform.up, playerHeight * .5f + .2f, groundLayer);
        animator.SetBool("IsOnGround", isGrounded);



        if (!Input.anyKey)
        {
            idleTimer += Time.deltaTime;
            //Debug.Log(idleTimer);
            if (idleTimer >= idleBreakTime)
            {
                animator.SetInteger("IdleType", Random.Range(1,4));
                idleTimer = 0;
            } 
        } else {
            animator.SetInteger("IdleType", 0);
            idleTimer = 0;
        }



        //Aiming
        if (Input.GetKeyDown(KeyCode.Q))
        {
            aimMode = !aimMode;
            animator.SetBool("AimMode", aimMode);
        }

        if (aimMode)
        {
            animator.SetLayerWeight(1, 1);
        } else {
            animator.SetLayerWeight(1, 0);
        }

        if (aimMode && !isDrawingArrow && Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("DrawArrow");
            isDrawingArrow = true;
        }
        if (aimMode && isDrawingArrow && Input.GetKeyUp(KeyCode.Mouse0))
        {
            animator.SetTrigger("ShootArrow");
            isDrawingArrow = false;
        }


        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y += jumpPower;
            characterController.Move(velocity * Time.deltaTime);
            animator.SetTrigger("JumpUp");
        }
        //Debug.Log(velocity.y);
        if (!isGrounded)
        {
            animator.SetBool("IsFalling", true);
        } else {
            animator.SetBool("IsFalling", false);
        }

        ApplyGravity();
        MovePlayer();

    }

    private void ApplyGravity()
    {
        Vector3 moveVectorGravity = Vector3.zero;
        if (isGrounded & (moveVectorGravity.y < 0.0f))
        {
            moveVectorGravity.y = -1.0f;
        } else {
            moveVectorGravity += Physics.gravity;
        }
        characterController.Move(moveVectorGravity * Time.deltaTime);
    }

    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            moveSpeed = runSpeed;
        else
            moveSpeed = walkSpeed;

        inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        inputDir = inputDir.normalized;
        targetVelocity = inputDir * moveSpeed;

        velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref velocity, .04f);
        if (velocity.magnitude < .01)
            velocity = Vector3.zero;

        animator.SetFloat("InputDirX", horizontalInput);
        animator.SetFloat("InputDirY", verticalInput);

        float speed = new Vector3(velocity.x, 0,velocity.z).magnitude;
        //Debug.Log(speed);
        characterController.Move(velocity * Time.deltaTime);
        animator.SetFloat("MoveSpeed", speed);
    }




}
