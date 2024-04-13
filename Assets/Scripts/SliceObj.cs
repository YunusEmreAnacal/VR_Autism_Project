using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class SliceObj : MonoBehaviour
{
    public Material bread;
    public Material lemon;
    public Material Watermelon;
    public float explosionForce;
    public float exposionRadius;
    public bool gravity, kinematic;

    private InputDevice leftController;
    private InputDevice rightController;

    public bool isInsideTrigger = false;

    public float slicingSpeedThreshold = 0.5f; // �rnek e�ik de�eri

    public Rigidbody knifeRigidbody;

    public AudioClip sesDosyasi1; // Trigger alan� 1 i�in ses dosyas�
    public AudioClip sesDosyasi2; // Trigger alan� 2 i�in ses dosyas�
    AudioSource source1;



    private Material x;

    void Start()
    {
        // Sol ve sa� kontrol cihazlar�n� al
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

            Debug.Log("Nesne trigger alan�na girdi.");
            //    isInsideTrigger = true;
            //other.GetComponent<Rigidbody>().isKinematic = true;
            //other.GetComponent<BoxCollider>().isTrigger = true;
             source1.clip = sesDosyasi1;
             source1.Play();


        }
        if (other.gameObject.CompareTag("Hands"))
        {
            source1.clip = sesDosyasi2;
            source1.Play();
            // �rne�in, sol kolu titre�tir
            if (leftController != null)
            {
                HapticFeedback(leftController, 1f, 0.5f);
            }

            // �rne�in, sa� kolu titre�tir
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
                Debug.Log("Nesne trigger alan�ndan ��kt�. + " + knifeRigidbody.velocity.magnitude);

            if (knifeRigidbody.velocity.magnitude >= slicingSpeedThreshold)
            {
                if (other.name.Contains("Bread"))
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
                else if (other.name.Contains("Lemon"))
                {
                    x = lemon;
                    SlicedHull sliceobj = Slice(other.gameObject, x);
                    GameObject SlicedObjtop = sliceobj.CreateUpperHull(other.gameObject, x);
                    GameObject SliceObjDown = sliceobj.CreateLowerHull(other.gameObject, x);
                    Destroy(other.gameObject);
                    AddComponentForLemon(SlicedObjtop);
                    AddComponentForLemon(SliceObjDown);
                    //other.GetComponent<Rigidbody>().isKinematic = false;
                    //other.GetComponent<BoxCollider>().isTrigger = false;
                }
                else if (other.name.Contains("Watermelon"))
                {
                    x = Watermelon;
                    SlicedHull sliceobj = Slice(other.gameObject, x);
                    GameObject SlicedObjtop = sliceobj.CreateUpperHull(other.gameObject, x);
                    GameObject SliceObjDown = sliceobj.CreateLowerHull(other.gameObject, x);
                    Destroy(other.gameObject);
                    AddComponentForWatermelon(SlicedObjtop);
                    AddComponentForWatermelon(SliceObjDown);
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
        XRGrabInteractable script = obj.AddComponent<XRGrabInteractable>();
        obj.tag= "CanSlice";
        obj.name = "Bread";
        
        script.selectMode = InteractableSelectMode.Multiple;
        script.useDynamicAttach = true;

        //obj.GetComponent<Rigidbody>().isKinematic = false;
        //obj.GetComponent<BoxCollider>().isTrigger = false;

    }
    void AddComponentForLemon(GameObject obj)
    {
        obj.AddComponent<BoxCollider>();
        var rigidbody = obj.AddComponent<Rigidbody>();
        rigidbody.useGravity = gravity;
        rigidbody.isKinematic = kinematic;
        rigidbody.AddExplosionForce(explosionForce, obj.transform.position, exposionRadius);
        //Destroy(obj,3f);
        XRGrabInteractable script = obj.AddComponent<XRGrabInteractable>();
        obj.tag = "CanSlice";
        obj.name = "Lemon";

        script.selectMode = InteractableSelectMode.Multiple;
        script.useDynamicAttach = true;

        obj.GetComponent<Rigidbody>().isKinematic = false;
        obj.GetComponent<BoxCollider>().isTrigger = false;

    }
    void AddComponentForWatermelon(GameObject obj)
    {
        obj.AddComponent<BoxCollider>();
        var rigidbody = obj.AddComponent<Rigidbody>();
        rigidbody.useGravity = gravity;
        rigidbody.isKinematic = kinematic;
        rigidbody.AddExplosionForce(explosionForce, obj.transform.position, exposionRadius);
        //Destroy(obj,3f);
        XRGrabInteractable script = obj.AddComponent<XRGrabInteractable>();
        obj.tag = "CanSlice";
        obj.name = "Watermelon";

        script.selectMode = InteractableSelectMode.Multiple;
        script.useDynamicAttach = true;

        obj.GetComponent<Rigidbody>().isKinematic = false;
        obj.GetComponent<BoxCollider>().isTrigger = false;

    }

    // Titre�im geribildirimini y�netmek i�in bir fonksiyon
    void HapticFeedback(InputDevice device, float amplitude, float duration)
    {
        HapticCapabilities capabilities;
        if (device.TryGetHapticCapabilities(out capabilities))
        {
            if (capabilities.supportsImpulse)
            {
                uint channel = 0; // Kanal� belirle, genellikle 0
                device.SendHapticImpulse(channel, amplitude, duration);
            }
        }
    }

}
