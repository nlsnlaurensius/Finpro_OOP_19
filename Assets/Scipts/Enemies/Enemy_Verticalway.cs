using UnityEngine;

public class Enemy_Verticalway : MonoBehaviour
{
    [SerializeField] private float movementDistance; // Distance for the enemy to move up and down
    [SerializeField] private float speed; // Speed of movement
    [SerializeField] private float damage; // Damage dealt to the player
    private bool movingUp; // Direction flag for movement
    private float topEdge; // Upper boundary for movement
    private float bottomEdge; // Lower boundary for movement

    private void Awake()
    {
        // Correctly calculate top and bottom boundaries
        topEdge = transform.position.y + movementDistance;
        bottomEdge = transform.position.y - movementDistance;
    }

    private void Update()
    {
        // Move the enemy up or down based on the current direction
        if (movingUp)
        {
            if (transform.position.y < topEdge) // Move up
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingUp = false; // Switch direction
            }
        }
        else
        {
            if (transform.position.y > bottomEdge) // Move down
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingUp = true; // Switch direction
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ensure collision is with a Player
        if (collision.CompareTag("Player"))
        {
            // Check if the Player has a Health component before applying damage
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("The Player object is missing a Health component.");
            }
        }
    }
}
