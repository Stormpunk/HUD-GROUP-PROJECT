using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float health = 3;
    public float maxHealth = 3;
    public float stamina = 100;
    public float maxStamina = 100;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            DrainHealth();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            DrainStamina();
        }
    }
    public void DrainStamina()
    {
        stamina -= 10;
    }
    public void DrainHealth()
    {
        health -= 1;
    }
    public void HealHealth()
    {
        health += 1;
    }
}
