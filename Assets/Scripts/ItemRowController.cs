using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRowController : MonoBehaviour
{
    private AccountingManager AccountingManager;
    public int _indexNum;


    void Start()
    {
        AccountingManager = GameObject.Find("AccountingManager").GetComponent<AccountingManager>();
    }

    void Update()
    {
        
    }

    public void PlusCount()
    {
        int point  = 10;
        AccountingManager.GetCount(_indexNum,point);

        Debug.Log("PlusCount list[" + _indexNum + "] += " + point);
    }

    public void MinusCount()
    {
        int point  = -10;
        AccountingManager.GetCount(_indexNum,point);

        Debug.Log("PlusCount list[" + _indexNum + "] += " + point);
    }


}
