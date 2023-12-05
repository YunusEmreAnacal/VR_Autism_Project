using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SlicingObject : MonoBehaviour
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
        }
    }

    private SlicedHull Slice(GameObject obj, Material mat)
    {
        return obj.Slice(transform.position, direction: transform.up, mat);
    }
}
