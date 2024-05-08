using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketTagInteractor : XRSocketInteractor
{

    public string targetTag;

    public GameObject basariliEkrani; // Görev baþarýlý olduðunda görünecek olan ekran
    public Transform hedefNokta; // Görev baþarýlý olduðunda ýþýnlanýlacak mekan
    public Transform karakterTransform; // Karakterin transformu
    public GameObject konfetiPrefab; // Konfeti partikül prefabý

    public float konfetiPatlamaSuresi = 5f; // Konfeti patlama süresi (saniye)
    private ParticleSystem konfetiPartikul; // Konfeti partikül sistemi
    public GameObject RayLine;

    void Start()
    {
        

        // Konfeti partikül sistemini al
        konfetiPartikul = konfetiPrefab.GetComponent<ParticleSystem>();
        // Baþlangýçta konfeti partikülünü devre dýþý býrak
        konfetiPartikul.Stop();
    }

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && interactable.transform.tag == targetTag;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && interactable.transform.tag == targetTag;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        XRBaseInteractable interactable = args.interactable;

        if (interactable.transform.tag == targetTag)
        {
            StartCoroutine(StartAfterDelay(2f));
            
        }
    }


    IEnumerator StartAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // Belirtilen süre kadar bekler.
        karakterTransform.position = hedefNokta.position;// Karakteri hedef noktaya ýþýnla

        //kaseTransform.position = hedefNokta.position;
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
        // Buraya gecikmeli olarak baþlamasý istediðiniz kod bloðunu yazabilirsiniz.
        Debug.Log("Delayed start after 2 second.");
    }

    // Konfeti partikülünü belirli süre sonra durdurma coroutine fonksiyonu
    IEnumerator KonfetiPatlat()
    {
        // Konfeti partikülünü baþlat
        konfetiPartikul.Play();
        //source.Play();
        // Belirli bir süre beklet
        yield return new WaitForSeconds(konfetiPatlamaSuresi);
        // Konfeti partikülünü durdur
        konfetiPartikul.Stop();
        //source.Stop();

    }

}
