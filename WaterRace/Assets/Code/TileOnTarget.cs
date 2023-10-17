using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOnTarget : MonoBehaviour
{
    private FactoryMap _factoryMap;
    private GameObject _parentObject;
    public void Init(FactoryMap factoryMap, GameObject parentGameObject)
    {
        _factoryMap = factoryMap;
        _parentObject = parentGameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<MovePlayer>())
        {
            _factoryMap.SpawnNewTile();
            StartCoroutine(OffParent());
        }

    }
    private IEnumerator OffParent()
    {
        yield return new WaitForSeconds(4);
        _parentObject.SetActive(false);

    }
}
