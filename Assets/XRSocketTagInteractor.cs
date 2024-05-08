using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketTagInteractor : XRSocketInteractor
{

    public string targetTag;

    public GameObject basariliEkrani; // G�rev ba�ar�l� oldu�unda g�r�necek olan ekran
    public Transform hedefNokta; // G�rev ba�ar�l� oldu�unda ���nlan�lacak mekan
    public Transform karakterTransform; // Karakterin transformu
    public GameObject konfetiPrefab; // Konfeti partik�l prefab�

    public float konfetiPatlamaSuresi = 5f; // Konfeti patlama s�resi (saniye)
    private ParticleSystem konfetiPartikul; // Konfeti partik�l sistemi
    public GameObject RayLine;

    void Start()
    {
        

        // Konfeti partik�l sistemini al
        konfetiPartikul = konfetiPrefab.GetComponent<ParticleSystem>();
        // Ba�lang��ta konfeti partik�l�n� devre d��� b�rak
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
        yield return new WaitForSeconds(delayTime); // Belirtilen s�re kadar bekler.
        karakterTransform.position = hedefNokta.position;// Karakteri hedef noktaya ���nla

        //kaseTransform.position = hedefNokta.position;
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
        // Buraya gecikmeli olarak ba�lamas� istedi�iniz kod blo�unu yazabilirsiniz.
        Debug.Log("Delayed start after 2 second.");
    }

    // Konfeti partik�l�n� belirli s�re sonra durdurma coroutine fonksiyonu
    IEnumerator KonfetiPatlat()
    {
        // Konfeti partik�l�n� ba�lat
        konfetiPartikul.Play();
        //source.Play();
        // Belirli bir s�re beklet
        yield return new WaitForSeconds(konfetiPatlamaSuresi);
        // Konfeti partik�l�n� durdur
        konfetiPartikul.Stop();
        //source.Stop();

    }

}
