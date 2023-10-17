using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterSystem;
public class AddBuoyant : MonoBehaviour
{
    private Transform _myTransform;

    public IEnumerator AddBuoyantCurotina()
    {
        _myTransform = transform;
        while (transform.position.y > 0f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.AddComponent<BuoyantObject>();
        Destroy(this);
    }
}
