using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Header("Wallrun Elements")]

    public LayerMask theWall;
    public LayerMask theGround;
    //identifies both the wall and the ground so that the player knows where to stick to the wall as well as 
    public float wallRunSpeed;
    public float staminaDrainRate;


    [Header("Game Object")]
    public GameObject player;

    [Header("Input")]
    private float horizontalInput, verticalInput;

    [Header("Detection")]
    public float wallCheckDist;
    public float minJumpHeight;
    private RaycastHit leftWallHit, rightWallHit;
    private bool wallLeft, wallRight;

    [Header("References")]
    public Transform orientation;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        WallCheck();
        StateMachine();
    }
    private void FixedUpdate()
    {
        if(rb.GetComponent<Movement>().isWallRunning == true)
        {
            WallRunning();
            rb.GetComponent<Stats>().stamina -= staminaDrainRate;
        }
    }

    private void WallCheck()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallCheckDist, theWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallCheckDist, theWall);
        if (wallLeft)
        {
            Debug.Log("Can Wallrun on WallLeft");
        }
        else if (wallRight)
        {
            Debug.Log("Cann wallrun on wallright");
        }
    }

    private bool AboveGroundCheck()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, theGround);
    }
    private void StartWallRun()
    {
        rb.GetComponent<Movement>().isWallRunning = true;
    }

    private void WallRunning()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);
        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        rb.MovePosition(transform.position + wallForward * wallRunSpeed * Time.deltaTime);
        if (!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
        {
            rb.AddForce(-wallForward * 100, ForceMode.Force);
        }
        if (Input.GetKeyDown(KeyCode.Space) || (!wallRight && !wallLeft) || (rb.GetComponent<Stats>().stamina < 0))
        {
            EndWallRun();
        }
    }

    private void EndWallRun()
    {
        rb.GetComponent<Movement>().isWallRunning = false;
        rb.useGravity = true;
    }
    private void StateMachine()
    {
        // Getting Inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // State 1 - Wallrunning
        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround())
        {
            if (!rb.GetComponent<Movement>().isWallRunning && rb.GetComponent<Stats>().stamina > 0)
                StartWallRun();
        }

        // State 3 - None
        else
        {
            if (rb.GetComponent<Movement>().isWallRunning && rb.GetComponent<Stats>().stamina < 0)
                EndWallRun();
        }

    }
    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, theGround);
    }
}
