using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortal : MonoBehaviour
{
    [SerializeField] private string nextSceneIndex;
    [SerializeField] private string playerTag = "Player";

    private static GameObject cachedPlayer;
    private static GameObject cachedFireballHolder; // Tambah cache untuk Fireball Holder

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            // Cache dan simpan player
            cachedPlayer = collision.gameObject;
            DontDestroyOnLoad(cachedPlayer);
            
            // Cache dan simpan Fireball Holder
            cachedFireballHolder = GameObject.Find("FireballHolder");
            if (cachedFireballHolder != null)
            {
                DontDestroyOnLoad(cachedFireballHolder);
            }
            
            // Nonaktifkan komponen yang membuat player bisa bergerak/jatuh
            DisablePlayerComponents();
            
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    private void DisablePlayerComponents()
    {
        if (cachedPlayer != null)
        {
            var rb = cachedPlayer.GetComponent<Rigidbody2D>();
            if (rb != null) rb.simulated = false;

            var movement = cachedPlayer.GetComponent<PlayerMovement>();
            if (movement != null) movement.enabled = false;

            var renderer = cachedPlayer.GetComponent<SpriteRenderer>();
            if (renderer != null) renderer.enabled = false;
        }
    }

    public static GameObject GetCachedPlayer()
    {
        return cachedPlayer;
    }

    public static GameObject GetCachedFireballHolder()
    {
        return cachedFireballHolder;
    }
}