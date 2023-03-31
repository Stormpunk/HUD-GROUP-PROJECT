using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    #region Public Variables
    [Header("Mouse Lock")] public bool isMouseLockEnabled = true;
    [Header("Camera FOV")] public bool isFieldofViewEnabled = true;
    public float cameraFovMin;
    public float cameraFovMax;
    public float cameraFovIncrement;
    [Header("Camera Rotate X")] public float xRotateMin;
    public float xRotateMax;
    [Header("Mouse Smoothing")] public float mouseSmooth;
    #endregion
    #region Private Variables
    private float m_mouseX, m_mouseY, m_rotateX;
    private float m_mouseScroll;
    private Transform m_Parent;
    private float m_fieldOfView;
    private Camera m_camera;
    #endregion
    #region Variables for Interact
    public float interactRange = 5f;
    #endregion
    // Start is called before the first frame update
    private void Awake()
    {
        m_camera = Camera.main;
        m_Parent = transform.parent;
        if (m_camera != null)
        {
            m_fieldOfView = m_camera.fieldOfView;
        }
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
        Debug.DrawRay(m_camera.transform.position, (m_camera.transform.forward * interactRange), Color.red);
        //can confirm that the interact range is long enough.
    }
    private void FixedUpdate()
    {
        MouseInputs();
        RotatePlayerY();
        RotatePlayerX();
    }
    private void MouseInputs()
    {
        #region Collect the Mouse Inputs
        m_mouseX = Input.GetAxis("Mouse X") * mouseSmooth;
        m_mouseY = Input.GetAxis("Mouse Y") * mouseSmooth;
        m_mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        #endregion
    }
    private void RotatePlayerY()
    {
        #region Rotate Player on the Y Axis
        m_Parent.Rotate(Vector3.up.normalized * m_mouseX);
        #endregion
    }
    private void RotatePlayerX()
    {
        #region Rotate Player on the X Axis
        m_rotateX += m_mouseY;
        m_rotateX = Mathf.Clamp(m_rotateX, xRotateMin, xRotateMax);
        m_camera.transform.localRotation = Quaternion.Euler(-m_rotateX, 0, 0);
        #endregion
    }
    public void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out hit, interactRange))
        {
            hit.transform.gameObject.SendMessage("Use");
            //in theory, this will try and use the "use" function of any object hit by the raycast. Meaning that I can pick up an item, get into a vehicle or something else all depending on what I hit without having to differentiate in script
        }
        else
        {
            Debug.Log("No Useable Object");
        }
    }
}
