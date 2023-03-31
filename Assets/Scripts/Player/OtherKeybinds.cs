using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherKeybinds : MonoBehaviour
{
    public GameObject inventoryPanel;
    private bool toggleState;
    // Start is called before the first frame update
    void Start()
    {
        toggleState = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            toggleState = !toggleState;
            inventoryPanel.SetActive(toggleState);
        }
    }
}
