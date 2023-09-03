using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class ExampleGameSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsWorldInject _world = default;

        public void Run(EcsSystems systems)
        {
            if (_runtimeData.Value.GameState == GameState.PLAYING)
            {
                if (Time.realtimeSinceStartup - _runtimeData.Value.LevelStartedTime > _staticData.Value.TimeToWinLevel)
                {
                    _world.Value.ChangeState(GameState.WIN);
                }
            }
        }
    }
}