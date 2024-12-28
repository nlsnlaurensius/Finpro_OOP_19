using UnityEngine;

public class DynamicEnemy : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float damage = 10.0f;
    [SerializeField] private float damageCooldown = 1f; // Cooldown antara damage

    private bool movingLeft = true;
    private float leftEdge;
    private float rightEdge;
    private float lastDamageTime; // Waktu terakhir memberikan damage

    private Animator anim; // Animator reference

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        anim = GetComponent<Animator>(); // Initialize Animator
    }

    private void Update()
    {
        float yPos = transform.position.y;
        float zPos = transform.position.z;

        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, yPos, zPos);
                FlipSprite(false);
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, yPos, zPos);
                FlipSprite(true);
            }
            else
            {
                movingLeft = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DealDamageToPlayer(collision);
        TriggerAttackAnimation();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Memberikan damage berkelanjutan dengan cooldown
        if (Time.time >= lastDamageTime + damageCooldown)
        {
            DealDamageToPlayer(collision);
            TriggerAttackAnimation();
        }
    }

    private void DealDamageToPlayer(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                lastDamageTime = Time.time;
                Debug.Log("Player terkena damage sebesar " + damage);
            }
            SoundManager.PlaySFX("hit");
        }
    }

    private void FlipSprite(bool facingRight)
    {
        Vector3 newScale = transform.localScale;
        newScale.x = Mathf.Abs(newScale.x) * (facingRight ? 1 : -1);
        transform.localScale = newScale;
    }

    private void TriggerAttackAnimation()
    {
        if (anim != null)
        {
            anim.SetTrigger("attack");
        }
    }
}
