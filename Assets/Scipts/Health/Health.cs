using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [SerializeField] private float respawnDelay = 2f;
    private PlayerRespawn playerRespawn;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        playerRespawn = GetComponent<PlayerRespawn>();
        
        if (playerRespawn == null)
        {
            Debug.LogError("PlayerRespawn component not found on player!");
        }
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            if (anim != null)
            {
                anim.SetTrigger("hurt");
            }
        }
        else
        {
            if (!dead)
            {
                if (anim != null)
                {
                    anim.SetTrigger("die");
                }
                
                var playerMovement = GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    playerMovement.enabled = false;
                }
                
                dead = true;
                Debug.Log("Player died, initiating respawn sequence...");
                Invoke("Respawn", respawnDelay);
            }
        }
    }

    private void Respawn()
    {
        Debug.Log("Respawn method called");
        if (playerRespawn != null)
        {
            dead = false;
            currentHealth = startingHealth;
            
            var playerMovement = GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
            
            if (anim != null)
            {
                anim.ResetTrigger("die");
                anim.ResetTrigger("hurt");   
                anim.Play("Idle"); 
            }
            
            playerRespawn.Respawn();
            Debug.Log("Player has been respawned with full health: " + currentHealth);
        }
        else
        {
            Debug.LogError("PlayerRespawn component not found during respawn!");
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}