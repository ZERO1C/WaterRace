using System;
using System.Collections.Generic;

using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using Zenject;

namespace Code.Pooling
{
    public class ObjectPooling : MonoBehaviour
    {
        public List<PoolObjects> PoolObjects = new List<PoolObjects>();
        public Dictionary<string, PoolObjects> FastAccess;

        [Inject]
        private void Init()
        {
            FastAccess = new Dictionary<string, PoolObjects>();
            foreach (var container in PoolObjects)
            {
                Debug.Log(container);
                for (int i = 0; i < container.PoolAmount; i++)
                {
                    var instantiate = Instantiate(container.Example, new Vector3(-99f, -99f, -99f), Quaternion.identity, transform);
                    container.TransformContainers.Add(instantiate);
                    container.Index = 0;
                }
                FastAccess.Add(container.Id, container);
            }
        }
        public GameObject ObjectActivation(string ID, Vector3 position)
        {
            return ObjectActivation(ID, position, Quaternion.identity);
        }

        public GameObject ObjectActivation(string ID, Vector3 position, Quaternion rotation)
        {
            Debug.Log(ID);
            Debug.Log(FastAccess);
            if (FastAccess.ContainsKey(ID))
            {
                if (FastAccess[ID].Index >= FastAccess[ID].TransformContainers.Count)
                {
                    FastAccess[ID].Index = 0;
                }

                var container = FastAccess[ID].TransformContainers[FastAccess[ID].Index];
                container.transform.position = position;
                container.transform.rotation = rotation;
                container.SetActive(true);
                FastAccess[ID].Index++;
                return container;
            }

            return null;
        }
    }

    [Serializable]
    public class PoolObjects
    {
        public string Id;
        public GameObject Example;
        [HideInInspector] public List<GameObject> TransformContainers;
        public int Index;
        [Range(1, 100)] public int PoolAmount = 5;

    }
}