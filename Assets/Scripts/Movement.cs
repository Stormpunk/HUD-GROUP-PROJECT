using System.Collections;
using System.Collections.Generic;
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
    public Rigidbody rb;
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
        staminaRegenTimer -= Time.deltaTime;
        if(staminaRegenTimer <= 0 && (rb.GetComponent<Stats>().stamina != rb.GetComponent<Stats>().maxStamina))
        {
            staminaRegenTimer = 0;
            rb.GetComponent<Stats>().stamina += Time.deltaTime;
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
}
