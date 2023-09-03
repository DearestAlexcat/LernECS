using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed  class DamageSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;

        private readonly EcsFilter<UnitComponent, DamageRequestComponent, Component<SliderHP>> _filter = null; // ������� ���� ������ �� ������� ����� ��������� DamageRequestComponent

        public void Run()
        {
            foreach (var it in _filter)
            {
                ref var unit = ref _filter.Get1(it);
                ref var damage = ref _filter.Get2(it);
                ref var damageUI = ref _filter.Get3(it);

                unit.HP = Mathf.Max(0, unit.HP - damage.Value);

                damageUI.Value.UpdateSliderText((int)unit.HP, unit.MaxHP);

                _world.SendMessage(unit.Name + " ������� ���� " + damage.Value);

                if(unit.HP <= 0)
                {
                    ref var entity = ref _filter.GetEntity(it);

                    // DestroyRequestComponent ����� ������� ����� ���. ����� ��� �� ����� � ������

                    entity.Get<DestroyRequestComponent>().KillerName = damage.Sender;      // �� ���� ������ ��������� DestroyRequestComponent!
                    entity.Get<DestroyRequestComponent>().UnitName = unit.Name;
                }

                _filter.GetEntity(it).Get<BloodFXRequest>();
                _filter.GetEntity(it).Get<PopUpRequest>().color = Color.red; ;
                _filter.GetEntity(it).Get<PopUpRequest>().Message = "-" + (int)damage.Value;
            }
        }
    }
}