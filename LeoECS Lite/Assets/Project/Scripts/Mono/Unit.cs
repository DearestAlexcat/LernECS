using Client;
using UnityEngine.EventSystems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class Unit : MonoBehaviour, IEntity, IPointerClickHandler
{
    int entity;
    public int Entity { get => entity; set => entity = value; }

    public void OnPointerClick(PointerEventData eventData)
    {
        Service<EcsWorld>.Get().AddEntity<ViewEventClick>(entity);
    }
}

interface IEntity
{
    public int Entity { get; set; }
}
