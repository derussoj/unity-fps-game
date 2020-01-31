using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	public PlayerWeapon weapon;

    public LayerMask mask;

    private Camera cam;
    
    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Vector3 centerPointAtMaxRange = cam.transform.position + cam.transform.forward * weapon.range;
        Vector3 centerPointRelativeToCamera = cam.transform.InverseTransformPoint(centerPointAtMaxRange);
        
		Vector2 randomPoint = Random.insideUnitCircle * 0.5f;
        
		Vector3 hitPointRelativeToCamera = centerPointRelativeToCamera + new Vector3(randomPoint.x, randomPoint.y, 0f);
        Vector3 hitPoint = cam.transform.TransformPoint(hitPointRelativeToCamera);
        
		Vector3 heading = hitPoint - cam.transform.position;

        // Debug.DrawLine(cam.transform.position, hitPoint, Color.green, 20f);
        // Debug.DrawLine(cam.transform.position, centerPointAtMaxRange, Color.red, 20f);
        
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, heading, out hit, weapon.range, mask))
        {
            Debug.Log("You hit: " + hit.collider.name);

			string maskName = LayerMask.LayerToName(hit.collider.gameObject.layer);

			if (maskName == "Enemy")
			{
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
					// calculate the correct damage amount using weapon.damage, buffs, etc.
					// get the current buff status from some sort of PlayerBuffs class?

                    enemyHealth.TakeDamage(weapon.damage);
                }
			}
        }
    }
}
