using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;

public class Slicer : MonoBehaviour
{
    public LayerMask sliceMask;
    public Material materialSlicedSide;
    public float explosionForce;
    public float exposionRadius;
    public bool gravity, kinematic, isTouched;

    private void Update()
    {
        Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, sliceMask);

        foreach (Collider objectToBeSliced in objectsToBeSliced)
        {
            // Nesnenin daha önce dilimlenip dilimlenmediğini kontrol et
            if (!objectToBeSliced.gameObject.GetComponent<SlicedObject>())
            {
                SliceAndDice(objectToBeSliced.gameObject);
            }
        }
    }

    private void SliceAndDice(GameObject obj)
    {
        // Nesneyi dilimle ve dilimlendiğini belirtmek için SlicedObject bileşenini ekle
        obj.AddComponent<SlicedObject>();

        SlicedHull slicedObject = SliceObject(obj, materialSlicedSide);

        GameObject upperHullGameobject = slicedObject.CreateUpperHull(obj, materialSlicedSide);
        GameObject lowerHullGameobject = slicedObject.CreateLowerHull(obj, materialSlicedSide);

        upperHullGameobject.transform.position = obj.transform.position;
        lowerHullGameobject.transform.position = obj.transform.position;

        MakeItPhysical(upperHullGameobject);
        MakeItPhysical(lowerHullGameobject);

        Destroy(obj);
    }

    private void MakeItPhysical(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<SphereCollider>();
        var rigidbody = obj.AddComponent<Rigidbody>();
        rigidbody.useGravity = gravity;
        rigidbody.isKinematic = kinematic;
        rigidbody.AddExplosionForce(explosionForce, obj.transform.position, exposionRadius);

        XRGrabInteractable script = obj.AddComponent<XRGrabInteractable>();
        obj.tag = "CanSlice";
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }

    // Eklenen SlicedObject sınıfı, bir nesnenin daha önce dilimlenip dilimlenmediğini kontrol etmek için kullanılır.
    public class SlicedObject : MonoBehaviour { }
}
