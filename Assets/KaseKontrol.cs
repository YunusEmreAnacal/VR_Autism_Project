using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaseKontrol : MonoBehaviour
{
    public GameObject basariliEkrani; // G�rev ba�ar�l� oldu�unda g�r�necek olan ekran
    public Transform hedefNokta; // G�rev ba�ar�l� oldu�unda ���nlan�lacak mekan
    public Transform karakterTransform; // Karakterin transformu
    public GameObject konfetiPrefab; // Konfeti partik�l prefab�

    public float konfetiPatlamaSuresi = 5f; // Konfeti patlama s�resi (saniye)

    private ParticleSystem konfetiPartikul; // Konfeti partik�l sistemi
    private int domatesSayisi = 0; // Toplam domates say�s�

    public GameObject RayLine;

    public AudioSource source;

    public AudioSource BravoVoice;


    void Start()
    {

        
        // Konfeti partik�l sistemini al
        konfetiPartikul = konfetiPrefab.GetComponent<ParticleSystem>();
        // Ba�lang��ta konfeti partik�l�n� devre d��� b�rak
        konfetiPartikul.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        
        // E�er etkile�ime giren nesne domatese ait bir collider ise
        if (other.name.Contains("Tomato"))
        {
            domatesSayisi++; // Domates say�s�n� bir art�r
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
        // E�er ekme�i kaseye yerle�tirdiysen ve d�rt domatesi de kaseye koyduysan
        if (domatesSayisi >= 4)
        {

            StartCoroutine(StartAfterDelay(2f));
 


        }
    }

    IEnumerator StartAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // Belirtilen s�re kadar bekler.
                                                    
        karakterTransform.position = hedefNokta.position;
        
        // Konfeti partik�l�n� aktifle�tir ve belirli bir s�re sonra durdur
        StartCoroutine(KonfetiPatlat());


        basariliEkrani.SetActive(true); // Ba�ar�l� ekran� g�ster
        if (!basariliEkrani)
        {
            RayLine.SetActive(false);
        }
        else
        {
            RayLine.SetActive(true);

        }
    }

    // Konfeti partik�l�n� belirli s�re sonra durdurma coroutine fonksiyonu
    IEnumerator KonfetiPatlat()
    {
        // Konfeti partik�l�n� ba�lat
        konfetiPartikul.Play();
        
        // Belirli bir s�re beklet
        yield return new WaitForSeconds(konfetiPatlamaSuresi);
        // Konfeti partik�l�n� durdur
        konfetiPartikul.Stop();
        

    }
}

