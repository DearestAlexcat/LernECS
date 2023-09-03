using Leopotam.EcsLite;

namespace Client
{
   public static class MessagePushEx
    {
        public static void SendMessage(this EcsWorld _world, string message)
        {
            _world.NewEntityRef<MessageRequestComponent>().Message = message;
        }
    }
}