using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoreStats : MonoBehaviour
{
    private float baseHealth, bonusHealth;
    public float maxHealth;
    private float baseStamina, bonusStamina;
    public float maxStamina;
    public float currentHealth, currentStamina;
    // Start is called before the first frame update
    private void Awake()
    {
        baseHealth = 100;
        bonusHealth = 0;
        baseStamina = 100;
        bonusStamina = 0;
    }
    void Start()
    {
        RecalculateStats();
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("."))
        {
            currentHealth -= 10;
        }
    }
    public void RecalculateStats()
    {
        maxHealth = baseHealth + bonusHealth;
        maxStamina = baseStamina + bonusStamina;
    }
}
