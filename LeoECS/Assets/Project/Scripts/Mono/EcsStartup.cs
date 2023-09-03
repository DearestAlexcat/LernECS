using Leopotam.Ecs;
using UnityEngine;


namespace Client {
    sealed class EcsStartup : MonoBehaviour {

        public UI GameUI;
        public Prefabs prefabs;

        EcsWorld _world;
        EcsSystems _systems;

        void Start() {

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif

            _systems
                .Add(new CreateUnitSystem())

                .Add(new AttackPlayerSystem())
                .Add(new AttackEnemySystem())

                .Add(new ShieldAbilitySystem())
                .Add(new SecondAbilitySystem())

                .Add(new DamageSystem())
                .OneFrame<DamageRequestComponent>()     // �� ������ ����. ���� �������� ������ ���������

                .Add(new PopUpSystem())
                .OneFrame<PopUpRequest>()

                .Add(new HealAbilitySystem())
                .Add(new HealSystem())
                .OneFrame<HealRequest>()

                .Add(new FxSystem())
                .OneFrame<HealFXRequest>()
                .OneFrame<BloodFXRequest>()

                .Add(new DestroyEntitySystem())
                .OneFrame<DestroyRequestComponent>()

                .Add(new WinSystem())
                .Add(new DieSystem())

                .Add(new MessageSystem())

                .Add(new TurnSystem())

                .OneFrame<MessageRequestComponent>()
                .OneFrame<TurnComponent>()
                .OneFrame<ViewEventClick>()
                .OneFrame<EnemyEndTurnEvent>()

                .Inject(GameUI)         // � ������ ������� �������� ������ �� ����� UI
                .Inject(prefabs)

                .Init();    // �������� Init �� ���� ��������� ��������
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