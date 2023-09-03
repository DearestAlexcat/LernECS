namespace Client 
{

    struct ChangeStateEvent
    {
        public GameState NewGameState;
    }

    struct MessageRequestComponent // OnFrame
    {
        public string message;
        public string Message { get => message; set => message = value; }
    }

    struct UnitComponent
    {
        public string Name;
        public float HP;
        public float MaxHP;
        public float Damage;
    }

    struct PlayerComponent
    {

    }

    struct EnemyComponent
    {

    }

    struct HealRequest
    {
        public string Sender;   
        public float Value;    
    }

    struct DamageRequestComponent // OnFrame
    {
        public string Sender;   
        public float Value;    
    }


    struct DestroyRequestComponent  // OnFrame
    {
        public string UnitName;    
        public string KillerName;   
    }

    struct ShieldAbilityComponent   // Init
    {
        public string Name;         
        public float Chance;        
        public float BlockValue;   
    }

    struct SecondChanceAblilityComponent  // Init 
    {
        public string Name;        
        public float Chance;          
    }

    struct IsGodComponent   // Flag to exclude the Unit from the search
    {

    }

    struct AbilityHealerComponent 
    {
        public string Name;           
        public float MinHealerValue;   
        public float MaxHealerValue; 
    }

    struct TurnComponent // OnFrame 
    {
        // Flag that checks if an attack was made. 
        // Blocks execution of HealerSystem in every frame
    }

    struct Component<T>
    {
        public T Value;
    }

    struct ViewEventClick
    {

    }

    struct BloodFXRequest
    {

    }

    struct HealFXRequest
    {

    }

    struct PopUpRequest
    {
        public string Message;
        public UnityEngine.Color color;
    }

    struct PlayerTurnComponent
    {

    }

    struct EnemyTurnComponent
    {

    }

    struct EnemyEndTurnEvent
    {

    }

    struct EndTurnComponent
    {

    }

}