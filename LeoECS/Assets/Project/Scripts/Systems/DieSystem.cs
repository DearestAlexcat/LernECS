using Leopotam.Ecs;
using UnityEngine.SceneManagement;

namespace Client
{
    sealed class DieSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<PlayerComponent> _playerFilter = null;

        private readonly UI _gameUI = null;

        public void Init()
        {
            _gameUI.DieWindow.buttonRestart.onClick.AddListener(() => SceneManager.LoadScene(0));
        }

        public void Run()
        {
            if (_playerFilter.IsEmpty())
            {
                _gameUI.DieWindow.SetActiveWindow(true);
            }
        }
    }
}