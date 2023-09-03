using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class DestroyEntitySystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;

        private readonly EcsFilterInject<Inc<DestroyRequestComponent>> _filter = default;
        private readonly EcsFilterInject<Inc<Component<GameObject>, DestroyRequestComponent>> _filterGO = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;


        public void Run (EcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            foreach (var item in _filterGO.Value)
            {
                ref var @object = ref _filterGO.Pools.Inc1.Get(item);
                GameObject.Destroy(@object.Value);
            }

            foreach (var item in _filter.Value)
            {
                ref var request = ref _filter.Pools.Inc1.Get(item);

                _world.Value.SendMessage($"{request.KillerName} убил {request.UnitName}");
                _world.Value.DelEntity(item);
            }
        }
    }
}