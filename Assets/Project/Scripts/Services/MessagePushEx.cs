using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
   public static class MessagePushEx
    {
        public static void SendMessage(this EcsWorld _world, string message)
        {
            _world.NewEntity().Get<MessageRequestComponent>().Message = message;
        }
    }
}