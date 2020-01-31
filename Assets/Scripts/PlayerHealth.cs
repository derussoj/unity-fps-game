using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	private PlayerMovement playerMovement;
	private PlayerShooting playerShooting;
    private SpawnManager spawnManager;

	private float baseHealth = 300f;
	private float gearedHealth;
	private float currentHealth;

	private bool isTakingDamage;
	private bool isDead;

	private void Awake()
	{
		playerMovement = GetComponent<PlayerMovement>();
		playerShooting = GetComponent<PlayerShooting>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

		// get any additional health from gear, subclass perks, etc.
		// (keep track of that in some sort of PlayerStats class?)
		// gearedHealth = baseHealth + healthFromGear;
		gearedHealth = baseHealth;

		currentHealth = gearedHealth;
	}

	private void Update()
	{
		if (isTakingDamage)
		{
			// set the screen (border) to a semitransparent red

			isTakingDamage = false;
		}
		else
		{
			// transition the screen (border) to clear
			// if the screen (border) is not already clear
		}
	}

	public void TakeDamage(float damageReceived)
	{
		if (!isDead)
		{
			isTakingDamage = true;

			currentHealth -= damageReceived;

			// update the UI
			// from 100% to 40% health, the bar should be the same color as the rest of the UI
			// (likely a semitransparent white)
			// at 40% health, the UI zooms in on the remaining health
			// so that 40% health appears as full health
			// except that the bar is now a semitransparent red

			// play a sound

			if (currentHealth <= 0)
			{
				Death();
			}
		}
	}

	private void Death()
	{
		isDead = true;

        // animation, camera change, UI change, etc.

		playerMovement.enabled = false;
		playerShooting.enabled = false;

        // disable the player's collider?

        StartCoroutine(Respawn());
	}

    private IEnumerator Respawn()
    {
        // eventually, this will have to be much more complicated
        // PVE - reset the scene and respawn the player
        // PVP - respawn the player
        // allow for reviving
        // and for different respawn delays

        float respawnDelay = 3f;

        yield return new WaitForSeconds(respawnDelay);

        isDead = false;

        // animation, camera change, UI change, etc.

        playerMovement.enabled = true;
        playerShooting.enabled = true;

        // enable the player's collider?

        transform.position = spawnManager.spawnPoint.position;
        transform.rotation = spawnManager.spawnPoint.rotation;
    }
}
