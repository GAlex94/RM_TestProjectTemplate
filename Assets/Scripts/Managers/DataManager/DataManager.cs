using System.IO;
using UnityEngine;

namespace testProjectTemplate
{
    public class DataManager : Singleton<DataManager>
    {
        private string profileName;
        private bool clearProfileOnStart;
        private bool dataDirty = false;
        GameData data = new GameData();
        private DefaultProfile defaultProfile;

        private string FilePath => Path.Combine(Application.persistentDataPath, profileName + ".json");
        
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
            if (PlayerPrefs.HasKey("SaveBattle"))
            {
                PlayerPrefs.DeleteKey("SaveBattle");
            }
        }


        [ContextMenu("Save")]
        private void SaveData()
        {
            if (!BattleGame.IsAwake)
            {
                return;
            }

            data.BattleData.teamOne.Clear();
            data.BattleData.teamTwo.Clear();


            data.BattleData.settingGame = GameManager.Instance.MainGameSetting;

            foreach (var unit in BattleGame.Instance.UnitController.TeamOne.units)
            {
               // data.BattleData.teamOne.Add(new SaveUnitInfo(unit.transform.position, unit.UnitType, unit.UnitState, unit.Target,unit.Speed, unit.Size));
            }

            foreach (var unit in BattleGame.Instance.UnitController.TeamTwo.units)
            {
               // data.BattleData.teamTwo.Add(new SaveUnitInfo(unit.transform.position, unit.UnitType, unit.UnitState, unit.Target, unit.Speed, unit.Size));
            }
            
            PlayerPrefs.SetString("SaveBattle", JsonUtility.ToJson(data, false));

            File.WriteAllText(FilePath, JsonUtility.ToJson(data, false));

            //TODO: To change the save progress in PlayerPrefs, and not on disk
            Debug.LogWarning("To change the save progress in PlayerPrefs, and not on disk");
        }

        public bool IsCanLoadData()
        {
            return PlayerPrefs.HasKey("SaveBattle");
        }

        private void LoadData()
        {

            if (!PlayerPrefs.HasKey("SaveBattle"))
            {
                return;
            }
            data = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString("SaveBattle"));

            if (File.Exists(FilePath))
            {
                data = JsonUtility.FromJson<GameData>(File.ReadAllText(FilePath));
            }
        }

        public void Save(bool isSaveDirty = true)
        {
            if (isSaveDirty)
            {
                SetDataDirty();
            }
            else
            {
                SaveData();
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
            SaveData();
            dataDirty = false;
        }
    }
}
