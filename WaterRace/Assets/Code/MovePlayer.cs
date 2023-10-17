using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterSystem;
using Zenject;

public class MovePlayer : MonoBehaviour
{

    public Transform TransformJet;
    public GameObject Effect;
    public GameObject RightEffect;
    public GameObject LeftEffect;

    private Rigidbody _rb;
    private BuoyantObject _buoyantObject;
    private DynamicJoystick _dynamicJoystick;

    private float _oldHorizontal;
    private float _newHorizontal;
    private float _waterLineNow;
    private float _waterLineRot;

    private bool _jetDownMove = false;
    private float _newDifferent;


    // to do замінити на ініціалізацію в фабриці у випадку створення

    [Inject]
    private void Init()
    {
        _buoyantObject = GetComponent<BuoyantObject>();
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(MoveJet());
        StartCoroutine(WaterLine());
    }

    public void BindDynamicJoystick( DynamicJoystick dynamicJoystick)
    {
        _dynamicJoystick = dynamicJoystick;
    }


    private IEnumerator MoveJet()
    {
        float oldDifferent = 0;
        yield return new WaitForFixedUpdate();

        while (true)
        {
            _newHorizontal = _dynamicJoystick.Horizontal; 
            if (_newHorizontal == 0f) _oldHorizontal = Mathf.Lerp(_oldHorizontal, _newHorizontal, 2f * Time.deltaTime);
            else _oldHorizontal = Mathf.Lerp(_oldHorizontal, _newHorizontal, 3f * Time.deltaTime);

            Vector3 vectorMove = new Vector3(1f - Mathf.Abs(_oldHorizontal) / 1.2f, 0f, -_oldHorizontal/1.3f);
            _rb.velocity = vectorMove * 20f;

            _newDifferent = _oldHorizontal - _newHorizontal;
            if (Mathf.Abs(_newDifferent) > 0.5f) _jetDownMove = true;
            if (_newDifferent > 0.5f) RightEffect.SetActive(true);
            else if (_newDifferent < -0.5f) LeftEffect.SetActive(true);


            oldDifferent = Mathf.Lerp(oldDifferent, _newDifferent, 4f * Time.deltaTime);
            Vector3 rotationJet = new Vector3(Mathf.Clamp(-100f * vectorMove.x * Mathf.Clamp(_waterLineRot + 0.1f,0,1), -40f, 0f), 90f + _oldHorizontal * 80f, oldDifferent * 40f);
            TransformJet.rotation = Quaternion.Euler(rotationJet);
            yield return new WaitForFixedUpdate();
        }
    }


    IEnumerator WaterLine()
    {
        float waterLineStep = 0.01f;

        while (true)
        {
            while (_waterLineNow > -0.2f)
            {
                _waterLineNow -= waterLineStep * 4f;
                _waterLineRot -= waterLineStep * 5f;
                yield return new WaitForFixedUpdate();
                _buoyantObject.waterLevelOffset = _waterLineNow;


            }
            Effect.SetActive(true);
            yield return new WaitForFixedUpdate();

            while (_waterLineNow < 0.3f)
            {
                if (_newDifferent < 0.1f)
                {
                    _waterLineNow += waterLineStep * 1f;
                    _waterLineRot = _waterLineNow;
                    _buoyantObject.waterLevelOffset = _waterLineNow;
                }
                yield return new WaitForFixedUpdate();

            }
            _jetDownMove = false;
            yield return new WaitUntil(() => _jetDownMove);

        }
    }
}