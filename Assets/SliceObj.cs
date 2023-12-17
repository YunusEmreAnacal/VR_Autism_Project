using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;

public class SliceObj : MonoBehaviour
{
    public Material materialSlicedSide;
    public float explosionForce;
    public float exposionRadius;
    public bool gravity, kinematic;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("CanSlice"))
        {
            SlicedHull sliceobj = Slice(other.gameObject, materialSlicedSide);
            GameObject SlicedObjtop = sliceobj.CreateUpperHull(other.gameObject, materialSlicedSide);
            GameObject SliceObjDown = sliceobj.CreateLowerHull(other.gameObject, materialSlicedSide);
            Destroy(other.gameObject);
            AddComponent(SlicedObjtop);
            AddComponent(SliceObjDown);
        }
    }

    private SlicedHull Slice(GameObject obj, Material mat)
    {
        return obj.Slice(transform.position, transform.forward, mat);
    }
    void AddComponent (GameObject obj){
        obj.AddComponent<BoxCollider>();
        var rigidbody = obj.AddComponent<Rigidbody>();
        rigidbody.useGravity = gravity;
        rigidbody.isKinematic = kinematic;
        rigidbody.AddExplosionForce(explosionForce, obj.transform.position, exposionRadius);
        //Destroy(obj,3f);
        XRGrabInteractable script = obj.AddComponent<XRGrabInteractable>();
        obj.tag= "CanSlice";
       
    }
}
