using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using digging;
using UnityEngine.SceneManagement;

public class AccountingManager : MonoBehaviour
{
    public  GameObject itemrow_prefab;
    public  SaveDataManager SaveData;
    public  StatusScript StatusScript;
    /*--- Savedata.savedata.BombListのインデックス番号を利用してその他のリストを管理する。 ---*/
    private int _AmountOfOrder = 0;
    public  int  AmountOfOrder { get {return _AmountOfOrder;}}
    private List<PurchaseList> _PurchaseList = new List<PurchaseList>();
    // private List<int> _PurchaseList = new List<int>();
    // private List<int> _OrderList = new List<int>();
    private List<GameObject> GOList = new List<GameObject>();

    void Start()
    {
        SaveData.Load();
        Set_PurchaseList();
        CreateItemRow();
    }



    private void Set_PurchaseList()
    {
        List<Bomb> BombList_temp = SaveData.savedata.BombList;

        foreach(Bomb Item in BombList_temp)
        {
            var temp = new PurchaseList();
            _PurchaseList.Add(temp);
        } 
    }

    private void CreateItemRow()
    {
        Vector3 initPosi = new Vector3(0f,25f,0f);   // 列の表示場所
        List<Bomb> TargetList = SaveData.savedata.BombList;

        foreach(Bomb TargetBomb in TargetList)
        {
            GameObject go = Instantiate(itemrow_prefab) as GameObject;
            int IndexNum = TargetList.IndexOf(TargetBomb);
            Vector3 pos = new Vector3(
                initPosi.x,
                initPosi.y + IndexNum * (-30),
                initPosi.z
            );
            GOList.Add(go);
            SetPositionItemRow(go,pos);
            SetInitialCount(go, TargetBomb, IndexNum);
            SetIndexNum(go, IndexNum);
        }
    }

    private void SetPositionItemRow(GameObject go,Vector3 pos) {
        go.transform.SetParent(UnityEngine.Object.FindObjectOfType<Canvas>().transform);    // Canvasの下にオブジェクトを移動
        RectTransform rectTransform = go.GetComponent<RectTransform>();

        rectTransform.localPosition = pos;
    }

    private void SetInitialCount(GameObject go, Bomb TargetBomb, int IndexNum)
    {

        int TargetQty = _PurchaseList[IndexNum].qty;
        _PurchaseList[IndexNum].amount = (int)TargetBomb.cost * TargetQty;
        CalcAmountOfOrder();

        foreach (string ColumnName in Enum.GetNames(typeof(ItemRowElement)))
        {
            Text TargetText = go.transform.Find(ColumnName).GetComponent<Text>();

            switch(ColumnName)
            {
                case "Name"     :   TargetText.text = TargetBomb.name;                              break;
                case "Qty"      :   TargetText.text = TargetQty.ToString();                         break;
                case "Unit"     :   TargetText.text = TargetBomb.cost.ToString();                   break;
                case "Amount"   :   TargetText.text = (TargetBomb.cost * TargetQty).ToString();     break;
                case "Additions":   TargetText.text = TargetQty.ToString();                         break;
                case "Stock"    :   TargetText.text = TargetBomb.count.ToString();                  break;
                case "Total"    :   TargetText.text = (TargetQty + TargetBomb.count).ToString();    break;
            }
            
        }
        
    }
    
    private void SetIndexNum(GameObject go, int IndexNum)
    {
        go.GetComponent<ItemRowController>()._indexNum = IndexNum;
    }

    public void GetCount(int IndexNum, int point) {
        _PurchaseList[IndexNum].qty　+= point;
        if(_PurchaseList[IndexNum].qty < 0)　{　_PurchaseList[IndexNum].qty = 0;　}

        Debug.Log("_PurchaseList[" + IndexNum + ".qty] = " + _PurchaseList[IndexNum].qty);

        UpDateDisplay(GOList[IndexNum], IndexNum);
    }
    
    private void UpDateDisplay(GameObject go, int IndexNum){
        Debug.Log("UpDateDisplay");
        SetInitialCount(go, SaveData.savedata.BombList[IndexNum] ,IndexNum);
        StatusScript.SetValueOnDisplay();

    }

    private void CalcAmountOfOrder()
    {
        int temp = 0;
        foreach(PurchaseList EachBomb in _PurchaseList)
        {
            temp += EachBomb.amount;
        }

        _AmountOfOrder = temp;
    }

    public void ConfirmPurchase()
    {
        int deltaOre = SaveData.savedata.numberOfOre - _AmountOfOrder;
        if(deltaOre >= 0){
            SaveData.savedata.numberOfOre = deltaOre;
            _AmountOfOrder = 0;

            foreach(GameObject go in GOList)
            {
                int IndexNum = GOList.IndexOf(go);

                SaveData.savedata.BombList[IndexNum].count +=  _PurchaseList[IndexNum].qty;
                _PurchaseList[IndexNum].qty = 0;
                
                UpDateDisplay(go, IndexNum);
            }
        } else {
            Debug.Log("Oreが足りません！");
        }
    }

    public void ResetPurchaseList()
    {
        foreach(GameObject go in GOList)
        {
            int IndexNum = GOList.IndexOf(go);

            _PurchaseList[IndexNum].qty = 0;
            _PurchaseList[IndexNum].amount = 0;
        }
    }

    // public void ReturnMenu()
    // {
    //     SaveData.Save();
    //     SceneManager.LoadScene("GameScene");
    // }

    private class PurchaseList
    {
        public int qty;
        public int amount;

        public PurchaseList(            ) {qty = 0; amount = 0;}
        public PurchaseList(int x, int y) {qty = x; amount = y;}
    }

    private enum　ItemRowElement
    {
        Name,
        Qty,
        Unit,
        Amount,
        Additions,
        Stock,
        Total
    }

}
