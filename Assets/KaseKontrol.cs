using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaseKontrol : MonoBehaviour
{
    public GameObject basariliEkrani; // Görev baþarýlý olduðunda görünecek olan ekran

    private bool ekmegeTutunuyor = false; // Ekmeði tutuyor mu?
    private int domatesSayisi = 0; // Toplam domates sayýsý

    void OnTriggerEnter(Collider other)
    {
        // Eðer etkileþime giren nesne ekmeðe ait bir collider ise
        if (other.name.Contains("Bread"))
        {
            
            ekmegeTutunuyor = true;
            Debug.Log("E girdi.");
            //Destroy(other.gameObject); // Ekmeði yok et (kaseye koyulduðunda yok edilir)
        }
        // Eðer etkileþime giren nesne domatese ait bir collider ise
        else if (other.name.Contains("Tomato"))
        {
            
            domatesSayisi++; // Domates sayýsýný bir artýr
            Debug.Log("T girdi.  " + domatesSayisi);
            //Destroy(other.gameObject); // Domatesi yok et (kaseye koyulduðunda yok edilir)
        }
    }

    void Update()
    {
        // Eðer ekmeði kaseye yerleþtirdiysen ve dört domatesi de kaseye koyduysan
        if (ekmegeTutunuyor && domatesSayisi == 4)
        {
            basariliEkrani.SetActive(true); // Baþarýlý ekraný göster
        }
    }
}

