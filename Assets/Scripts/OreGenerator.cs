using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using digging;

public class OreGenerator : MonoBehaviour
{
    public GameObject OrePrefab;
    public MasterDataManager MasterData;

    float FieldDepth;
    float FieldWidth;
    List<Vector3> SeedPositions = new List<Vector3>();

    void Start()
    {

        FieldWidth = GameObject.Find("Field").GetComponent<Renderer>().bounds.size.x;
        FieldDepth = GameObject.Find("Field").GetComponent<Renderer>().bounds.size.z;

        // Argorisum1();
        Argorisum2();

    }


    void CreateOre(string OreName, int NumOre, float sigma ,Vector3 Posi)
    {
        for (int i = 0; i < NumOre; i++) {
            GameObject cgo = Instantiate(OrePrefab) as GameObject;
            var TargetOre = new Ore();
            TargetOre = MasterData.Ores[OreName];

            cgo.transform.position = GetChildPosition(Posi, sigma);
            cgo.name = OreName;
            cgo.GetComponent<OreController>().Point = TargetOre.point;
            cgo.GetComponent<Renderer>().material.SetColor("_BaseColor", TargetOre.color );
        }
    }

    Vector3 GetPosition()
    {
        float min_X = - ( FieldWidth / 2 );
        float max_X =   ( FieldWidth / 2 );
        float min_Z = - ( 0f );
        float max_Z =  FieldDepth / 2 ;
        float Posi_X = Random.Range (min_X, max_X);
        float Posi_Y = Random.Range (min_Z, max_Z);

        return new Vector3 (Posi_X, 0.1f, Posi_Y);
    }

    Vector3　GetChildPosition (Vector3 Posi_Seed, float sigma)
    {
        Vector3 Child = new Vector3();
        float random_R1 = Random.value;     // 0~1の乱数
        float random_R2 = Random.value;     // 0~1の乱数
        // float sigma = 3;                    // 標準偏差
        float nd_X = sigma * Mathf.Sqrt(-2.0f * Mathf.Log(random_R1)) * Mathf.Cos(2.0f * Mathf.PI * random_R2);   //  正規分布(normal distribution)に従う乱数
        float nd_Z = sigma * Mathf.Sqrt(-2.0f * Mathf.Log(random_R1)) * Mathf.Sin(2.0f * Mathf.PI * random_R2);   //  正規分布(normal distribution)に従う乱数

        Child.x = Posi_Seed.x + nd_X;
        Child.z = Posi_Seed.z + nd_Z;
        Child.y = Posi_Seed.y;

        float min_X = - ( FieldWidth / 2 );
        float max_X =   ( FieldWidth / 2 );
        float min_Z =  0f;
        float max_Z =  FieldDepth / 2 ;

        Child.x = Mathf.Clamp(Child.x, min_X, max_X);
        Child.z = Mathf.Clamp(Child.z, min_Z, max_Z);

        return Child;
    }

    List<Vector3> GetSeedPositions(int NumSeeds)
    {
        List<Vector3> list = new List<Vector3>();
        for(int i = 0; i < NumSeeds; i++)
        {
            list.Add(GetPosition());
        }

        return list;
    }

    Vector3 GetCirclePosition(Vector3 Seed_Posi, float radius)
    {
        float angle = Random.Range(0,360);
        var Position = new Vector3 ();
        Position.x = Seed_Posi.x + Mathf.Sin(angle) * radius;
        Position.z = Seed_Posi.z + Mathf.Cos(angle) * radius;
        Position.y = Seed_Posi.y;

        return Position;
    }

    void Argorisum1()
    {
        int SeedNum = 3;
        SeedPositions = GetSeedPositions(SeedNum);

        foreach(Vector3 Posi in SeedPositions)
        {
            CreateOre("GreenOre", 30, 3f, Posi);
        }
        CreateOre("YellowOre", 15, 3f, GetPosition());
        CreateOre("RedOre"   ,  3, 3f, GetPosition());
    }

    void Argorisum2()
    {
        var Center = new Vector3();
        Center = GetPosition();
        float Radius = 10f;
        CreateOre("YellowOre", 10, 1f, Center);
        for (int i = 0; i < 15; i++){
            CreateOre("GreenOre", 3, 1f, GetCirclePosition(Center, Radius));
        }
    }
}
