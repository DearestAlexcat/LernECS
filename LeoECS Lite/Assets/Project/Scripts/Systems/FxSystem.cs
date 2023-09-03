using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class FxSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Component<GameObject>, BloodFXRequest>> _bloodFilter = null;
        private readonly EcsFilterInject<Inc<Component<GameObject>, HealFXRequest>> _healFilter = null;
        
        private readonly EcsCustomInject<Prefabs> _prefabs = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        public void Run(EcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            foreach (var it in _bloodFilter.Value)
            {
                var fx = Object.Instantiate(_prefabs.Value.BloodFx, _bloodFilter.Pools.Inc1.Get(it).Value.transform.position, Quaternion.identity);
                Object.Destroy(fx, 10f);
            }

            foreach (var it in _healFilter.Value)
            {
                var fx = Object.Instantiate(_prefabs.Value.HealFx, _healFilter.Pools.Inc1.Get(it).Value.transform.position, Quaternion.identity);
                Object.Destroy(fx, 10f);
            }
        }
    }
}