using Leopotam.Ecs;
using UnityEngine;


namespace Client
{
    sealed class CreateUnitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;

        private readonly Prefabs _prefabs = null;

        private readonly UI _gameUI = null;


        public void Init()
        {
            CreateOrc("�������", 90, 90, 12f);
            CreateOrc("�������", 102, 102, 14f);

            CreatePlayer("Player Alex", 1000, 1000, 20);

            CreateElf("��������", 110f, 110f, 10f);

            _world.NewEntity().Get<PlayerTurnComponent>();
        }

        private EcsEntity CreateUnit(string name, float hp, float maxHP, float damage)
        {
            // ������� �����
            var entity = _world.NewEntity();

            ref var data = ref entity.Get<UnitComponent>(); // �������� �����
            data.Name = name;
            data.HP = hp;
            data.MaxHP = maxHP;
            data.Damage = damage;

            _world.SendMessage("������ unit � ������ " + name + " � hp " + hp + " � ������ " + damage);

            return entity;
        }

        private void CreateObject(EcsEntity entity, GameObject prefab, string name)
        {
            var go = GameObject.Instantiate(prefab, _gameUI.HolderUnits);
            go.GetComponent<Unit>().Entity = entity;

            entity.Get<Component<GameObject>>().Value = go;
            entity.Get<Component<GameObject>>().Value.name = name;

            entity.Get<Component<SliderHP>>().Value = go.GetComponent<SliderHP>();
        }

        private void CreatePlayer(string name, float hp, float maxHP, float damage)
        {
            var entity = CreateUnit(name, hp, maxHP, damage);

            entity.Get<PlayerComponent>();  // ��������, ��� ��� entity �������� Player

            ref var hpText = ref entity.Get<Component<SliderHP>>();
            hpText.Value = _gameUI.PlayerSlider;
            hpText.Value.UpdateSliderText(hp, maxHP);

            _gameUI.PlayerNameText.text = name;
        }

        private void CreateOrc(string name, float hp, float maxHP, float damage)
        {
            // ������� �����
            var orcEntity = CreateUnit("[Orc] " + name, hp, maxHP, damage);

            // �������� ����� EnemyComponent
            orcEntity.Get<EnemyComponent>();  // ��������, ��� ��� entity �������� Player

            ref var orcData = ref orcEntity.Get<UnitComponent>();

            // Abilitys

            ref var ability = ref orcEntity.Get<ShieldAbilityComponent>();
            ability.Name = "���";
            ability.Chance = 50f;
            ability.BlockValue = 50f;

            ref var secondAblility = ref orcEntity.Get<SecondChanceAblilityComponent>();
            secondAblility.Name = "������ ����";
            secondAblility.Chance = 50f;

            // Objects

            CreateObject(orcEntity, _prefabs.OrcPrefab, orcData.Name);
            orcEntity.Get<Component<SliderHP>>().Value.UpdateSliderText(hp, maxHP);

            // Messages

            _world.SendMessage("�� ������������ " + ability.Name + ", ������ " + ability.Chance + ", ������ " + ability.BlockValue);
            _world.SendMessage("�� ������������ " + secondAblility.Name + ", ������ " + ability.Chance);
        }

        private void CreateElf(string name, float hp, float maxHP, float damage)
        {
            var elfEntity = CreateUnit("[Elf] " + name, hp, maxHP, damage);

            elfEntity.Get<EnemyComponent>();  // ��������, ��� ��� entity �������� Player

            ref var elfData = ref elfEntity.Get<UnitComponent>();

            // elfEntity.Get<IsGodComponent>();

            // Abilitys

            ref var ability = ref elfEntity.Get<AbilityHealerComponent>();

            ability.Name = "�����";
            ability.MinHealerValue = 5f;
            ability.MaxHealerValue = 10f;

            // Objects

            CreateObject(elfEntity, _prefabs.ElfPrefab, elfData.Name);
            elfEntity.Get<Component<SliderHP>>().Value.UpdateSliderText(hp, maxHP);

            // Messages

            _world.SendMessage("�� ������������ " + ability.Name);
        }

    }
}