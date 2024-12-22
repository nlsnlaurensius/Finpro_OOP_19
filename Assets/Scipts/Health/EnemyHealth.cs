using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        Debug.Log("Enemy Terkena Damage. Sisa Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

   private void Die()
    {
        dead = true;
        Debug.Log("Enemy Mati");
        Destroy(gameObject);
    }
}