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
    public GameObject Effect;
    public GameObject RightEffect;
    public GameObject LeftEffect;

    private float _oldHorizontal;
    private float _newHorizontal;
    private float waterLineNow;
    private float waterLineRot;

    private bool _jetDownMove = false;
    private float newDifferent;
    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        StartCoroutine(MoveJet());
        StartCoroutine(WaterLine());
    }
    private IEnumerator MoveJet()
    {
        float oldDifferent = 0;
        while (true)
        {
            _newHorizontal = DynamicJoystick.Horizontal;
            if (_newHorizontal == 0f) _oldHorizontal = Mathf.Lerp(_oldHorizontal, _newHorizontal, 2f * Time.deltaTime);
            else _oldHorizontal = Mathf.Lerp(_oldHorizontal, _newHorizontal, 3f * Time.deltaTime);

            Vector3 vectorMove = new Vector3(1f - Mathf.Abs(_oldHorizontal) / 1.2f, 0f, -_oldHorizontal/1.3f);
            Rb.velocity = vectorMove * 20f;

            newDifferent = _oldHorizontal - _newHorizontal;
            if (Mathf.Abs(newDifferent) > 0.5f) _jetDownMove = true;
            if (newDifferent > 0.5f) RightEffect.SetActive(true);
            else if (newDifferent < -0.5f) LeftEffect.SetActive(true);


            oldDifferent = Mathf.Lerp(oldDifferent, newDifferent, 4 * Time.deltaTime);
            Vector3 rotationJet = new Vector3(Mathf.Clamp(-100f * vectorMove.x * Mathf.Clamp(waterLineRot + 0.1f,0,1), -40f, 0f), 90f + _oldHorizontal * 80f, oldDifferent * 40f);
            Debug.Log(newDifferent);
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
                waterLineNow -= waterLineStep * 4f;
                waterLineRot -= waterLineStep * 5f;
                yield return new WaitForFixedUpdate();
                BuoyantObject.waterLevelOffset = waterLineNow;


            }
            Effect.SetActive(true);
            yield return new WaitForFixedUpdate();

            while (waterLineNow < 0.3f)
            {
                if (newDifferent < 0.1f)
                {
                    waterLineNow += waterLineStep * 1f;
                    waterLineRot = waterLineNow;
                    BuoyantObject.waterLevelOffset = waterLineNow;
                }
                yield return new WaitForFixedUpdate();

            }
            _jetDownMove = false;
            yield return new WaitUntil(() => _jetDownMove);

        }
    }
}