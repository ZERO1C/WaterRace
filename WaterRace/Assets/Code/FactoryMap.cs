using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryMap : MonoBehaviour
{
    public TileMoveToPosition[] TerrainTile;
    public Vector3 SpawnPosition;
    public void SpawnNewTile()
    {
        Instantiate(TerrainTile[Random.Range(0, TerrainTile.Length)],SpawnPosition,Quaternion.identity,transform).Init();
        SpawnPosition.x += 100;
    }
}
