// CharacterControllerによるPlayerの制御
//  Blockにぶつかった時にBlcokを破壊してポイントを得る。
//  Oreにぶつかった時にOreを破壊してポイントを得る。

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private float   _speed           = 10.0f;
    private float   _moveX           = 0f;
    private float   _moveZ           = 0f;

    private int     _waittime_sec    = 1;
    private bool    _HpStock         = true;
    private Vector3 _latestPos = new Vector3(0, 0, 0);  //前回のPosition
    private Vector3 _direction = new Vector3(0, 0, 0);


    GameDirector        GameDirector_scr;
    CharacterController controller;  // CaracterController型
    Animator    Animator;

    // Start is called before the first frame update
    void Start()
    {
        this.controller         = GetComponent<CharacterController>();
        this.GameDirector_scr   = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        Animator = transform.Find("Crocodile_1_1").GetComponent<Animator>();
        // this.transform.rotation =　Quaternion.identity;
        
    }

    // Update is called once per frame
    void Update()
    {
        _moveX = Input.GetAxis("Horizontal") * _speed;
        _moveZ = Input.GetAxis("Vertical") * _speed;
        _direction = new Vector3(_moveX, 0, _moveZ);

        controller.SimpleMove(_direction);
        // controller.Move(_direction * Time.deltaTime);

        Vector3 diff = this.transform.position - _latestPos;   //前回からどこに進んだかをベクトルで取得
        _latestPos = this.transform.position;  //前回のPositionの更新

        //ベクトルの大きさが0.02以上の時に向きを変える処理をする
        if (diff.magnitude > 0.02f)
        {
            transform.rotation = Quaternion.LookRotation(diff); //向きを変更する
            Animator.SetFloat("Speed", diff.magnitude);
        } else {
            Animator.SetFloat("Speed", 0f);
        }
    }

    public void StopDigging()
    {
        // Debug.Log("Call StopDigging");
        _HpStock = false;
    }

    IEnumerator DestroyObj(GameObject Target)
    {
        yield return new WaitForSeconds(_waittime_sec);
        Destroy(Target);
    }

    /*--- Blockにぶつかった時の動作 （Space Key押してる時のみ） ---*/
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if( _HpStock == true ) {
            if (Input.GetKey(KeyCode.Space)){
                if (hit.gameObject.name == "BlockPrefab(Clone)")
                {
                    GameObject      Block           = hit.gameObject;
                    BlockController BlockController = Block.GetComponent<BlockController>();
                    Block.GetComponent<Rigidbody>().isKinematic = false;
                    Block.GetComponent<Collider>().isTrigger    = true;
                    this.GameDirector_scr.HP    +=  (  -BlockController.Point   );
                    this.GameDirector_scr.Score +=      BlockController.TakePoint();

                    StartCoroutine(DestroyObj(Block));
                }
            }
        }
    }

    /*--- Oreにぶつかった時の動作 ---*/
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Ore")
        {
            GameObject  Ore             = other.gameObject;
            Rigidbody   Rigidbody_Ore   = Ore.GetComponent<Rigidbody>();
            Rigidbody_Ore.isKinematic       = false;
            Rigidbody_Ore.angularVelocity   = new Vector3 (0, 60f, 0);
            this.GameDirector_scr.NumOre += (int) Ore.GetComponent<OreController>().TakePoint();

            StartCoroutine(DestroyObj(Ore));
        }
    }
}
