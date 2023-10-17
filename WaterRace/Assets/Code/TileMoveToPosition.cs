using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMoveToPosition : MonoBehaviour
{
    // to do розбить на два класи Tile і TileMove
    private Transform _myTransform;
    public void Init(FactoryMap factoryMap)
    {
        _myTransform = transform;
        GetComponentInChildren<TileOnTarget>().Init(factoryMap, gameObject);
        StartCoroutine(MoveToPosition());
    }
    private IEnumerator MoveToPosition()
    {
        Vector3 myPosition = _myTransform.position;
        while(myPosition.y<-2.8f)
        {
            myPosition.y = Mathf.Lerp(myPosition.y, -2.5f, 3f * Time.deltaTime);
            _myTransform.position = myPosition;
            yield return new WaitForFixedUpdate();
        }
        myPosition.y = -2.5f; 
        _myTransform.position = myPosition;

    }
}
