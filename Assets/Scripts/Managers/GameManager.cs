using System.Collections;
using System.Linq;
using UnityEngine;

namespace testProjectTemplate
{
    public class GameManager : Singleton<GameManager>
    {
        private string settingName = "GameConfig";

        private bool isDebug = true;
        public bool IsDebug => isDebug;

        [field: SerializeField] public StateGameEnum CurrentStateGame { get; private set; }
        [field: SerializeField] public float SpeedSimulate { get; set; }

        [field: SerializeField]  private MainSettingGame mainGameSetting;

        public GameConfig MainGameSetting => mainGameSetting.GameConfig;

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            StartCoroutine(DefferGameStart());
        }

        public void Init(bool isDebug, string settingName, StateGameEnum gameState)
        {
            this.isDebug = isDebug;
            this.settingName = settingName;
            mainGameSetting = null;
            SpeedSimulate = 1;
            SetStateGame(gameState);
            if (!Debug.isDebugBuild)
            {
                SetStateGame(StateGameEnum.Menu);
                this.isDebug = false;
            }
            else
            {
                if (isDebug)
                {
                    //TODO: test 
                }
            }
        }
       
        public void SetStateGame(StateGameEnum stateGame)
        {
            CurrentStateGame = stateGame;
        }

        public void StartGame(StateGameEnum gameState)
        {
            SetStateGame(gameState);
            StartCoroutine(DefferGameStart());
        }

        IEnumerator DefferGameStart()
        {
            if (mainGameSetting == null)
            {
                StartCoroutine(LoadSetting());
            }

            yield return new WaitForEndOfFrame();

            if (CurrentStateGame == StateGameEnum.Menu)
            {
                Debug.Log("Start Main Menu");

            }
            else
            {
                Debug.Log("Start Game");

                IGame mainGameObject = FindObjectsOfType<MonoBehaviour>().OfType<IGame>().FirstOrDefault();

                if (mainGameObject != null)
                    mainGameObject.StartGame();
                else
                {
                    Debug.LogError("IGame object not found in scene! Game didn't launch..");
                }
            }
        }

        private IEnumerator LoadSetting()
        {
            string filePath = "";

#if UNITY_ANDROID && !UNITY_EDITOR
              filePath = "jar:file://" + Application.dataPath + "!/assets/" + settingName + ".json";
#elif UNITY_IOS && !UNITY_EDITOR
            filePath = Application.dataPath + "/Raw/" + settingName + ".json";
#else
            filePath = Application.dataPath + "/StreamingAssets/" + settingName + ".json";
#endif
            using (WWW www = new WWW(filePath))
            {
                yield return www;
                mainGameSetting = JsonUtility.FromJson<MainSettingGame>(www.text);
            }
        }
    }

    public enum StateGameEnum
    {
        Menu,
        Pause,
        Game,
    }
}
