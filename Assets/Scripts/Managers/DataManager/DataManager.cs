using System.IO;
using UnityEngine;

namespace testProjectTemplate
{
    public class DataManager : Singleton<DataManager>
    {
        private string profileName;
        private bool clearProfileOnStart;
        [SerializeField] private GameData data = new GameData();
        private bool dataDirty = false;

        private DefaultProfile defaultProfile;

        private string FilePath => Path.Combine(Application.persistentDataPath, profileName + ".json");

        public GameData GetCurrentData => data;

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            if (clearProfileOnStart)
            {
                Clear();
            }
            else
            {
                Load();
            }
        }

        public void Init(string profileName, bool clearProfileOnStart, DefaultProfile defaultProfile)
        {
            this.profileName = profileName;
            this.clearProfileOnStart = clearProfileOnStart;
            this.defaultProfile = defaultProfile;
         
            if (!Debug.isDebugBuild)
                this.clearProfileOnStart = false;
        }

        public void Clear()
        {
            data = defaultProfile != null ? defaultProfile.profileData : new GameData();

            Save();

            if (File.Exists(FilePath))
            {
                Load();
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("Profile not saved! Check file system!");
#endif
                data = new GameData();
            }
        }


        [ContextMenu("Save")]
        public void Save()
        {
            File.WriteAllText(FilePath, JsonUtility.ToJson(data, false));

            //TODO: To change the save progress in PlayerPrefs, and not on disk
            Debug.LogWarning("To change the save progress in PlayerPrefs, and not on disk");
        }

        private void Load()
        {
            if (File.Exists(FilePath))
            {
                data = JsonUtility.FromJson<GameData>(File.ReadAllText(FilePath));

                UpdateRuntimeByLoadedData();
            }
            else
            {
                Clear();
            }
        }

        private void SetDataDirty()
        {
            if (dataDirty == false)
            {
                dataDirty = true;
                Invoke("DefferSave", 1.0f);
            }
        }

        private void DefferSave()
        {
            Save();
            dataDirty = false;
        }

        private void UpdateRuntimeByLoadedData()
        {
        }
    }
}
