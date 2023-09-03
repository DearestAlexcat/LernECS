using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class AttackPlayerSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;

        //private readonly EcsFilterInject<Inc<UnitComponent>, Exc<IsGodComponent>> _turnEnemy = default;
        private readonly EcsFilterInject<Inc<ViewEventClick>> _clicksFilter = default; // Attacked enemy units
        private readonly EcsFilterInject<Inc<UnitComponent, PlayerComponent>> _playersFilter = default;
        private readonly EcsFilterInject<Inc<PlayerTurnComponent>> _turnPlayer = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        //private readonly UI _gameUI = null;

        //public void Init()
        //{
        //    _gameUI.attackButton.onClick.AddListener(() =>
        //        {
        //            var damage = Random.Range(10, 20);
        //            _world.Value.SendMessage(new string('-', 20));
        //            _world.Value.SendMessage($"Игрок наносит {damage} единиц урона");

        //            foreach (var it in _filter.Value)
        //            {
        //                ref var entity = ref _filter.GetEntity(it);
        //                ref var attacking = ref entity.Get<DamageRequestComponent>();   // Вешаем компонент на entity

        //                attacking.Sender = "Player";
        //                attacking.Value = damage;
        //            }

        //            _world.NewEntity().Get<TurnComponent>();
        //        });
        //}

        public void Run (EcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            if (_turnPlayer.Value.IsEmpty()) return;

            foreach (var it in _clicksFilter.Value)
            {
                // All players that are there deal damage
                foreach (var it2 in _playersFilter.Value)
                {
                    var playerUnit = _playersFilter.Pools.Inc1.Get(it2);

                    _world.Value.SendMessage(new string('-', 40));
                    _world.Value.SendMessage($"{playerUnit.Name} наносит {playerUnit.Damage} единиц урона");

                    ref var attacking = ref _world.Value.AddEntityRef<DamageRequestComponent>(it); // Adding component
                    attacking.Sender = playerUnit.Name;
                    attacking.Value = playerUnit.Damage;
                }

            }
        }

    }
}