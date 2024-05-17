using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaseKontrol : MonoBehaviour
{
    public GameObject basariliEkrani; // Görev baþarýlý olduðunda görünecek olan ekran
    public Transform hedefNokta; // Görev baþarýlý olduðunda ýþýnlanýlacak mekan
    public Transform karakterTransform; // Karakterin transformu
    public GameObject konfetiPrefab; // Konfeti partikül prefabý

    public float konfetiPatlamaSuresi = 5f; // Konfeti patlama süresi (saniye)

    private ParticleSystem konfetiPartikul; // Konfeti partikül sistemi
    private int domatesSayisi = 0; // Toplam domates sayýsý

    public GameObject RayLine;

    public AudioSource source;

    public AudioSource BravoVoice;


    void Start()
    {

        
        // Konfeti partikül sistemini al
        konfetiPartikul = konfetiPrefab.GetComponent<ParticleSystem>();
        // Baþlangýçta konfeti partikülünü devre dýþý býrak
        konfetiPartikul.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        
        // Eðer etkileþime giren nesne domatese ait bir collider ise
        if (other.name.Contains("Tomato"))
        {
            domatesSayisi++; // Domates sayýsýný bir artýr
            Debug.Log("T girdi.  " + domatesSayisi);

            XRGrabInteractableTwoAttach scriptComponent = other.GetComponent<XRGrabInteractableTwoAttach>();
            if (scriptComponent != null)
            {
                scriptComponent.enabled = false;
            }

            BravoVoice.Play();
        }
        
    }

    void Update()
    {
        // Eðer ekmeði kaseye yerleþtirdiysen ve dört domatesi de kaseye koyduysan
        if (domatesSayisi >= 4)
        {

            StartCoroutine(StartAfterDelay(2f));
 


        }
    }

    IEnumerator StartAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // Belirtilen süre kadar bekler.
                                                    
        karakterTransform.position = hedefNokta.position;
        
        // Konfeti partikülünü aktifleþtir ve belirli bir süre sonra durdur
        StartCoroutine(KonfetiPatlat());


        basariliEkrani.SetActive(true); // Baþarýlý ekraný göster
        if (!basariliEkrani)
        {
            RayLine.SetActive(false);
        }
        else
        {
            RayLine.SetActive(true);

        }
    }

    // Konfeti partikülünü belirli süre sonra durdurma coroutine fonksiyonu
    IEnumerator KonfetiPatlat()
    {
        // Konfeti partikülünü baþlat
        konfetiPartikul.Play();
        
        // Belirli bir süre beklet
        yield return new WaitForSeconds(konfetiPatlamaSuresi);
        // Konfeti partikülünü durdur
        konfetiPartikul.Stop();
        

    }
}

