using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTargetTerrain : MonoBehaviour
{
    public GameObject TerrainOb;
    private void OnTriggerExit(Collider other)
    {
        GetComponentInParent<FactoryMap>().SpawnNewTile();
        Destroy(TerrainOb,2);
    }
}
