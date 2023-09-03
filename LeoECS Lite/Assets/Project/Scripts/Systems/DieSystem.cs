using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine.SceneManagement;

namespace Client
{
    sealed class DieSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<PlayerComponent>> _playerFilter = default;
        private readonly EcsCustomInject<UI> _gameUI = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        public void Init(EcsSystems systems)
        {
            _gameUI.Value.DieWindow.buttonRestart.onClick.AddListener(() => Levels.LoadCurrent());
        }

        public void Run (EcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            if (_playerFilter.Value.IsEmpty())
            {
                _gameUI.Value.DieWindow.SetActiveWindow(true);

                systems.GetWorld().ChangeState(GameState.LOSE);
            }
        }
    }
}