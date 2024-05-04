using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObejctTeleport : MonoBehaviour
{
    public Transform respawnPoint; // I��nlanma noktas�

    void OnCollisionEnter(Collision collision)
    {
        // Yere �arpma olay�n� kontrol et
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Objeyi yeniden ���nla
            TeleportToRespawn();
        }
    }

    void TeleportToRespawn()
    {
        // I��nlanma noktas�na objeyi ta��
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;
    }
}
