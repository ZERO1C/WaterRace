using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Code.Pooling;

public class FactoryMap : MonoBehaviour
{

    public string[] RaftId;
    public string[] TitlId;

    public Vector3 SpawnPosition;


    private ParticlesPooling _particlesPooling;
    private ObjectPooling _objectPooling;

    [Inject]
    public void Init(ParticlesPooling particlesPooling, ObjectPooling objectPooling)
    {
        _particlesPooling = particlesPooling;
        _objectPooling = objectPooling;
        for (int i = 0; i < 6; i++)
        {
            SpawnNewTile();
        }
    }

    public void SpawnNewTile()
    {
        TileMoveToPosition tileMoveToPosition = _objectPooling.ObjectActivation(TitlId[Random.Range(0, TitlId.Length)], SpawnPosition).GetComponent<TileMoveToPosition>();
        tileMoveToPosition.Init(this);
        tileMoveToPosition.GetComponentInChildren<ObjectOnWater>()?.Init(_particlesPooling);

        GameObject waterGameObject = _objectPooling.ObjectActivation(RaftId[Random.Range(0, RaftId.Length)], SpawnPosition + new Vector3(Random.Range(-50, 51), 10, 45 + Random.Range(-5, 6)));
        waterGameObject.GetComponent<ObjectOnWater>().Init(_particlesPooling);

        SpawnPosition.x += 100;
    }
}
