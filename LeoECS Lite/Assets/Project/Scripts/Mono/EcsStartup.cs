using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;


namespace Client {
    sealed class EcsStartup : MonoBehaviour {

        public Prefabs prefabs;

        EcsWorld _world;
        EcsSystems _systems;

        public RuntimeData runtimeData;
        public StaticData staticData;

        void Start() {

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            runtimeData = new RuntimeData();

            Service<RuntimeData>.Set(runtimeData);
            Service<StaticData>.Set(staticData);
            Service<EcsWorld>.Set(_world);

            GameInitialization.FullInit();

            _systems

                .Add(new InitializeSystem())
                .Add(new ChangeStateSystem())

                .Add(new CreateUnitSystem())

                .Add(new AttackPlayerSystem())
                .Add(new AttackEnemySystem())

                .Add(new ShieldAbilitySystem())
                .Add(new SecondAbilitySystem())

                .Add(new DamageSystem())
                .DelHere<DamageRequestComponent>()    

                .Add(new PopUpSystem())
                .DelHere<PopUpRequest>()

                .Add(new HealAbilitySystem())
                .Add(new HealSystem())
                .DelHere<HealRequest>()

                .Add(new FxSystem())
                .DelHere<HealFXRequest>()
                .DelHere<BloodFXRequest>()

                .Add(new DestroyEntitySystem())
                .DelHere<DestroyRequestComponent>()

                .Add(new WinSystem())
                .Add(new DieSystem())

                .Add(new MessageSystem())

                .Add(new TurnSystem())

                .DelHere<MessageRequestComponent>()
                .DelHere<TurnComponent>()
                .DelHere<ViewEventClick>()
                .DelHere<EnemyEndTurnEvent>()

#if UNITY_EDITOR
            .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject(runtimeData, staticData, Service<UI>.Get(), prefabs)
                .Init();    // Call Init on all available systems
        }

        void Update() {
            _systems?.Run();
        }

        void OnDestroy() {
            if (_systems != null) {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}