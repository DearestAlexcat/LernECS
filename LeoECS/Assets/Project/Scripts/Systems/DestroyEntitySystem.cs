using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class DestroyEntitySystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<DestroyRequestComponent> _filter = null;
        private readonly EcsFilter<Component<GameObject>, DestroyRequestComponent> _filterGO = null;

        public void Run()
        {
            // Уничтожает объекты. Можно обойтись и без цикла
            foreach (var item in _filterGO)
            {
                ref var @object = ref _filterGO.Get1(item);
                GameObject.Destroy(@object.Value);
            }

            // Учичтожает сущность
            foreach (var item in _filter)
            {
                ref var request = ref _filter.Get1(item);

                _world.SendMessage($"{request.KillerName} убил {request.UnitName}");

                // GameObject.Destroy(_filter.GetEntity(item).Get<Component<GameObject>>().Value); // Можно так, а можно расширить фильтр

                _filter.GetEntity(item).Destroy();  // Удаляем юнит в том случае если на нем висит компонент DestroyRequestComponent
            }
        }
    }
}