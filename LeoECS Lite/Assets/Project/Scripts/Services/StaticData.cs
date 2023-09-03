using UnityEngine;

namespace Client
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        [Header("Levels")]
        public Levels ThisLevels;

        [Header("Required prefabs")]
        public UI UI;

        [Header("Gameplay variable")] 
        public float TimeToWinLevel = 1;
    }
}
