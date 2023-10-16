using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterSystem;
public class MovePlayer : MonoBehaviour
{
    public BuoyantObject BuoyantObject;
    public DynamicJoystick DynamicJoystick;
    public Transform TransformJet;
    public Rigidbody Rb;
    public GameObject effect;

    private float _oldHorizontal;
    private float _newHorizontal;
    private float waterLineNow;
    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        StartCoroutine(MoveJet());
        StartCoroutine(WaterLine());
    }
    private void FixedUpdate()
    {


    }
    private IEnumerator MoveJet()
    {
        float oldDifferent = 0;
        float newDifferent = 0;
        while (true)
        {
            _newHorizontal = DynamicJoystick.Horizontal;
            if (_newHorizontal == 0f) _oldHorizontal = Mathf.Lerp(_oldHorizontal, _newHorizontal, 2f * Time.deltaTime);
            else _oldHorizontal = Mathf.Lerp(_oldHorizontal, _newHorizontal, 3f * Time.deltaTime);
            Debug.Log((_oldHorizontal - _newHorizontal));

            Vector3 vectorMove = new Vector3(1f - Mathf.Abs(_oldHorizontal) / 1.2f, 0f, -_oldHorizontal/1.3f);
            Rb.velocity = vectorMove * 15f;

            newDifferent = _oldHorizontal - _newHorizontal;
            oldDifferent = Mathf.Lerp(oldDifferent, newDifferent, 4 * Time.deltaTime);
            Vector3 rotationJet = new Vector3(Mathf.Clamp(-50f * vectorMove.x * Mathf.Clamp( waterLineNow,0,1), -30f, 0f), 90f + _oldHorizontal * 80f, oldDifferent * 40f);
            Debug.Log(rotationJet);
            TransformJet.rotation = Quaternion.Euler(rotationJet);
            yield return new WaitForFixedUpdate();
        }
    }


    IEnumerator WaterLine()
    {
        float waterLineStep = 0.01f;

        while (true)
        {
            while (waterLineNow > -0.2f)
            {
                waterLineNow -= waterLineStep*5f;
                yield return new WaitForFixedUpdate();
                BuoyantObject.waterLevelOffset = waterLineNow;


            }
            effect.SetActive(true);
            yield return new WaitForFixedUpdate();

            while (waterLineNow < 0.4f)
            {
                waterLineNow += waterLineStep*1.5f;
                BuoyantObject.waterLevelOffset = waterLineNow;
                yield return new WaitForFixedUpdate();

            }
            yield return new WaitForSeconds(Random.Range(0f, 10f) / 10f);

        }
    }
}