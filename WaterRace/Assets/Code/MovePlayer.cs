using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public DynamicJoystick DynamicJoystick;
    public Transform TransformJet;

    private float _oldHorizontal;
    private float _newHorizontal;
    private void FixedUpdate()
    {
        _newHorizontal = DynamicJoystick.Horizontal;
        _oldHorizontal = Mathf.Lerp(_oldHorizontal, _newHorizontal, 5 * Time.deltaTime);
        Debug.Log((_oldHorizontal - _newHorizontal));

        Vector3 vectorMove = new Vector3(1 - Mathf.Abs(_oldHorizontal), 0, -_oldHorizontal);
        transform.position += (vectorMove / 10);
        Vector3 rotationJet = new Vector3(Mathf.Clamp(-20 * vectorMove.x, -10, 0), 90 + _oldHorizontal * 45, (_oldHorizontal - _newHorizontal) * 20);
        Debug.Log(rotationJet);
        TransformJet.rotation = Quaternion.Euler(rotationJet);

    }
}