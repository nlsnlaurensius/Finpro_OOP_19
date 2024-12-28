using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDiem : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float damage = 10.0f;
    private bool movingLeft = true; // Start moving to the left
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        // Calculate left and right boundaries
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        // Keep the original Y and Z positions to avoid changing them
        float yPos = transform.position.y;
        float zPos = transform.position.z;

        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, yPos, zPos);
                MoveDirection(1);
            }
            else
            {
                movingLeft = false; // Change direction to the right
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, yPos, zPos);
                MoveDirection(-1);
            }
            else
            {
                movingLeft = true; // Change direction to the left
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the Player
        if (collision.CompareTag("Player"))
        {
            // Get the Health component from the Player
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Player took damage: " + damage);
            }
            else
            {
                Debug.LogWarning("Player does not have a Health component.");
            }
        }
    }

    private void MoveDirection(int direction)
    {
        // Change the enemy's visual direction
        Vector3 newScale = transform.localScale;
        newScale.x = Mathf.Abs(newScale.x) * direction; // Ensure X is positive before multiplying
        transform.localScale = newScale;
    }
}
