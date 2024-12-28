using UnityEngine;
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float damage = 0.5f;
    private float direction;
    private bool hit;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        hit = false;
        boxCollider.enabled = true;
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit) return;

        hit = true;
        boxCollider.enabled = false;

        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log($"Hit enemy! Damage dealt: {damage}"); // Debug log untuk memverifikasi hit
                SoundManager.PlaySFX("explosion");
            }
        }

        // Trigger animasi ledakan
        anim.SetTrigger("explode");
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        
        // Atur skala untuk menghadap arah yang benar
        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x) * Mathf.Sign(_direction);
        transform.localScale = localScale;
    }

    // Dipanggil di akhir animasi explode
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}