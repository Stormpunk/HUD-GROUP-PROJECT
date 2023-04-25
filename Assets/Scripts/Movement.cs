using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Walk
    public float moveX;
    public float moveZ;
    public float speed;
    public float baseSpeed;
    #endregion
    #region Sprint
    public float sprintSpeed;
    public bool isSprinting;
    public float staminaRegenTimer;
    #endregion
    public enum MovementState { Idle, Walking, Jumping, WallRunning, Sprinting };
    public MovementState state;
    public Rigidbody rb;
    public bool isWallRunning;
    public float jumpspeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        isSprinting = false;
        rb = GetComponent<Rigidbody>();
        speed = 10;
        sprintSpeed = 10;
        baseSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        #region Bool and Movement 
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
        if (isSprinting)
        {
            speed = sprintSpeed;
            rb.gameObject.GetComponent<Stats>().stamina -= (5 * Time.deltaTime);
            staminaRegenTimer = 3.0f;
        }
        else
        {
            speed = baseSpeed;
            isSprinting = false;
        }
        #endregion
        #region Stamina
        if (staminaRegenTimer <= 0 && (rb.GetComponent<Stats>().stamina != rb.GetComponent<Stats>().maxStamina))
        {
            staminaRegenTimer = 0;
            rb.GetComponent<Stats>().stamina += Time.deltaTime;
        }
        if(staminaRegenTimer <= 0)
        {
            staminaRegenTimer = 0.0f;
        }
        staminaRegenTimer -= Time.deltaTime;
        if(staminaRegenTimer >= 3f && (rb.GetComponent<Stats>().stamina == rb.GetComponent<Stats>().maxStamina))
        {
            staminaRegenTimer = 3f;
        }
        #endregion
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (rb.velocity.magnitude > 0)
        {
            state = MovementState.Walking;
        }
        else if (rb.velocity.magnitude > 0 && isSprinting == true)
        {
            state = MovementState.Sprinting;
        }
        else if (rb.velocity.magnitude > 0 && isWallRunning == true)
        {
            state = MovementState.WallRunning;
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        rb.velocity = transform.TransformDirection(new Vector3(moveX * speed, rb.velocity.y, moveZ * speed));
    }
    public void Jump()
    {
        rb.AddForce(rb.transform.up * jumpspeed, ForceMode.Impulse);
        rb.GetComponent<Stats>().DrainStamina();
    }
}
