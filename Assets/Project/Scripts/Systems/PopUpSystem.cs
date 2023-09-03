using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class PopUpSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Component<GameObject>, PopUpRequest> _popUpFilter = null;
        private readonly Prefabs _prefabs = null;

        public void Run()
        {
            foreach (var it in _popUpFilter)
            {
                ref var obj = ref _popUpFilter.Get1(it).Value;

                var popUP = Object.Instantiate<PopUpText>(_prefabs.PopUpPrefab, Random.insideUnitSphere + obj.transform.position, Quaternion.identity);

                popUP.MessasgeText = _popUpFilter.Get2(it).Message;
                popUP.Color = _popUpFilter.Get2(it).color;
            }
        }
    }
}