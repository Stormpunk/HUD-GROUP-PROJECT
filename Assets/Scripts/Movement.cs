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
    #endregion
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 10;
        sprintSpeed = 10;
        baseSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
            rb.gameObject.GetComponent<Stats>().stamina -= (5 * Time.deltaTime);
        }
        else
        {
            speed = baseSpeed;
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
