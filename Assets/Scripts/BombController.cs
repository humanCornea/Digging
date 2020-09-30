using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using digging;

public class BombController : MonoBehaviour
{
    SphereCollider  BombCollider;
    GameDirector    GameDirector_scr;
    [System.NonSerialized]public Bomb ThisBomb; // Bomb生成時にBombGeneratorから渡される値

    GameObject Plane;
    private int waittime = 1;
    // Start is called before the first frame update
    void Start()
    {
        BombCollider        = this.gameObject.GetComponent<SphereCollider>();
        GameDirector_scr    = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        Plane               = transform.Find("Plane").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
                BombCollider.radius = ThisBomb.range;
                Destroy(Plane);
        }
    }

    void OnTriggerEnter(Collider other) {
        /*--- Blockに当たった時 ---*/
        if (other.gameObject.tag == "Block")
        {
            GameObject Block = other.gameObject;
            GameDirector_scr.Score += Block.GetComponent<BlockController>().TakePoint();
            Destroy(Block);
        }
        /*--- Oreに当たった時 ---*/
        else if (other.gameObject.tag == "Ore")
        {
            GameObject Ore = other.gameObject;
            Rigidbody Rigidbody_Ore = Ore.GetComponent<Rigidbody>();
            Rigidbody_Ore.isKinematic = false;
            Rigidbody_Ore.angularVelocity = new Vector3 (0, 60f, 0);
            GameDirector_scr.NumOre += (int)Ore.GetComponent<OreController>().TakePoint();
            StartCoroutine(DestroyObj(Ore));
        }
    }

    IEnumerator DestroyObj(GameObject Target) {
        yield return new WaitForSeconds(waittime);
        Destroy(Target);
    }


}
