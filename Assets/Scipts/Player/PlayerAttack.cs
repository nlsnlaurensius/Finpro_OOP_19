using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private bool attackTriggered = false;

    SoundManager soundManager;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
            SoundManager.PlaySFX("slash");
        }
        
        // Cek status animasi
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (attackTriggered && stateInfo.IsName("attack") && stateInfo.normalizedTime >= 0.95f)
        {
            LaunchFireball();
            attackTriggered = false;
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        attackTriggered = true;
    }

    private void LaunchFireball()
    {
        int projectileIndex = FindFireball();
        if (projectileIndex != -1) // Memastikan ada fireball yang tersedia
        {
            GameObject fireball = fireballs[projectileIndex];
            fireball.transform.position = firePoint.position;
            
            Projectile projectile = fireball.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.SetDirection(Mathf.Sign(transform.localScale.x));
            }
        }
        SoundManager.PlaySFX("fireball");
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return -1; // Return -1 jika tidak ada fireball yang tersedia
    }
}