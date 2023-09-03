using Client;
using UnityEngine.EventSystems;
using Leopotam.Ecs;
using UnityEngine;

public class Unit : MonoBehaviour, IEntity, IPointerClickHandler
{
    EcsEntity entity;
    public EcsEntity Entity { get => entity; set => entity = value; }

    public void OnPointerClick(PointerEventData eventData)
    {
        Entity.Get<ViewEventClick>();
    }
}

interface IEntity
{
    public EcsEntity Entity { get; set; }
}
