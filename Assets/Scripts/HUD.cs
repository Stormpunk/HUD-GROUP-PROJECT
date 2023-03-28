using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public Image healthBar;
    public Image staminaBar;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = player.GetComponent<Stats>().health / 3;
        staminaBar.fillAmount = player.GetComponent<Stats>().stamina / 100;
    }
}
