using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccesfullVoice : MonoBehaviour
{
    public AudioSource BravoVoice;
    void OnTriggerEnter(Collider other)
    {

        // Eðer etkileþime giren nesne domatese ait bir collider ise
        if (other.name.Contains("XR Origin"))
        {

            BravoVoice.Play();
        }
    }
}
