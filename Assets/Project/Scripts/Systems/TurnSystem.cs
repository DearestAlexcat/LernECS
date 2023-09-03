using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class TurnSystem : IEcsRunSystem
    {
        // Для ожидание клика игрока
        private readonly EcsFilter<ViewEventClick> _clicksFilter = null;
        private readonly EcsFilter<PlayerTurnComponent> _playerTurnsFilter = null;

        private readonly EcsFilter<EnemyEndTurnEvent> _enemyEndFilter = null;
        private readonly EcsFilter<EnemyTurnComponent> _enemyTurnsFilter = null;


        public void Run()
        {
            foreach (var it in _clicksFilter)
            {
                foreach (var it2 in _playerTurnsFilter)
                {
                    var turnEntity = _playerTurnsFilter.GetEntity(it2);
                    turnEntity.Get<EnemyTurnComponent>();
                    turnEntity.Del<PlayerTurnComponent>();
                }
            }

            foreach (var it in _enemyEndFilter)
            {
                foreach (var it2 in _enemyTurnsFilter)
                {
                    var turnEntity = _enemyTurnsFilter.GetEntity(it2);
                    turnEntity.Get<PlayerTurnComponent>();
                    turnEntity.Del<EnemyTurnComponent>();
                }
            }
        }
    }
}