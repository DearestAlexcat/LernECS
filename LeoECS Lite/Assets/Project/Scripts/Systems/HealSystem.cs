using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class HealSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;

        // If the DestroyRequestComponent, unit is considered dead
        private readonly EcsFilterInject<Inc<UnitComponent, HealRequest, Component<SliderHP>>, Exc<DestroyRequestComponent>> _unitFilter = null;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        public void Run (EcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            foreach (var it in _unitFilter.Value)
            {
                ref var unitHeal = ref _unitFilter.Pools.Inc1.Get(it);
                var request = _unitFilter.Pools.Inc2.Get(it);
                var unitHPUI = _unitFilter.Pools.Inc3.Get(it);

                unitHeal.HP = Mathf.Min(unitHeal.HP + request.Value, unitHeal.MaxHP);
                unitHPUI.Value.UpdateSliderText((int)unitHeal.HP, unitHeal.MaxHP);

                _world.Value.SendMessage($"{request.Sender} вылечил {unitHeal.Name}");

                _world.Value.AddEntity<HealFXRequest>(it);

                ref var popup = ref _world.Value.AddEntityRef<PopUpRequest>(it);
                popup.color = Color.green;
                popup.Message = "+" + (int)request.Value;
            }
        }
    }
}