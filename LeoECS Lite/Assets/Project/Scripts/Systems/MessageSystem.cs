using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// MessageSystem is executed if there is an entity with a MessageRequestComponent component
    /// </summary>

    sealed class MessageSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MessageRequestComponent>> _filter = default;
        private readonly EcsCustomInject<Prefabs> _prefabs = default;
        private readonly EcsCustomInject<UI> _gameUI = default;

        public void Init(EcsSystems systems)
        {
            _gameUI.Value.clearButton.onClick.AddListener(() =>
            {
                foreach (RectTransform item in _gameUI.Value.MessageScrollRect.content)    // Обход всех вложенных объектов содержащих компонент Transform
                {
                    GameObject.Destroy(item.gameObject);
                }
            });
        }

        public void Run (EcsSystems systems)
        {
            foreach (var it in _filter.Value)
            {
                var logText = GameObject.Instantiate<UnityEngine.UI.Text>(_prefabs.Value.LogTextPrefab, _gameUI.Value.MessageScrollRect.content);

                logText.text = _filter.Pools.Inc1.Get(it).Message;

                logText.transform.SetAsFirstSibling();

                //Debug.Log(_filter.Get1(it).Message);
            }
        }
    }
}