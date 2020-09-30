using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*---   以下の制御
    ・プレイヤーの移動操作
    ・ブロックの破壊操作(Dig)
---*/

public class PlayerController : MonoBehaviour
{
    float Speed;
    float Accel = 5f;
    float dig = 1;
    float waittime_sec = 1f;
    float back = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)){
            Speed = Accel * 2 * dig * Time.deltaTime;
        } else {
            Speed = Accel * dig * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(-Speed, 0, 0);
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(Speed, 0, 0);
        }
        if(Input.GetKey(KeyCode.UpArrow)){
            transform.Translate(0, 0, Speed);
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            transform.Translate(0, 0, -Speed);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BlockPrefab(Clone)") {
            if (Input.GetKey(KeyCode.Space)) {
                StartCoroutine(ResetDig());
            }
        }

        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(back, 0, 0);
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(-back, 0, 0);
        }
        if(Input.GetKey(KeyCode.UpArrow)){
            transform.Translate(0, 0, -back);
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            transform.Translate(0, 0, back);
        }

        StartCoroutine(ResetDig());
    }

    IEnumerator ResetDig()
    {
        dig = 0f;
        yield return new WaitForSeconds(waittime_sec);
        dig = 1f;

    }


}
