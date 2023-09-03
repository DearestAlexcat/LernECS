using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed  class DamageSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;

        private readonly EcsFilterInject<Inc<UnitComponent, DamageRequestComponent, Component<SliderHP>>> _filter = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        public void Run (EcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            foreach (var it in _filter.Value)
            {
                ref var unit = ref _filter.Pools.Inc1.Get(it);
                ref var damage = ref _filter.Pools.Inc2.Get(it);
                ref var damageUI = ref _filter.Pools.Inc3.Get(it);

                unit.HP = Mathf.Max(0, unit.HP - damage.Value);

                damageUI.Value.UpdateSliderText((int)unit.HP, unit.MaxHP);

                _world.Value.SendMessage(unit.Name + " получил урон " + damage.Value);

                if(unit.HP <= 0)
                {
                    ref var destro = ref _world.Value.AddEntityRef<DestroyRequestComponent>(it);
                    destro.KillerName = damage.Sender;
                    destro.UnitName = unit.Name;
                }


                _world.Value.AddEntity<BloodFXRequest>(it);

                ref var popup = ref _world.Value.AddEntityRef<PopUpRequest>(it);
                popup.color = Color.red;
                popup.Message = "-" + (int)damage.Value;
            }
        }
    }
}