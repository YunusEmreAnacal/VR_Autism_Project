using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;

public class SliceObj : MonoBehaviour
{
    public Material bread;
    public Material lemon;
    public Material Watermelon;
    public float explosionForce;
    public float exposionRadius;
    public bool gravity, kinematic;

    public bool isInsideTrigger = false;

    public float slicingSpeedThreshold = 0.5f; // Örnek eþik deðeri

    public Rigidbody knifeRigidbody;


    private Material x;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("CanSlice"))
        {

            Debug.Log("Nesne trigger alanýna girdi.");
            //    isInsideTrigger = true;
             other.GetComponent<Rigidbody>().isKinematic = true;
             other.GetComponent<BoxCollider>().isTrigger = true;

            
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
       

        if (other.gameObject.CompareTag("CanSlice"))
            {
                Debug.Log("Nesne trigger alanýndan çýktý. + " + knifeRigidbody.velocity.magnitude);

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
                }
                other.GetComponent<Rigidbody>().isKinematic = false;
                other.GetComponent<BoxCollider>().isTrigger = false;
            }

            
            
            
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

    }
}
