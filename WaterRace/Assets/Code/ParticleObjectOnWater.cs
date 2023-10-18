using Code.Pooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using WaterSystem;
public class ParticleObjectOnWater : MonoBehaviour
{
    private Transform _myTransform;
    private Transform _parentTransform;
    private Rigidbody _rb;
    private ParticlesPooling _particlesPooling;
    private string _id = "SplashingAround";
    private Vector3 _startLocalPosition;
    private Quaternion _startLocalRotation;
    private BuoyantObject _buoyantObject;
    private BoxCollider _boxCollider;
    public void Init(ParticlesPooling particlesPooling, Transform parentTransform, Vector3 targetPosition)
    {
        _particlesPooling = particlesPooling;
        _parentTransform = parentTransform;
        _myTransform = transform;
        _startLocalPosition = _myTransform.localPosition;
        _startLocalRotation = _myTransform.localRotation;

        _rb = gameObject.AddComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        StartCoroutine(LifeCycleParticle(targetPosition));

    }

    public void Activation(Vector3 targetPosition)
    {
        StartCoroutine(LifeCycleParticle(targetPosition));
    }

    public IEnumerator LifeCycleParticle(Vector3 targetPosition)
    {

        Vector3 velocityObject = -(targetPosition - _myTransform.position);
        velocityObject += new Vector3(Random.Range(-10f, 11f) / 10f, 0.2f, Random.Range(-10f, 11f) / 10f);
        velocityObject.x = Mathf.Abs(velocityObject.x);
        _rb.velocity = velocityObject * 10f;
        _rb.drag = 1f;
        _myTransform.parent = null;
        _boxCollider.enabled = true;


        while (_myTransform.position.y > 0f)
        {
            yield return new WaitForSeconds(0.1f);
        }


        if (_buoyantObject) _buoyantObject.enabled = true;
        else _buoyantObject = gameObject.AddComponent<BuoyantObject>();
        gameObject.layer = 0;
        _particlesPooling.PlayParticle(_id, _myTransform.position);

        yield return new WaitForSeconds(5);

        _rb.isKinematic = true;
        _rb.useGravity = false;
        _myTransform.parent = _parentTransform;
        _myTransform.localPosition = _startLocalPosition;
        _myTransform.localRotation = _startLocalRotation;
        _buoyantObject.enabled = false;
        

        _boxCollider.enabled = false;
        _rb.useGravity = false;
        _rb.isKinematic = true;

    }


}
