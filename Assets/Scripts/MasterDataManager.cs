using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using digging;

[CreateAssetMenu(fileName = "MasterDataManager", menuName = "ScriptableObject/MasterDataManager", order = 0)]
public class MasterDataManager : ScriptableObject {

    public Dictionary<string, Bomb>     Bombs       = new Dictionary<string, Bomb>();
    public Dictionary<string, Sonar>    Sonars      = new Dictionary<string, Sonar>(); 
    public Dictionary<string, Ore>      Ores        = new Dictionary<string, Ore>(); 
    public Dictionary<int   , Lv>       Lvs         = new Dictionary<int   , Lv>();

    private class List_Pack {
        public List<Sonar>  SonarList   = new List<Sonar>();
        public List<Bomb>   BombList    = new List<Bomb>();
        public List<Ore>    OreList     = new List<Ore>(); 
        public List<Lv>     LvList      = new List<Lv>(); 
    }

    // private void Awake() { Debug.Log("Call Awake"); }

    private void OnEnable() {
        // Debug.Log("Call OnEnable");
        LoadMasterData();
    }

    /*--- MasterDataの保存場所を返す ---*/
    string GetPath_MasterData(string name)    // Unity上とその他のプラットホームで保存場所を変更
    {
        string filePath = "Resouce/MasterData/" + name; // ファイル名

#if UNITY_EDITOR
        filePath += ".json";
#else
        filePath = Application.persistentDataPath + "/" + filePath;
#endif
        return filePath;
    }


    /*--- Jsonファイルを読み込んでstring型で返す ---*/
    public string GetJson(string name)
    {
        string LoadData;
        LoadData = File.ReadAllText(GetPath_MasterData(name));
        return LoadData;
    }

    /*--- 指定されたファイル名のマスターデータが存在するか判定する ---*/
    public bool CheckList(string name)
    {
        Debug.Log("Load MasteData/" + name);
        if (File.Exists(GetPath_MasterData(name))) {
            //Debug.Log("True");
            return true;
        } else {
            Debug.LogWarning("No MasterData/" + name);
            return false;
        }
    }

    public void LoadMasterData()
    {
       LoadSonarList();
       LoadBombList();
       LoadOreList();
       LoadLvList();
    // Debug.Log("MasterData Sonars.Count  = " + Sonars.Count);
    // Debug.Log("MasterData Bombs.Count   = " + Bombs.Count);
    // Debug.Log("MasterData Ores.Count    = " + Ores.Count);
    // Debug.Log("MasterData Lvs.Count     = " + Lvs.Count);
    }

    public void LoadSonarList()
    {
        var List_Pack = new List_Pack();
        string FileName = "Sonars";

        if(CheckList(FileName)){
            List_Pack = JsonUtility.FromJson<List_Pack>(GetJson(FileName));
            
            List<Sonar> list = List_Pack.SonarList;
            
            // Debug.Log("list.Count " + list.Count);
            int count = list.Count;
            Enumerable.Range(0, count).ToList().ForEach(i => Sonars.Add (list[i].name, list[i]));
        }
    }

    public void LoadBombList()
    {
        var List_Pack = new List_Pack();
        string FileName = "Bombs";

        if(CheckList(FileName)){
            List_Pack = JsonUtility.FromJson<List_Pack>(GetJson(FileName));
            
            List<Bomb> list = List_Pack.BombList;
            
            // Debug.Log("list.Count " + list.Count);
            int count = list.Count;
            Enumerable.Range(0, count).ToList().ForEach(i => Bombs.Add (list[i].name, list[i]));
        }
    }

    public void LoadOreList()
    {
        var List_Pack = new List_Pack();
        string FileName = "Ore";

        if(CheckList(FileName)){
            List_Pack = JsonUtility.FromJson<List_Pack>(GetJson(FileName));
            
            List<Ore> list = List_Pack.OreList;
            
            // Debug.Log("list.Count " + list.Count);
            int count = list.Count;
            Enumerable.Range(0, count).ToList().ForEach(i => Ores.Add (list[i].name, list[i]));
        }
    }

    public void LoadLvList()
    {
        var List_Pack = new List_Pack();
        string FileName = "Lv";

        if(CheckList(FileName)){
            List_Pack = JsonUtility.FromJson<List_Pack>(GetJson(FileName));
            
            List<Lv> list = List_Pack.LvList;
            
            // Debug.Log("list.Count " + list.Count);
            int count = list.Count;
            Enumerable.Range(0, count).ToList().ForEach(i => Lvs.Add (list[i].lv, list[i]));
        }
    }
}

namespace digging {
    [System.Serializable]
    public class Bomb {
        public int      id;
        public string   name;
        public float    range;
        public float    cost;
        public int      count;
    }

    [System.Serializable]
    public class Sonar {
        public int      id;
        public string   name;
        public string   target;
        public float    range;
        public float    mpusage;
    }

    [System.Serializable]
    public class Lv {
        public int      lv;
        public float    HP;
        public float    MP;
    }

    [System.Serializable]
    public class Ore {
        public int      id;
        public string   name;
        public float    point;
        public Color    color;
    }
}