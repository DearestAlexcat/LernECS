using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine.SceneManagement;

namespace Client
{
    sealed class WinSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<EnemyComponent>> _enemiesFilter = default;
        private readonly EcsCustomInject<UI> _gameUI = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        public void Init(EcsSystems systems)
        {
            _gameUI.Value.WinWindow.buttonRestart.onClick.AddListener(() => Levels.LoadCurrent());
        }

        public void Run (EcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            if (_enemiesFilter.Value.IsEmpty())
            {
                _gameUI.Value.WinWindow.SetActiveWindow(true);

                systems.GetWorld().ChangeState(GameState.WIN);
            }
        }
    } 
}