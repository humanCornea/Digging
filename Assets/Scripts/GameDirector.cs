using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using digging;

public class GameDirector : MonoBehaviour
{
    public MasterDataManager    MasterData;
    public SaveDataManager      SaveData;
    public SonarManager         SonarManager;
    public BombGenerator        BombGenerator;
    public Image                StatusUI;
    public GameObject           PropertyUI;   // 非アクティブだとFind()で探せないのでアタッチする
    public PlayerController2    PlayerController;
    public SliderController     SliderController;

    private float   _Score   = 0f;
    private int     _numOre  = 0;
    private float   _time    = 0f;
    private float   _HP      = 100f;
    private float   _mp      = 100f;
    private float   _MaxHP;
    private float   _maxmp;
    private Text    _scoreText,
                    _timerText,
                    _HPText,
                    _MPText,
                    _OreText,
                    _BombText,
                    _SonarText;

    private Bomb    _SelectedBomb;
    private Sonar   _SelectedSonar;

    void Start()
    {
        InitializeGame();
        InitializeUI();
        
        if (Time.timeScale != 1f)    { Time.timeScale = 1f;   } 
    }

    void Update()
    {
        /*--- 時間の表示処理 ---*/
        _time           += Time.deltaTime;
        _timerText.text = "Time : " + _time.ToString("F1");
        _BombText.text  = _SelectedBomb.count.ToString()    + " "                   + _SelectedBomb.name.ToString();



        /*--- Pauseしながらメニュー画面を開く ---*/
        if (Input.GetKeyDown(KeyCode.R)) {　Pause();　OpenMenu(); }

        /*--- HPが0になったら掘れなくなる ---*/
        if (_HP <= 0) {
            _HP             = 0;
            PlayerController.StopDigging();
            UpdateUI();
        }

        /*--- 操作設定の一部 ---*/
        if (Input.GetKey(KeyCode.RightShift)) 
        {　
            if(Input.GetKeyDown(　KeyCode.Q )){　ChangeSelectedBomb();　 }
            if(Input.GetKeyDown(　KeyCode.F )){　ChangeSelectedSonar();　}
        }
    }

/*--- アクセサ ---*/
    public float Score
    {
        set { _Score  = value;  UpdateUI(); }
        get { return _Score; }
    }

    public int NumOre
    {
        set { _numOre = value;  UpdateUI(); } 
        get { return _numOre; }
    }

    public float HP
    {
        set { _HP = value;      UpdateUI();     SliderController.SetHpValue(); }
        get { return _HP; }
    }

    public float MP
    {
        set {_mp = value;       UpdateUI();     SliderController.SetMpValue();　}
        get { return _mp; }
    }

    public float MaxHP { get{ return _MaxHP; } }
    public float MaxMP { get{ return _maxmp; } }


/*--- Pause機能関連 ---*/
    private void Pause()
    {
        if (Time.timeScale != 0)    { Time.timeScale = 0;   } 
        else                        { Time.timeScale = 1f;  }
    }

    private void OpenMenu()
    {
        if (PropertyUI.activeSelf == false) { PropertyUI.SetActive (true);  }
        else                                { PropertyUI.SetActive (false); }
    }

    private void CancelPause()
    {
        Time.timeScale = 1f;
        PropertyUI.SetActive (false);
    }

    private void FinishGame()
    {
        SaveData.savedata.numberOfOre += _numOre;
        SaveData.Save();
        SceneManager.LoadScene ("ResultsScene");
    }

/*--- 初期化関連 ---*/

    private void InitializeGame()
    {
        SaveData.Load();

        _SelectedBomb = SaveData.PickBombInSavedataList(SaveData.savedata.MyBomb.name);
            BombGenerator.SelectedBomb = _SelectedBomb;
        _SelectedSonar = SaveData.PickSonarInSavedataList(SaveData.savedata.MySonar.name);
            SonarManager.SelectedSonar = _SelectedSonar;

        // _numOre = SaveData.savedata.numberOfOre;
        _numOre = 0;
        _MaxHP   = SaveData.savedata.MaxHP;
        _HP      = _MaxHP;
        _maxmp  = SaveData.savedata.MaxMP;
        _mp     = _maxmp;
    }

    private void InitializeUI()
    {
        Transform statusUI = StatusUI.transform;

        _timerText  = statusUI.Find(  "Time"    ).GetComponent<Text>();
        _scoreText  = statusUI.Find(  "Score"   ).GetComponent<Text>();
        _HPText     = statusUI.Find(  "HP"      ).GetComponent<Text>();
        _MPText     = statusUI.Find(  "MP"      ).GetComponent<Text>();
        _OreText    = statusUI.Find(  "Ore"     ).GetComponent<Text>();
        _BombText   = statusUI.Find(  "Bomb"    ).GetComponent<Text>();
        _SonarText  = statusUI.Find(  "Sonar"   ).GetComponent<Text>();

        UpdateUI();
    }

/*--- その他の機能 ---*/
    private void UpdateUI()
    {
        _timerText.text = "Time : "                         + _time.ToString("F1");
        _scoreText.text = "Score : "                        + _Score.ToString();
        _OreText.text   = "Ore : "                          + _numOre.ToString();
        _HPText.text    = "HP : "                           + _HP.ToString()        + "/" + _MaxHP.ToString(); 
        _MPText.text    = "MP : "                           + _mp.ToString()        + "/" + _maxmp.ToString();
        _BombText.text  = _SelectedBomb.count.ToString()    + " "                   + _SelectedBomb.name.ToString();
        _SonarText.text = _SelectedSonar.name.ToString();      
    }

    private void ChangeSelectedBomb()
    {
        Bomb TargetBomb = new Bomb();
        List<Bomb> TargetList = SaveData.savedata.BombList;
        int IndexNum = TargetList.IndexOf(_SelectedBomb);
            IndexNum++;
        int ListCount = TargetList.Count;

        if(IndexNum > ListCount - 1)　{　IndexNum = IndexNum - ListCount;　}

        TargetBomb = TargetList[IndexNum];
        _SelectedBomb = TargetBomb;
        BombGenerator.SelectedBomb = _SelectedBomb;

        UpdateUI();
    }

    private void ChangeSelectedSonar()
    {
        Sonar TargetSonar = new Sonar();
        List<Sonar> TargetList = SaveData.savedata.SonarList;
        int IndexNum = TargetList.IndexOf(_SelectedSonar);
            IndexNum++;
        int ListCount = TargetList.Count;

        if(IndexNum > ListCount - 1){　IndexNum = IndexNum - ListCount;　}

        TargetSonar = TargetList[IndexNum];
        _SelectedSonar = TargetSonar;
        SonarManager.SelectedSonar = _SelectedSonar;

        UpdateUI();
    }
}