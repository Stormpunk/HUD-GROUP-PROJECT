using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatBars : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Image healthBar, staminaBar;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void LateUpdate()
    {
        healthBar.fillAmount = player.GetComponent<PlayerCoreStats>().currentHealth / player.GetComponent<PlayerCoreStats>().maxHealth;
    }
}
