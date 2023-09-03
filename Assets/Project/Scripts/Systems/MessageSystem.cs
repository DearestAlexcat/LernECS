using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
	/// <summary>
    /// MessageSystem выполняется если есть сущность с компонентом MessageRequestComponent
    /// </summary>

    sealed class MessageSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<MessageRequestComponent> _filter = null;

        private readonly Prefabs _prefabs = null;

        private readonly UI _gameUI = null;

        public void Init()
        {
            _gameUI.clearButton.onClick.AddListener(() =>
            {
                foreach (RectTransform item in _gameUI.MessageScrollRect.content)    // Обход всех вложенных объектов содержащих компонент Transform
                {
                    GameObject.Destroy(item.gameObject);
                }
            });
        }

        public void Run()
        {
            foreach (var it in _filter)
            {
                var logText = GameObject.Instantiate<UnityEngine.UI.Text>(_prefabs.LogTextPrefab, _gameUI.MessageScrollRect.content);

                logText.text = _filter.Get1(it).Message;

                logText.transform.SetAsFirstSibling();

                //Debug.Log(_filter.Get1(it).Message);
            }
        }
    }
}