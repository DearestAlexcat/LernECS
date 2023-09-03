using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class AttackEnemySystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;

        private readonly EcsFilterInject<Inc<EnemyTurnComponent>> _turnEnemy = default; // Do opponent's move
        private readonly EcsFilterInject<Inc<UnitComponent, EnemyComponent>, Exc<EndTurnComponent>> _enemyFilter = null; // Exclude those who have already made a move
        private readonly EcsFilterInject<Inc<EndTurnComponent>> _turnsEnemies = default; 
        private readonly EcsFilterInject<Inc<PlayerComponent>> _playerFilter = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        public void Run(EcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            if (_turnEnemy.Value.IsEmpty()) return;

            if(_enemyFilter.Value.IsEmpty()) // If all mobs have already made a move
            {
                foreach (var it in _turnsEnemies.Value)
                {
                    _turnsEnemies.Pools.Inc1.Del(it);    
                }
            }

            foreach (var it in _enemyFilter.Value)
            {
                var enemyUnit = _enemyFilter.Pools.Inc1.Get(it);

                // All players deal damage
                foreach (var it2 in _playerFilter.Value)
                {
                    _world.Value.SendMessage(new string('-', 40));
                    _world.Value.SendMessage($"{enemyUnit.Name} наносит {enemyUnit.Damage} единиц урона");

                    ref var attacking = ref _world.Value.AddEntityRef<DamageRequestComponent>(it2); // Adding component

                    attacking.Sender = enemyUnit.Name;
                    attacking.Value = enemyUnit.Damage;
                }

                _world.Value.AddEntity<EndTurnComponent>(it);
                
                break;
            }

            _world.Value.NewEntity<EnemyEndTurnEvent>();
        }
    }
}