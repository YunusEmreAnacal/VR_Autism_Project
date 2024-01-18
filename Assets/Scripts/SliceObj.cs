using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;

public class SliceObj : MonoBehaviour
{
    public Material bread;
    public Material lemon;
    public Material tomato;
    public float explosionForce;
    public float exposionRadius;
    public bool gravity, kinematic;

    private Material x;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("CanSlice"))
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
}
