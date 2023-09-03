using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class FxSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Component<GameObject>, BloodFXRequest> _bloodFilter = null;
        private readonly EcsFilter<Component<GameObject>, HealFXRequest> _healFilter = null;
        private readonly Prefabs _prefabs = null;

        public void Run()
        {
            foreach (var it in _bloodFilter)
            {
                var fx = Object.Instantiate(_prefabs.BloodFx, _bloodFilter.Get1(it).Value.transform.position, Quaternion.identity);
                Object.Destroy(fx, 10f);
            }

            foreach (var it in _healFilter)
            {
                var fx = Object.Instantiate(_prefabs.HealFx, _healFilter.Get1(it).Value.transform.position, Quaternion.identity);
                Object.Destroy(fx, 10f);
            }
        }
    }
}