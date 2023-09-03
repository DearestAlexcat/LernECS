using Leopotam.Ecs;
using UnityEngine.SceneManagement;

namespace Client
{
    sealed class WinSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<EnemyComponent> _enemiesFilter = null;

        private readonly UI _gameUI = null;

        public void Init()
        {
            _gameUI.WinWindow.buttonRestart.onClick.AddListener(() => SceneManager.LoadScene(0));
        }

        public void Run()
        {
            if(_enemiesFilter.IsEmpty())
            {
                _gameUI.WinWindow.SetActiveWindow(true);
            }
        }
    } 
}