using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class HealAbilitySystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;

        private readonly EcsFilterInject<Inc<ViewEventClick>> _clicksFilter = default;                          // When clicking on a unit, we create an entity with a component
        private readonly EcsFilterInject<Inc<UnitComponent, AbilityHealerComponent>> _healerFilter = default;   // Those who heal
        private readonly EcsFilterInject<Inc<UnitComponent>, Exc<PlayerComponent>> _unitFilter = default;       // Those who are being treated
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        public void Run (EcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            if (_clicksFilter.Value.IsEmpty()) return;  // In case of a click on a mob

            foreach (var it1 in _healerFilter.Value)  // alternately heal the most beaten unit from 1 to n
            {
                var unitHealer = _healerFilter.Pools.Inc1.Get(it1);  
                var ability = _healerFilter.Pools.Inc2.Get(it1);      
                
                float maxHP = int.MaxValue;

                int unitEnityForHeal = -1;

                // Look for the most beaten unit
                foreach (var it2 in _unitFilter.Value)
                {
                    var unit = _unitFilter.Pools.Inc1.Get(it2);
                
                    if (unit.MaxHP - unit.HP > float.Epsilon && unit.HP < maxHP)
                    {
                        maxHP = unit.HP;
                        unitEnityForHeal = it2;
                    }
                }

                if (unitEnityForHeal < 0) continue;

                // Hang up a request for treatment
                ref var unitHeal = ref _world.Value.AddEntityRef<HealRequest>(unitEnityForHeal);
                unitHeal.Sender = unitHealer.Name;
                unitHeal.Value = Random.Range(ability.MinHealerValue, ability.MaxHealerValue);
            }
        }
    }
}