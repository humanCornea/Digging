using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{

    public GameObject BlcokPrefab;

    // Start is called before the first frame update
    void Start()
    {
        FieldType2();
    }

    private void FieldType1()
    {
        float BlockSize     = 0.5f;
        float DepthMargin   = 5f;       // フィールドの踊り場の広さ
        float FieldWidth    = GameObject.Find("Field").GetComponent<Renderer>().bounds.size.x;
        float FieldDepth    = GameObject.Find("Field").GetComponent<Renderer>().bounds.size.z;

        int hwNumBlock  = (int)(FieldWidth / BlockSize);
        int hdNumBlock  = (int)((FieldDepth - DepthMargin) / BlockSize);
        int NumBlock    = (hwNumBlock * hdNumBlock);
        
        for (int i = 0; i < NumBlock / 2 ; i++){
            GameObject go = Instantiate (BlcokPrefab) as GameObject;
            go.transform.position =
                new Vector3 (
                    -14.5f + BlockSize * (i % hwNumBlock) ,
                    0.25f,
                    0.5f + BlockSize * (i / hwNumBlock)
                );
            go.GetComponent<BlockController>().Point = 1f;
        }

        print("FiledDepth: " + FieldDepth);
        print("DepthMargin: " + DepthMargin);
        print("hdNumBlock" + hdNumBlock);
    }

    private void FieldType2()
    {
        float BlockSize     = 1f;
        float DepthMargin   = 5f;       // フィールドの踊り場の広さ
        float FieldWidth    = GameObject.Find("Field").GetComponent<Renderer>().bounds.size.x;
        float FieldDepth    = GameObject.Find("Field").GetComponent<Renderer>().bounds.size.z;

        int hwNumBlock  = (int)(FieldWidth / BlockSize);
        int hdNumBlock  = (int)((FieldDepth - DepthMargin) / BlockSize);
        int NumBlock    = (hwNumBlock * hdNumBlock);
        
        for (int i = 0; i < NumBlock / 2 ; i++){
            GameObject go = Instantiate (BlcokPrefab) as GameObject;
            go.transform.position =
                new Vector3 (
                    -14.5f + BlockSize * (i % hwNumBlock) ,
                    0.5f,
                    0.5f + BlockSize * (i / hwNumBlock)
                );
            go.GetComponent<BlockController>().Point = 1f;

            // Color color = new Color(Random.value, Random.value, Random.value, 1f);
            Color color = new Color(0.1f, 0.1f, 0.1f, 1f);
            go.GetComponent<Renderer>().material.SetColor("_Color", color);
        }

        // for (int i = 0; i < NumBlock / 2 ; i++){
        //     GameObject go = Instantiate (BlcokPrefab) as GameObject;
        //     go.transform.position =
        //         new Vector3 (
        //             -14.5f + BlockSize * (i % hwNumBlock) ,
        //             0.75f,
        //             0.5f + BlockSize * (i / hwNumBlock)
        //         );
        //     go.GetComponent<BlockController>().Point = 1f;
        // }

        print("FiledDepth: " + FieldDepth);
        print("DepthMargin: " + DepthMargin);
        print("hdNumBlock" + hdNumBlock);
    }

}
