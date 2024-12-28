using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPoint;  // Position of the player's first position

    private void Start()
    {
        // Set the player's first position as the respawn point
        respawnPoint = transform.position;

        // Update the player's position to the respawn point when the scene starts
        transform.position = respawnPoint;
    }

    public void Respawn()
    {
        // Respawn the player at the initial position (respawn point)
        transform.position = respawnPoint;
        Debug.Log("Player respawned at: " + respawnPoint);
        SoundManager.PlaySFX("spawn");
    }

    // Optional: Reset the spawn point when changing levels
    private void OnLevelWasLoaded(int level)
    {
        // Ensure the player always respawns at the first position in each new level
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level 2")
        {
            respawnPoint = new Vector2(-10.5f, -1.15f);
        }
        else if (scene.name == "Level 3")
        {
            respawnPoint = new Vector2(-25.5f, -1.45f);
        }
        transform.position = respawnPoint;
    }
}
