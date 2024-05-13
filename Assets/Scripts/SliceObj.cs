using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class SliceObj : MonoBehaviour
{
    public Material bread;
    public Material Cucumber;
    public Material Tomato;
    public float explosionForce;
    public float exposionRadius;
    public bool gravity, kinematic;

    public Transform respawnVegatables;
    public Transform respawnBread;

    private InputDevice leftController;
    private InputDevice rightController;

    public bool isInsideTrigger = false;

    public float slicingSpeedThreshold = 0.5f; // Örnek eþik deðeri
    public float slicingSpeedMax = 0.5f;
    public Rigidbody knifeRigidbody;
    public BoxCollider knifeCollider;

    public AudioClip breadVoice; // Trigger alaný 1 için ses dosyasý
    public AudioClip cucumberVoice; // Trigger alaný 1 için ses dosyasý
    public AudioClip tomatoVoice; // Trigger alaný 1 için ses dosyasý

    public AudioClip handAlertVoice; // Trigger alaný 2 için ses dosyasý

    AudioSource source1;



    private Material x;

    void Start()
    {
        // Sol ve sað kontrol cihazlarýný al
        InputDeviceCharacteristics controllerCharacteristics = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Right;
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    private void Awake()
    {
        source1 = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("CanSlice"))
        {

            Debug.Log("Nesne trigger alanýna girdi.");
            //    isInsideTrigger = true;
            //other.GetComponent<Rigidbody>().isKinematic = true;
            //other.GetComponent<BoxCollider>().isTrigger = true;
            if (other.name.Contains("Bread"))
            {
                source1.clip = breadVoice;
                source1.Play();
            }

            if (other.name.Contains("Cucumber"))
            {
                source1.clip = cucumberVoice;
                source1.Play();
            }

            if (other.name.Contains("Tomato"))
            {
                source1.clip = tomatoVoice;
                source1.Play();
            }
            


        }
        if (other.gameObject.CompareTag("Hands") )
        {
            source1.clip = handAlertVoice;
            source1.Play();
            // Örneðin, sol kolu titreþtir
            if (leftController != null)
            {
                HapticFeedback(leftController, 1f, 0.5f);
            }

            // Örneðin, sað kolu titreþtir
            if (rightController != null)
            {
                HapticFeedback(rightController, 1f, 0.5f);
            }
        }
       
    }




    private void OnTriggerExit(Collider other)
    {
       

        if (other.gameObject.CompareTag("CanSlice"))
            {
                Debug.Log("Nesne trigger alanýndan çýktý. + " + knifeRigidbody.velocity.magnitude);

            if (knifeRigidbody.velocity.magnitude >= slicingSpeedThreshold && knifeRigidbody.velocity.magnitude <= slicingSpeedMax)
            {
                if (other.name.Contains("Bread") || other.name.Contains("BreadC"))
                {
                    x = bread;
                    SlicedHull sliceobj = Slice(other.gameObject, x);
                    GameObject SlicedObjtop = sliceobj.CreateUpperHull(other.gameObject, x);
                    GameObject SliceObjDown = sliceobj.CreateLowerHull(other.gameObject, x);
                    Destroy(other.gameObject);
                    AddComponentForBread(SlicedObjtop);
                    AddComponentForBread(SliceObjDown);
                    //other.GetComponent<Rigidbody>().isKinematic = false;
                    //other.GetComponent<BoxCollider>().isTrigger = false;
                }
                else if (other.name.Contains("Cucumber") || other.name.Contains("CucumberC"))
                {
                    x = Cucumber;
                    SlicedHull sliceobj = Slice(other.gameObject, x);
                    GameObject SlicedObjtop = sliceobj.CreateUpperHull(other.gameObject, x);
                    GameObject SliceObjDown = sliceobj.CreateLowerHull(other.gameObject, x);
                    Destroy(other.gameObject);
                    AddComponentForCucumber(SlicedObjtop);
                    AddComponentForCucumber(SliceObjDown);
                    //other.GetComponent<Rigidbody>().isKinematic = false;
                    //other.GetComponent<BoxCollider>().isTrigger = false;
                }
                else if (other.name.Contains("Tomato") || other.name.Contains("TomatoC"))
                {
                    x = Tomato;
                    SlicedHull sliceobj = Slice(other.gameObject, x);
                    GameObject SlicedObjtop = sliceobj.CreateUpperHull(other.gameObject, x);
                    GameObject SliceObjDown = sliceobj.CreateLowerHull(other.gameObject, x);
                    Destroy(other.gameObject);
                    AddComponentForTomato(SlicedObjtop);
                    AddComponentForTomato(SliceObjDown);
                    //other.GetComponent<Rigidbody>().isKinematic = false;
                    //other.GetComponent<BoxCollider>().isTrigger = false;
                }
                
            }


             source1.Stop();
            
            //isInsideTrigger = false;

        }

    }

    private SlicedHull Slice(GameObject obj, Material mat)
    {
        return obj.Slice(transform.position, transform.forward, mat);
    }

    void AddComponentForBread (GameObject obj){
        obj.AddComponent<BoxCollider>();
        var rigidbody = obj.AddComponent<Rigidbody>();
        rigidbody.useGravity = gravity;
        rigidbody.isKinematic = kinematic;
        rigidbody.AddExplosionForce(explosionForce, obj.transform.position, exposionRadius);
        //Destroy(obj,3f);
        XRGrabInteractableTwoAttach script = obj.AddComponent<XRGrabInteractableTwoAttach>();
        obj.tag= "CanSlice";
        obj.name = "BreadC";
        
        script.selectMode = InteractableSelectMode.Multiple;
        script.useDynamicAttach = true;

        ObejctTeleport script2 = obj.AddComponent<ObejctTeleport>();
        script2.respawnPoint = respawnBread;

        //obj.GetComponent<Rigidbody>().isKinematic = false;
        //obj.GetComponent<BoxCollider>().isTrigger = false;

    }
    void AddComponentForCucumber(GameObject obj)
    {
        obj.AddComponent<BoxCollider>();
        var rigidbody = obj.AddComponent<Rigidbody>();
        rigidbody.useGravity = gravity;
        rigidbody.isKinematic = kinematic;
        rigidbody.AddExplosionForce(explosionForce, obj.transform.position, exposionRadius);
        //Destroy(obj,3f);
        XRGrabInteractableTwoAttach script = obj.AddComponent<XRGrabInteractableTwoAttach>();
        obj.tag = "CanSlice";
        obj.name = "CucumberC";

        script.selectMode = InteractableSelectMode.Multiple;
        script.useDynamicAttach = true;

        obj.GetComponent<Rigidbody>().isKinematic = false;
        obj.GetComponent<BoxCollider>().isTrigger = false;

        ObejctTeleport script2 = obj.AddComponent<ObejctTeleport>();
        script2.respawnPoint = respawnVegatables;

    }
    void AddComponentForTomato(GameObject obj)
    {
        obj.AddComponent<BoxCollider>();
        var rigidbody = obj.AddComponent<Rigidbody>();
        rigidbody.useGravity = gravity;
        rigidbody.isKinematic = kinematic;
        rigidbody.AddExplosionForce(explosionForce, obj.transform.position, exposionRadius);
        //Destroy(obj,3f);
        XRGrabInteractableTwoAttach script = obj.AddComponent<XRGrabInteractableTwoAttach>();
        obj.tag = "CanSlice";
        obj.name = "TomatoC";


        script.selectMode = InteractableSelectMode.Multiple;
        script.useDynamicAttach = true;

        obj.GetComponent<Rigidbody>().isKinematic = false;
        obj.GetComponent<BoxCollider>().isTrigger = false;

        ObejctTeleport script2 = obj.AddComponent<ObejctTeleport>();
        script2.respawnPoint = respawnVegatables;

    }

    // Titreþim geribildirimini yönetmek için bir fonksiyon
    void HapticFeedback(InputDevice device, float amplitude, float duration)
    {
        HapticCapabilities capabilities;
        if (device.TryGetHapticCapabilities(out capabilities))
        {
            if (capabilities.supportsImpulse)
            {
                uint channel = 0; // Kanalý belirle, genellikle 0
                device.SendHapticImpulse(channel, amplitude, duration);
            }
        }
    }

}
