using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using digging;

/*---
    Sonarは設置と同時に使用し処理が終了する。SonarPrefabはオブジェクトだけ残して機能はもたない。
    SelectedSonarを設置する。
    SelectedSonarはGameDirectorから与えられる。

    SonarのColliderは起動時に拡大し、SonarのColliderに接触したOreを可視化する。
---*/

public class SonarManager : MonoBehaviour
{
    public GameObject       SonarPrefab;
    public GameObject       Player;
    public GameDirector     GameDirector;
    [System.NonSerialized]public Sonar SelectedSonar = new Sonar();

    private GameObject _NewSonar;
    private float _mp;
    private float _mp_usage;
    private float _mp_delta;
    // Update is called once per frame
    private void Update()
    {
        /*--- Set and Run Sonar---*/
        if(Input.GetKey(KeyCode.RightShift) == false)
        {
            if (Input.GetKeyDown(KeyCode.F))
            { 
                _mp         = GameDirector.MP       ;
                _mp_usage   = SelectedSonar.mpusage ;

                if (_mp - _mp_usage >= 0) {
                    _NewSonar = CreateSonar();
                }

            }
            
            if (Input.GetKey( KeyCode.F ))
            {
                Vector3 pos = Player.transform.position;
                pos.y = 1f;
                _NewSonar.transform.position = pos;
            } else {
                _NewSonar = null;
            }

            if (Input.GetKeyUp( KeyCode.F))
            {
                RunSonar();
            }
        }
    }

    private GameObject CreateSonar() {  
        GameObject go = Instantiate (SonarPrefab) as GameObject;

        Vector3 pos = Player.transform.position;
        pos.y = 1f;
        go.transform.position = pos;

        return go;
    }

    private void RunSonar()
    {
        SphereCollider sc = this.gameObject.GetComponent<SphereCollider>();
        // Debug.Log("SphereCollider.name = " + sc.name);
        this.transform.position = Player.transform.position;
        sc.radius = SelectedSonar.range;
        GameDirector.MP -= SelectedSonar.mpusage;

        // Debug.Log("Set " + SelectedSonar.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        /*--- ソナー範囲内のOreを可視化する。 ---*/
        if (other.gameObject.tag == "Ore")
        {
            if (other.gameObject.name == SelectedSonar.target)
            {
                GameObject Ore = other.gameObject;
                Ore.GetComponent<Renderer>().material.renderQueue = (int) (RenderQueue.Transparent + 1);
            }
        }
    }
}
