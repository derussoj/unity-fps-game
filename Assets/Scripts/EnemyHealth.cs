using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float baseHealth;

    private float scaledHealth;
    private float currentHealth;

    private bool isDead;

    private void Awake()
    {
        // get the current difficulty level
        // (keep track of that in PlayerController?)
        // scaledHealth = baseHealth * difficultyScalingFactor;
        scaledHealth = baseHealth;
        
        currentHealth = scaledHealth;
    }

    public void TakeDamage(float damageReceived)
    {
        if (!isDead)
        {
			// get the current debuff status from some sort of Debuffs class?
			// and use that to adjust the damage amount

            currentHealth -= damageReceived;

            if (currentHealth <= 0)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        isDead = true;

        // animation

        Destroy(gameObject);
    }
}
