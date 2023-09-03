using UnityEngine;

namespace Client
{
    static class GameInitialization
    {
        public static void FullInit()
        {
            InitializeUi();
        }

        public static UI InitializeUi()
        {
            var configuration = Service<StaticData>.Get();
            
            var ui = Service<UI>.Get();
            
            if (ui == null)
            {
                ui = Object.Instantiate(configuration.UI);
                ui.gameObject.GetComponent<Canvas>().worldCamera = Camera.main;

                Service<UI>.Set(ui);
            }

            return ui;
        }
    }
}