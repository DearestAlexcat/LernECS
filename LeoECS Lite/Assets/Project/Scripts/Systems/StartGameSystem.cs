using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Client
{
    internal class StartGameSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        private readonly EcsWorldInject _world = default;
 

        public void Run(EcsSystems systems)
        {
            if (_runtimeData.Value.GameState == GameState.BEFORE)
            {
                if (EventSystem.current.IsPointerOverGameObject() || EventSystem.current.IsPointerOverGameObject(0))
                {
                    return;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    _world.Value.ChangeState(GameState.PLAYING);
                }
            }
        }
    }
}