using Zenject;

namespace Gameplay.Scripts.DataManagement
{
    public class PlayerPrefsSaveManager
    {
        private DiContainer _diContainer;
        public PlayerPrefsData PrefsData { get; private set; }


        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public void Init()
        {
            PrefsData =  DataController.ReadUserDataFromFileAsync() ?? new PlayerPrefsData();
            
            PrefsData.Initialize(_diContainer);
        }

        public void ForceSave()
        {
            DataController.SaveUserDataToFileAsync(PrefsData);
        }
        
        public static void DeleteData()
        {
            DataController.ResetProgress();
        }
        
        
    }
}