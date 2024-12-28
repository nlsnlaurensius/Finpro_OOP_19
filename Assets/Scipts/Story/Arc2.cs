using UnityEngine;
using UnityEngine.SceneManagement;

public class Arc2 : MonoBehaviour
{
    [SerializeField] private Vector2 spawnPosition = new Vector2(-10.5f, -1.15f);

    private void OnEnable()
    {
        StartCoroutine(PlayVideoAndTransition());
    }

    private System.Collections.IEnumerator PlayVideoAndTransition()
    {
        // Load Level 2
        SceneManager.LoadScene("Level 2", LoadSceneMode.Single);

        // Wait for the scene to load
        yield return new WaitForEndOfFrame();

        // Get the player reference after the scene is loaded
        GameObject player = LevelPortal.GetCachedPlayer();
        if (player != null)
        {
            // Set player position for Level 2
            player.transform.position = spawnPosition;

            // Enable the player's components
            var rb = player.GetComponent<Rigidbody2D>();
            if (rb != null) rb.simulated = true;

            var movement = player.GetComponent<PlayerMovement>();
            if (movement != null) movement.enabled = true;

            var renderer = player.GetComponent<SpriteRenderer>();
            if (renderer != null) renderer.enabled = true;
        }
    }
}
