using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class AttackPlayerSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;

       // private readonly EcsFilter<UnitComponent>.Exclude<IsGodComponent> _filter = null; // ��������� �������� ��� �������� ��������� IsGodComponent

        private readonly EcsFilter<ViewEventClick> _clicksFilter = null;                    // ��������� ��������� �����
        private readonly EcsFilter<UnitComponent, PlayerComponent> _playersFilter = null;   // ��� ������

        private readonly EcsFilter<PlayerTurnComponent> _turnPlayer = null;   // ��� ������


        //private readonly UI _gameUI = null;

        //public void Init()
        //{
        //    _gameUI.attackButton.onClick.AddListener(() =>
        //        {
        //            var damage = Random.Range(10, 20);
        //            _world.SendMessage(new string('-', 20));
        //            _world.SendMessage($"����� ������� {damage} ������ �����");

        //            foreach (var it in _filter)
        //            {
        //                ref var entity = ref _filter.GetEntity(it);
        //                ref var attacking = ref entity.Get<DamageRequestComponent>();   // ������ ��������� �� entity

        //                attacking.Sender = "Player";
        //                attacking.Value = damage;
        //            }

        //            _world.NewEntity().Get<TurnComponent>();
        //        });
        //}

        public void Run()
        {
            if (_turnPlayer.IsEmpty()) return;

            foreach (var it in _clicksFilter)
            {
                // ��� ������, ��� ������ ���� ������� ����
                foreach (var it2 in _playersFilter)
                {
                    var playerUnit = _playersFilter.Get1(it2);

                    _world.SendMessage(new string('-', 40));
                    _world.SendMessage($"{playerUnit.Name} ������� {playerUnit.Damage} ������ �����");

                    ref var entity = ref _clicksFilter.GetEntity(it);
                    ref var attacking = ref entity.Get<DamageRequestComponent>();   // ������ ��������� �� entity

                    attacking.Sender = playerUnit.Name;
                    attacking.Value = playerUnit.Damage;
                }

            }
        }

    }
}