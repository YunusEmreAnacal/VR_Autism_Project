using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level4Kontrol : MonoBehaviour
{
    public GameObject basariliEkrani; // G�rev ba�ar�l� oldu�unda g�r�necek olan ekran
    public Transform hedefNokta; // G�rev ba�ar�l� oldu�unda ���nlan�lacak mekan
    public Transform karakterTransform; // Karakterin transformu
    public GameObject konfetiPrefab; // Konfeti partik�l prefab�

    public float konfetiPatlamaSuresi = 5f; // Konfeti patlama s�resi (saniye)

    private ParticleSystem konfetiPartikul; // Konfeti partik�l sistemi
    private int domatesDilimSayisi = 0; // Toplam domates say�s�
    private int salatal�kDilimSayisi = 0;
    private int ekmekDilimSayisi = 0;

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
        if (other.name.Contains("TomatoC"))
        {
            domatesDilimSayisi++; // Domates say�s�n� bir art�r
            Debug.Log("T girdi.  " + domatesDilimSayisi);

            XRGrabInteractableTwoAttach scriptComponent = other.GetComponent<XRGrabInteractableTwoAttach>();
            if (scriptComponent != null)
            {
                scriptComponent.enabled = false;
            }
            BravoVoice.Play(); 
        }

        // E�er etkile�ime giren nesne salatal��a ait bir collider ise
        if (other.name.Contains("CucumberC"))
        {
            salatal�kDilimSayisi++; // salatal�k say�s�n� bir art�r
            Debug.Log("C girdi.  " + salatal�kDilimSayisi);

            XRGrabInteractableTwoAttach scriptComponent = other.GetComponent<XRGrabInteractableTwoAttach>();
            if (scriptComponent != null)
            {
                scriptComponent.enabled = false;
            }
            BravoVoice.Play();
        }

        // E�er etkile�ime giren nesne ekme�e ait bir collider ise
        if (other.name.Contains("BreadC"))
        {
            ekmekDilimSayisi++; // ekmek say�s�n� bir art�r
            Debug.Log("B girdi.  " + ekmekDilimSayisi);

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
        if (domatesDilimSayisi >= 3 && salatal�kDilimSayisi >= 3 && ekmekDilimSayisi >= 3)
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
