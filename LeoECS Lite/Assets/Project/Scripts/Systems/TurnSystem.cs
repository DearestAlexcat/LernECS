using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class TurnSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;

        // To wait for the player's click
        private readonly EcsFilterInject<Inc<ViewEventClick>> _clicksFilter = null;
        private readonly EcsFilterInject<Inc<PlayerTurnComponent>> _playerTurnsFilter = null;
        private readonly EcsFilterInject<Inc<EnemyEndTurnEvent>> _enemyEndFilter = null;
        private readonly EcsFilterInject<Inc<EnemyTurnComponent>> _enemyTurnsFilter = null;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        public void Run (EcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            foreach (var it in _clicksFilter.Value)
            {
                foreach (var it2 in _playerTurnsFilter.Value)
                {
                    _world.Value.AddEntity<EnemyTurnComponent>(it2);
                    _world.Value.DelEntity<PlayerTurnComponent>(it2);
                }
            }

            foreach (var it in _enemyEndFilter.Value)
            {
                foreach (var it2 in _enemyTurnsFilter.Value)
                {
                    _world.Value.AddEntity<PlayerTurnComponent>(it2);
                    _world.Value.DelEntity<EnemyTurnComponent>(it2);
                }
            }
        }
    }
}