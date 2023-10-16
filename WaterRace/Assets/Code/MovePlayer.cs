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

    private float _oldHorizontal;
    private float _newHorizontal;
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
            Vector3 rotationJet = new Vector3(Mathf.Clamp(-20f * vectorMove.x, -10f, 0f), 90f + _oldHorizontal * 80f, oldDifferent * 40f);
            Debug.Log(rotationJet);
            TransformJet.rotation = Quaternion.Euler(rotationJet);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator WaterLine()
    {
        float waterLineNow = -0.02f;
        while (true)
        {
            while (waterLineNow < -0.09f)
            {
                waterLineNow = Mathf.Lerp(waterLineNow, -0.1f, 1 * Time.deltaTime);
                BuoyantObject.waterLevelOffset = waterLineNow;
                yield return new WaitForFixedUpdate();

            }

            while (waterLineNow > -0.01)
            {
                waterLineNow = Mathf.Lerp(waterLineNow, 0, 1 * Time.deltaTime);
                BuoyantObject.waterLevelOffset = waterLineNow;
                yield return new WaitForFixedUpdate();

            }

        }
    }
}