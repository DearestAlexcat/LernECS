using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
     sealed class AttackEnemySystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;

        private readonly EcsFilter<EnemyTurnComponent> _turnEnemy = null;   // ����� �� ����������
        
		private readonly EcsFilter<UnitComponent, EnemyComponent>.Exclude<EndTurnComponent> _enemyFilter = null; // ��������� ��� ��� ��� �������

        private readonly EcsFilter<EndTurnComponent> _turnsEnemies = null;

        private readonly EcsFilter<PlayerComponent> _playerFilter = null;

        public void Run()
        {
            if (_turnEnemy.IsEmpty()) return;

            if(_enemyFilter.IsEmpty()) // ���� ��� ���� ��� ��������
            {
                foreach (var it in _turnsEnemies)
                {
                    _turnsEnemies.GetEntity(it).Del<EndTurnComponent>();
                    _enemyFilter.OnAddEntity(_turnsEnemies.GetEntity(it)); // ������� ��������� ������
                }
            }

            foreach (var it in _enemyFilter)
            {
                var enemyUnit = _enemyFilter.Get1(it);

                // ��� ������, ��� ������ ���� ������� ����
                foreach (var it2 in _playerFilter)
                {
                    _world.SendMessage(new string('-', 40));
                    _world.SendMessage($"{enemyUnit.Name} ������� {enemyUnit.Damage} ������ �����");

                    ref var entity = ref _playerFilter.GetEntity(it2);
                    ref var attacking = ref entity.Get<DamageRequestComponent>();   // ������ ��������� �� entity

                    attacking.Sender = enemyUnit.Name;
                    attacking.Value = enemyUnit.Damage;
                }

                if(_enemyFilter.GetEntity(it).IsAlive())
                {
                    _enemyFilter.GetEntity(it).Get<EndTurnComponent>();
                }

                break;
            }

            _world.NewEntity().Get<EnemyEndTurnEvent>();
        }
    }
}