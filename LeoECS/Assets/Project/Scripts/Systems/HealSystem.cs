using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class HealSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;

        // если висит DestroyRequestComponent, выличить уже нельзя т.к. юнит считается мертвым
        private readonly EcsFilter<UnitComponent, HealRequest, Component<SliderHP>>.Exclude<DestroyRequestComponent> _unitFilter = null; 

        public void Run()
        {
            foreach (var it in _unitFilter)
            {
                ref var unitHeal = ref _unitFilter.Get1(it);
                var request = _unitFilter.Get2(it);
                var unitHPUI = _unitFilter.Get3(it);

                unitHeal.HP = Mathf.Min(unitHeal.HP + request.Value, unitHeal.MaxHP);
                unitHPUI.Value.UpdateSliderText((int)unitHeal.HP, unitHeal.MaxHP);

                _world.SendMessage($"{request.Sender} вылечил {unitHeal.Name}");

                _unitFilter.GetEntity(it).Get<HealFXRequest>();
                _unitFilter.GetEntity(it).Get<PopUpRequest>().color = Color.green;
                _unitFilter.GetEntity(it).Get<PopUpRequest>().Message = "+" + (int)request.Value;
            }
        }
    }
}