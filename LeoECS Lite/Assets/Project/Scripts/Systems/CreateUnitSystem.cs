using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class CreateUnitSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsCustomInject<Prefabs> _prefabs = default;
        private readonly EcsCustomInject<UI> _gameUI = default;

        public void Init(EcsSystems systems)
        {
            CreateOrc("Аркадий", 90, 90, 12f);
            CreateOrc("Валерий", 102, 102, 14f);

            CreatePlayer("Player Alex", 1000, 1000, 20);

            CreateElf("Зелундий", 110f, 110f, 10f);

            _world.Value.NewEntity<PlayerTurnComponent>();

            _world.Value.ChangeState(GameState.PLAYING);
        }

        private int CreateUnit(string name, float hp, float maxHP, float damage)
        {
            var entity = _world.Value.NewEntity<UnitComponent>();
            ref var data = ref _world.Value.GetEntityRef<UnitComponent>(entity);
            data.Name = name;
            data.HP = hp;
            data.MaxHP = maxHP;
            data.Damage = damage;

            _world.Value.SendMessage("Создан unit с именем " + name + " с hp " + hp + " с уроном " + damage);

            return entity;
        }

        private void CreateObject(int entity, GameObject prefab, string name)
        {
            var go = Object.Instantiate(prefab, _gameUI.Value.HolderUnits);
            go.GetComponent<Unit>().Entity = entity;

            ref var obj = ref _world.Value.AddEntityRef<Component<GameObject>>(entity);
            obj.Value = go;
            obj.Value.name = name;

            _world.Value.AddEntityRef<Component<SliderHP>>(entity).Value = go.GetComponent<SliderHP>();
        }

        private void CreatePlayer(string name, float hp, float maxHP, float damage)
        {
            var entity = CreateUnit(name, hp, maxHP, damage);

            _world.Value.AddEntity<PlayerComponent>(entity);

            ref var hpText = ref _world.Value.AddEntityRef<Component<SliderHP>>(entity);
            hpText.Value = _gameUI.Value.PlayerSlider;
            hpText.Value.UpdateSliderText(hp, maxHP);

            _gameUI.Value.PlayerNameText.text = name;
        }

        private void CreateOrc(string name, float hp, float maxHP, float damage)
        {
            var orcEntity = CreateUnit("[Orc] " + name, hp, maxHP, damage);

            _world.Value.AddEntity<EnemyComponent>(orcEntity);

            var orcData = _world.Value.GetEntityRef<UnitComponent>(orcEntity);

            // Abilitys
            ref var ability = ref _world.Value.AddEntityRef<ShieldAbilityComponent>(orcEntity);
            ability.Name = "Щит";
            ability.Chance = 50f;
            ability.BlockValue = 50f;

            ref var secondAblility = ref _world.Value.AddEntityRef<SecondChanceAblilityComponent>(orcEntity);
            secondAblility.Name = "Второй шанс";
            secondAblility.Chance = 50f;

            // Objects
            CreateObject(orcEntity, _prefabs.Value.OrcPrefab, orcData.Name);
            _world.Value.GetEntityRef<Component<SliderHP>>(orcEntity).Value.UpdateSliderText(hp, maxHP);

            // Messages
            _world.Value.SendMessage("Со способностью " + ability.Name + ", шансом " + ability.Chance + ", блоком " + ability.BlockValue);
            _world.Value.SendMessage("Со способностью " + secondAblility.Name + ", шансом " + ability.Chance);
        }

        private void CreateElf(string name, float hp, float maxHP, float damage)
        {
            var elfEntity = CreateUnit("[Elf] " + name, hp, maxHP, damage);

            _world.Value.AddEntity<EnemyComponent>(elfEntity);

            var elfData = _world.Value.GetEntityRef<UnitComponent>(elfEntity);

            // Abilitys
            ref var ability = ref _world.Value.AddEntityRef<AbilityHealerComponent>(elfEntity);
            ability.Name = "Хилка";
            ability.MinHealerValue = 5f;
            ability.MaxHealerValue = 10f;

            // Objects
            CreateObject(elfEntity, _prefabs.Value.ElfPrefab, elfData.Name);
            _world.Value.GetEntityRef<Component<SliderHP>>(elfEntity).Value.UpdateSliderText(hp, maxHP);

            // Messages
            _world.Value.SendMessage("Со способностью " + ability.Name);
        }

    }
}