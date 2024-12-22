using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortal : MonoBehaviour
{
    [SerializeField] private string nextSceneIndex;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private Vector2 spawnPosition;

    private static GameObject cachedPlayer; // Untuk menyimpan referensi player

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            cachedPlayer = collision.gameObject; // Simpan referensi player
            DontDestroyOnLoad(cachedPlayer);
            
            // Nonaktifkan komponen yang membuat player bisa bergerak/jatuh
            DisablePlayerComponents();
            
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    private void DisablePlayerComponents()
    {
        if (cachedPlayer != null)
        {
            // Nonaktifkan Rigidbody2D
            var rb = cachedPlayer.GetComponent<Rigidbody2D>();
            if (rb != null) rb.simulated = false;

            // Nonaktifkan script movement player (sesuaikan nama componentnya)
            var movement = cachedPlayer.GetComponent<PlayerMovement>();
            if (movement != null) movement.enabled = false;

            // Sembunyikan player selama video
            var renderer = cachedPlayer.GetComponent<SpriteRenderer>();
            if (renderer != null) renderer.enabled = false;
        }
    }

    public static GameObject GetCachedPlayer()
    {
        return cachedPlayer;
    }
}