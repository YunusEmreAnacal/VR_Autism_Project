using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObejctTeleport : MonoBehaviour
{
    public Transform respawnPoint; // Iþýnlanma noktasý

    void OnCollisionEnter(Collision collision)
    {
        // Yere çarpma olayýný kontrol et
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Objeyi yeniden ýþýnla
            TeleportToRespawn();
        }
    }

    void TeleportToRespawn()
    {
        // Iþýnlanma noktasýna objeyi taþý
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;
    }
}
