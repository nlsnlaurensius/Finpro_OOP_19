using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPoint; // Posisi checkpoint terakhir
    private bool hasCheckpoint = false;

    private void Start()
    {
        // Set posisi awal sebagai respawn point default
        respawnPoint = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cek jika player menyentuh checkpoint
        if (collision.CompareTag("Checkpoint"))
        {
            UpdateCheckpoint(collision.transform.position);
            Debug.Log("Checkpoint reached!");
        }
    }

    private void UpdateCheckpoint(Vector3 checkpointPosition)
    {
        respawnPoint = checkpointPosition;
        hasCheckpoint = true;
        Debug.Log("Checkpoint updated at: " + respawnPoint);
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        Debug.Log("Player respawned at: " + respawnPoint);
    }
}