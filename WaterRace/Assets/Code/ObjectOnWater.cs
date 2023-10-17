using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnWater : MonoBehaviour
{
    public GameObject[] ChildrenOb;
    private Code.Pooling.ParticlesPooling _particlesPooling;
    public void Init(Code.Pooling.ParticlesPooling particlesPooling)
    {
        _particlesPooling = particlesPooling;
    }
    private void OnTriggerEnter(Collider other)
    {
        foreach (var item in ChildrenOb)
        {



            ParticleObjectOnWater particleObjectOnWater = item.GetComponent<ParticleObjectOnWater>();

            if (particleObjectOnWater) particleObjectOnWater.Activation(other.transform.position);
            else item.AddComponent<ParticleObjectOnWater>().Init(_particlesPooling, transform, other.transform.position);

            Transform itemTransform = item.transform;





        }
        gameObject.SetActive(false);
    }
}
