using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using digging;

public class BombGenerator : MonoBehaviour
{
    public GameObject   BombPrefab;
    public GameObject   Player;
    [System.NonSerialized]public Bomb SelectedBomb = new Bomb();    // 開始時にGameDirectorから渡される値

    private GameObject _NewBomb;
    private int _BombCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightShift)== false){
            if (SelectedBomb.count > 0)
            {
                if (Input.GetKeyDown(KeyCode.Q)) {
                    _NewBomb = BombCreate();
                    SelectedBomb.count --;
                }
            }

            if (Input .GetKey(KeyCode.Q)){
                if (_NewBomb != null )
                {
                    Vector3 pos = Player.transform.position;
                    pos.y = _NewBomb.transform.position.y;

                    _NewBomb.transform.position = pos;
                }
            } else {
                _NewBomb = null;
            }
        }
    }

    private GameObject BombCreate()
    {
        GameObject go = Instantiate(BombPrefab) as GameObject;
        go.transform.position = Player.transform.position;
        go.GetComponent<BombController>().ThisBomb = SelectedBomb;


        /*--- zファイティング回避のために生成するBombのy軸を少しずつずらす処理 ---*/
        var pos = go.transform.position;
        pos.y += 0.0001f * _BombCount; 
        go.transform.position = pos;

        Transform go2 = go.transform.Find("Plane");
        go2.GetComponent<Renderer>().material.renderQueue = (int) (RenderQueue.Transparent + 2);
        go2.GetComponent<Renderer>().material.SetFloat("_radius", SelectedBomb.range);

        _BombCount += 1;

        return go;
    }

}