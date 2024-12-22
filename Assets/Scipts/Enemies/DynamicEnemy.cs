using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicEnemy : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float damage = 10.0f;
    private bool movingLeft = true; // Mulai dengan bergerak ke kiri
    private float leftEdge;

    private float rightEdge;

    private void Awake()
    {
        // Hitung batas kiri dan kanan
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        // Mendapatkan posisi Y dan Z asli agar tidak berubah
        float yPos = transform.position.y;
        float zPos = transform.position.z;

        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, yPos, zPos);
                 MoveDirection(1);
            }
            else
            {
                movingLeft = false; // Berubah arah ke kanan
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, yPos, zPos);
                MoveDirection(-1);
            }
            else
            {
                movingLeft = true; // Berubah arah ke kiri
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cek jika tabrakan dengan Player
        if (collision.collider.CompareTag("Player"))
        {
            // Ambil komponen Health dari Player
            Health playerHealth = collision.collider.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Player terkena damage sebesar " + damage);
            }
        }
    }

    private void MoveDirection(int direction)
    {
        // Ubah arah tampilan enemy
        Vector3 newScale = transform.localScale;
        newScale.x = Mathf.Abs(newScale.x) * direction; // Pastikan nilai X positif sebelum dikalikan
        transform.localScale = newScale;
    }
}
