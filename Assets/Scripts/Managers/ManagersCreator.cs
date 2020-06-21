using UnityEngine;

namespace testProjectTemplate
{
    public class ManagersCreator : MonoBehaviour
    {
        [Header("Game managers settings")]
        [SerializeField] private bool isDebug = true;
        [SerializeField] private StateGameEnum curStateGame = StateGameEnum.Menu;
     
        [Header("Main Game Settings")]
        [SerializeField] private string settingName = "GameConfig";

        [Header("Data managers settings")] 
        [SerializeField] private string profileName = "MainProfile";
        [SerializeField] private bool clearProfile = false;
        [SerializeField] private DefaultProfile defaultProfile = null;

        void Awake()
        {
            if (!GameManager.IsAwake)
            {
                DataManager.Instance.Init(profileName, clearProfile, defaultProfile);
                GameManager.Instance.Init(isDebug, settingName, curStateGame);
               
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
                Application.targetFrameRate = 60;
                
#if !UNITY_EDITOR
                if (!Debug.isDebugBuild)
                {
                   Debug.unityLogger.logEnabled = false;
                }
#endif
            }
        }
    }
}