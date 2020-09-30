using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    private Vector3 offset;
    public float radius, theta, phi,speed;
    public int InpHori, InpVert;
    
    void Start()
    {
        offset = this.GetComponent<Transform>().position;
        radius = 20f; //半径長
        theta = -180f; //方位角
        phi = 35f; //仰角
        speed = 100f * Time.deltaTime;
    }

    void Update()
    {
        theta = -180f;
        radius = 20f; //半径長

        this.CameraRotate();
        // theta += InpHori * speed;
        // phi += InpVert * speed;
        // theta += - Input.GetAxis("Horizontal") * speed;
        // phi += - Input.GetAxis("Vertical") * speed;

        theta %= 360f;
        phi %= 360f;

        //球面座標からデカルト座標への変換
        this.transform.position = new Vector3(
            radius * Mathf.Cos(phi *Mathf.Deg2Rad) * Mathf.Sin(theta * Mathf.Deg2Rad),
            radius * Mathf.Sin(phi * Mathf.Deg2Rad),
            radius * Mathf.Cos(phi * Mathf.Deg2Rad) * Mathf.Cos(theta * Mathf.Deg2Rad)
        ) + Target.position;

        //変換した値をカメラ位置情報に
        this.transform.eulerAngles = new Vector3(phi, theta - 180 ,0);
    }

    void CameraRotate()
    {
        if(Input.GetKey(KeyCode.A)){
            theta = -150;
        }
        if(Input.GetKey(KeyCode.D))
        {
            theta = -210;
        }
        if(Input.GetKey(KeyCode.S))
        {
            radius = 40;
        }
        
    }
}