using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class PopUpSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Component<GameObject>, PopUpRequest>> _popUpFilter = default;
        private readonly EcsCustomInject<Prefabs> _prefabs = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        public void Run (EcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            foreach (var it in _popUpFilter.Value)
            {
                ref var obj = ref _popUpFilter.Pools.Inc1.Get(it).Value;

                var popUP = Object.Instantiate<PopUpText>(_prefabs.Value.PopUpPrefab, Random.insideUnitSphere + obj.transform.position, Quaternion.identity);

                popUP.MessasgeText = _popUpFilter.Pools.Inc2.Get(it).Message;
                popUP.Color = _popUpFilter.Pools.Inc2.Get(it).color;
            }
        }
    }
}