using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using digging;

[CreateAssetMenu(fileName = "SaveDataManager", menuName = "ScriptableObject/SaveDataManager", order = 0)]
public class SaveDataManager : ScriptableObject {
    public float testHP;
    public SaveData savedata = new SaveData();
    public MasterDataManager    MasterData;

    [System.Serializable]
    public class SaveData
    {
        public float MaxHP;
        public float MaxMP;
        public int numberOfOre;
        public Bomb MyBomb;
        public List<Bomb> BombList = new List<Bomb>();
        public Sonar MySonar;
        public List<Sonar> SonarList = new List<Sonar>();
    }

    public float GetMaxHp() {
        return savedata.MaxHP;
    }

    public void SetHP(float setHP)
    {
        savedata.MaxHP = setHP;
    }

///Save機能関連/////////////////////////
    /*--- SaveDataの保存場所を返す ---*/
    string GetPath()    // Unity上とその他のプラットホームで保存場所を変更
    {
        string filePath = "Resouce/SaveData"; // ファイル名

#if UNITY_EDITOR
        filePath += ".json";
#else
        filePath = Application.persistentDataPath + "/" + filePath;
#endif
        return filePath;
    }

    /*--- savedataをjsonに変換してファイルに保存する ---*/
    public void Save()
    {
        Debug.Log("Save");
//        SetInitialSonar();
        string json = JsonUtility.ToJson(savedata);
        File.WriteAllText(GetPath(), json);
    }


    /*--- Jsonファイルを読み込んでstring型で返す ---*/
    public string GetJson()
    {
        string LoadData;
        LoadData = File.ReadAllText(GetPath());
        return LoadData;
    }

    /*--- json形式の文字列をSaveDataクラスに変換する ---*/
    public void Load()
    {
        Debug.Log("Load");
        if (File.Exists(GetPath())) {
            this.savedata = JsonUtility.FromJson<SaveData>(GetJson());
        } else {
            Debug.Log("Created New SaveData");
            this.savedata = SetInitialValue();
            // SetInitialSonar();
            // SetInitialBomb();
            // this.savedata.MySonar = this.savedata.SonarList[0];
            // this.savedata.MyBomb = this.savedata.BombList[0];
        }
    }

    public Bomb PickBombInSavedataList(string Name)
    {
        Bomb TargetBomb = new Bomb();
        foreach(Bomb tempBomb in savedata.BombList)
        {
            if(tempBomb.name == Name){ TargetBomb = tempBomb; break; }
        }
        return TargetBomb;
    }

    public Sonar PickSonarInSavedataList(string Name)
    {
        Sonar TargetSonar = new Sonar();
        foreach(Sonar tempSonar in savedata.SonarList)
        {
            if(tempSonar.name == Name){ TargetSonar = tempSonar; break; }
        }
        return TargetSonar;
    }

//////////////////////////

    private Sonar SetInitialSonar()
    {
        Debug.Log("SetIntialSonar");        // SonarDictionary = new Dictionary<string, Sonar>();

        var initsonar = new Sonar();
        initsonar = MasterData.Sonars["GreenSonar"];

        // Debug.Log("savedata.Sonarlist[0].name = " + savedata.SonarList[0].name);        
        return initsonar;
    }

    private Bomb SetInitialBomb()
    {
        Debug.Log("SetInitialBomb");

        var initbomb = new Bomb();
        initbomb = MasterData.Bombs["NormalBomb"];
        initbomb.count = 10;

        // savedata.BombList.Add(initbomb);
        // Debug.Log("savedata.Bomblist[0].name = " + savedata.BombList[0].name);
        return initbomb;
    }

    private SaveData SetInitialValue() {
        Debug.Log("SetInitialValue");
        SaveData initialvalue = new SaveData();
        initialvalue.MaxHP = 2000f;
        initialvalue.MaxMP = 300f;
        initialvalue.SonarList.Add(SetInitialSonar());  initialvalue.MySonar    = initialvalue.SonarList[0];
        initialvalue.SonarList.Add(MasterData.Sonars["YellowSonar"]);
        initialvalue.BombList.Add(SetInitialBomb());    initialvalue.MyBomb     = initialvalue.BombList[0];
        initialvalue.BombList.Add(MasterData.Bombs["BigBomb"]);
        
        return initialvalue;
    }

}