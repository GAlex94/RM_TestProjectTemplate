using System.Collections;
using System.Linq;
using UnityEngine;

namespace testProjectTemplate
{
    public class GameManager : Singleton<GameManager>
    {
        private bool isDebug = true;
        public bool IsDebug => isDebug;

        [field: SerializeField] public StateGameEnum CurrentStateGame { get; private set; }

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            StartCoroutine(DefferGameStart());
        }

        public void Init(bool isDebug, StateGameEnum gameState)
        {
            this.isDebug = isDebug;
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
    }

    public enum StateGameEnum
    {
        Menu,
        Pause,
        Game,
    }
}
