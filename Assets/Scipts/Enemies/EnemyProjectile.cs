using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float resetTime = 3f;
    
    private float lifetime;
    private float speed;
    private float direction;
    private bool isInitialized;
    
    public void ActivateProjectile(float projectileSpeed)
    {
        lifetime = 0;
        speed = projectileSpeed;
        isInitialized = true;
        gameObject.SetActive(true);
    }
    
    private void Update()
    {
        if (!isInitialized) return;
        
        // Bergerak berdasarkan direction
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
        
        // Reset projectile setelah waktu tertentu
        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
        {
            gameObject.SetActive(false);
            isInitialized = false;
        }
    }
    
    public void SetDirection(float _direction)
    {
        direction = _direction;
        
        // Atur skala untuk menghadap arah yang benar
        Vector3 localScale = transform.localScale;
        if (Mathf.Sign(localScale.x) != direction)
        {
            localScale.x = -localScale.x;
        }
        transform.localScale = localScale;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        
        // Nonaktifkan projectile saat mengenai apapun kecuali trigger lainnya
        if (!collision.isTrigger)
        {
            gameObject.SetActive(false);
            isInitialized = false;
        }
    }
}