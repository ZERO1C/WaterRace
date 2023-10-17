using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnWater : MonoBehaviour
{
    public GameObject[] ChildrenOb;

    private void OnTriggerEnter(Collider other)
    {
        foreach (var item in ChildrenOb)
        {
            Rigidbody rb = item.AddComponent<Rigidbody>();
            Transform itemTransform = item.transform;
            Vector3 velocityItem = -(other.transform.position - itemTransform.position);
            velocityItem.y += 1;
            rb.velocity = velocityItem*10;
            itemTransform.parent = null;
            StartCoroutine(item.AddComponent<AddBuoyant>().AddBuoyantCurotina());
        }
        Destroy(gameObject);
    }
}
