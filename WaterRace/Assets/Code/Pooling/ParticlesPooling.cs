using System;
using System.Collections.Generic;

using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using Zenject;

namespace Code.Pooling
{
    public class ParticlesPooling : MonoBehaviour
    {
        public List<Particles> Particles = new List<Particles>();
        [Range(1, 100)] public int PoolAmount = 5;
        public Dictionary<string, Particles> FastAccess;

        [Inject]
        private void Init()
        {
            FastAccess = new Dictionary<string, Particles>();
            foreach (var container in Particles)
            {

                for (int i = 0; i < PoolAmount; i++)
                {
                    var instantiate = Instantiate(container.Example, new Vector3(-99f, -99f, -99f), Quaternion.identity, transform);
                    var system = instantiate.GetComponent<ParticleSystem>();
                    container.ParticlesContainers.Add(system);
                    container.Index = 0;
                }
                FastAccess.Add(container.Id, container);
            }
        }
        public ParticleSystem PlayParticle(string ID, Vector3 position)
        {
            return PlayParticle(ID, position, Quaternion.identity);
        }

        public ParticleSystem PlayParticle(string ID, Vector3 position, Quaternion rotation)
        {
            if (FastAccess.ContainsKey(ID))
            {
                if (FastAccess[ID].Index >= FastAccess[ID].ParticlesContainers.Count)
                {
                    FastAccess[ID].Index = 0;
                }

                var container = FastAccess[ID].ParticlesContainers[FastAccess[ID].Index];
                container.transform.position = position;
                container.transform.rotation = rotation;
                container.Play();
                FastAccess[ID].Index++;
                return container;
            }

            return null;
        }
    }

    [Serializable]
    public class Particles
    {
        public string Id;
        public GameObject Example;
        [HideInInspector] public List<ParticleSystem> ParticlesContainers;
        public int Index;
    }
}