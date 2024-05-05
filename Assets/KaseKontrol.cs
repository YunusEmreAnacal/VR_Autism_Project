using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaseKontrol : MonoBehaviour
{
    public GameObject basariliEkrani; // G�rev ba�ar�l� oldu�unda g�r�necek olan ekran

    private bool ekmegeTutunuyor = false; // Ekme�i tutuyor mu?
    private int domatesSayisi = 0; // Toplam domates say�s�

    void OnTriggerEnter(Collider other)
    {
        // E�er etkile�ime giren nesne ekme�e ait bir collider ise
        if (other.name.Contains("Bread"))
        {
            
            ekmegeTutunuyor = true;
            Debug.Log("E girdi.");
            //Destroy(other.gameObject); // Ekme�i yok et (kaseye koyuldu�unda yok edilir)
        }
        // E�er etkile�ime giren nesne domatese ait bir collider ise
        else if (other.name.Contains("Tomato"))
        {
            
            domatesSayisi++; // Domates say�s�n� bir art�r
            Debug.Log("T girdi.  " + domatesSayisi);
            //Destroy(other.gameObject); // Domatesi yok et (kaseye koyuldu�unda yok edilir)
        }
    }

    void Update()
    {
        // E�er ekme�i kaseye yerle�tirdiysen ve d�rt domatesi de kaseye koyduysan
        if (ekmegeTutunuyor && domatesSayisi == 4)
        {
            basariliEkrani.SetActive(true); // Ba�ar�l� ekran� g�ster
        }
    }
}

