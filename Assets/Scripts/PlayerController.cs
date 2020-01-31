using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Collider playerCollider;
    private PlayerMovement playerMovement;

    [HideInInspector]
    public bool isGrounded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if (isGrounded == false)
        {
            if (rb.velocity.y < 0)
            {
                float gravityMultiplier = 2f;

                rb.velocity -= Vector3.down * Physics.gravity.y * (gravityMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (rb.velocity.y > 0)
            {
                float gravityMultiplier = 2f;

                rb.velocity -= Vector3.down * Physics.gravity.y * (gravityMultiplier - 1) * Time.fixedDeltaTime;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // or tag == "Player" or tag == "Enemy"?
        if ((playerMovement.currentJumpCharges != playerMovement.maxJumpCharges || isGrounded != true) &&
            collision.gameObject.tag == "Environment")
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                float distanceFromBase = contact.thisCollider.bounds.center.y - contact.thisCollider.bounds.extents.y - contact.point.y;

                if (-0.1f < distanceFromBase && distanceFromBase < 0.1f)
                {
                    if (Vector3.Angle(contact.normal, Vector3.up) < 60)
                    {
                        playerMovement.currentJumpCharges = playerMovement.maxJumpCharges;

                        isGrounded = true;

                        break;
                    }
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // or tag == "Player" or tag == "Enemy"?
        if (isGrounded != false && collision.gameObject.tag == "Environment")
        {
            RaycastHit hit;

            // Don't worry about Physics.SphereCast for now.
			if (Physics.Raycast(playerCollider.bounds.center, Vector3.down, out hit, playerCollider.bounds.extents.y + 0.1f) &&
                Vector3.Angle(hit.normal, Vector3.up) < 60)
            {
                isGrounded = true;
            }
			else if (Physics.Raycast(playerCollider.bounds.center + new Vector3(playerCollider.bounds.extents.x, 0f, 0f),
                Vector3.down, out hit, playerCollider.bounds.extents.y + 0.1f) &&
                Vector3.Angle(hit.normal, Vector3.up) < 60)
            {
                isGrounded = true;
            }
			else if (Physics.Raycast(playerCollider.bounds.center - new Vector3(playerCollider.bounds.extents.x, 0f, 0f),
                Vector3.down, out hit, playerCollider.bounds.extents.y + 0.1f) &&
                Vector3.Angle(hit.normal, Vector3.up) < 60)
            {
                isGrounded = true;
            }
			else if (Physics.Raycast(playerCollider.bounds.center + new Vector3(0f, 0f, playerCollider.bounds.extents.z),
                Vector3.down, out hit, playerCollider.bounds.extents.y + 0.1f) &&
                Vector3.Angle(hit.normal, Vector3.up) < 60)
            {
                isGrounded = true;
            }
			else if (Physics.Raycast(playerCollider.bounds.center - new Vector3(0f, 0f, playerCollider.bounds.extents.z),
                Vector3.down, out hit, playerCollider.bounds.extents.y + 0.1f) &&
                Vector3.Angle(hit.normal, Vector3.up) < 60)
            {
                isGrounded = true;
            }
			else if (Physics.Raycast(playerCollider.bounds.center + new Vector3(playerCollider.bounds.extents.x, 0f, playerCollider.bounds.extents.z),
                Vector3.down, out hit, playerCollider.bounds.extents.y + 0.1f) &&
                Vector3.Angle(hit.normal, Vector3.up) < 60)
            {
                isGrounded = true;
            }
			else if (Physics.Raycast(playerCollider.bounds.center - new Vector3(playerCollider.bounds.extents.x, 0f, playerCollider.bounds.extents.z),
                Vector3.down, out hit, playerCollider.bounds.extents.y + 0.1f) &&
                Vector3.Angle(hit.normal, Vector3.up) < 60)
            {
                isGrounded = true;
            }
			else if (Physics.Raycast(playerCollider.bounds.center + new Vector3(playerCollider.bounds.extents.x, 0f, -playerCollider.bounds.extents.z),
                Vector3.down, out hit, playerCollider.bounds.extents.y + 0.1f) &&
                Vector3.Angle(hit.normal, Vector3.up) < 60)
            {
                isGrounded = true;
            }
			else if (Physics.Raycast(playerCollider.bounds.center + new Vector3(-playerCollider.bounds.extents.x, 0f, playerCollider.bounds.extents.z),
                Vector3.down, out hit, playerCollider.bounds.extents.y + 0.1f) &&
                Vector3.Angle(hit.normal, Vector3.up) < 60)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
    }
}
