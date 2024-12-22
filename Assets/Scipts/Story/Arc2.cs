using UnityEngine;
using UnityEngine.SceneManagement;

public class Arc2 : MonoBehaviour
{
    [SerializeField] private Vector2 spawnPosition = new Vector2(10f, 1.6f);

    [SerializeField] private float videoLength = 0f; 

    private void OnEnable()
    {
        StartCoroutine(PlayVideoAndTransition());
    }

    private System.Collections.IEnumerator PlayVideoAndTransition()
    {
        // Tunggu sampai video selesai
        yield return new WaitForSeconds(videoLength);

        // Load Level 2
        SceneManager.LoadScene("Level 2", LoadSceneMode.Single);

        // Tunggu scene selesai load
        yield return new WaitForEndOfFrame();

        // Dapatkan referensi player yang tersimpan
        GameObject player = LevelPortal.GetCachedPlayer();
        if (player != null)
        {
            // Set posisi player
            player.transform.position = spawnPosition;

            // Aktifkan kembali komponen player
            var rb = player.GetComponent<Rigidbody2D>();
            if (rb != null) rb.simulated = true;

            var movement = player.GetComponent<PlayerMovement>();
            if (movement != null) movement.enabled = true;

            var renderer = player.GetComponent<SpriteRenderer>();
            if (renderer != null) renderer.enabled = true;
        }
    }
}