using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 5f;
    [SerializeField] private float damage = 0.3f;
    
    [Header("Range Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireball;
    [SerializeField] private float projectileSpeed = 10f; // Tambahkan variabel kecepatan
    
    private float cooldownTimer = 0f;
    
    private void Start()
    {
        // Menghadap ke kiri
        Vector3 newScale = transform.localScale;
        newScale.x = -Mathf.Abs(newScale.x);
        transform.localScale = newScale;
    }
    
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        
        if (cooldownTimer >= attackCooldown)
        {
            RangedAttack();
        }
    }
    
    private void RangedAttack()
    {
        cooldownTimer = 0;
        int projectileIndex = FindFireball();
        
        // Pastikan firePoint dan fireball array tidak null dan memiliki elemen
        if (firePoint != null && fireball != null && fireball.Length > 0)
        {
            // Aktifkan dan posisikan fireball
            GameObject currentFireball = fireball[projectileIndex];
            currentFireball.transform.position = firePoint.position;
            
            // Dapatkan komponen EnemyProjectile
            var projectile = currentFireball.GetComponent<EnemyProjectile>();
            if (projectile != null)
            {
                projectile.SetDirection(transform.localScale.x < 0 ? 1 : -1);
                projectile.ActivateProjectile(projectileSpeed);
            }
        }
    }
    
    private int FindFireball()
    {
        for (int i = 0; i < fireball.Length; i++)
        {
            if (!fireball[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Health playerHealth = collision.collider.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Player terkena damage sebesar " + damage);
            }
            SoundManager.PlaySFX("hit");
        }
    }
}