using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StatusScript : MonoBehaviour
{
    public SaveDataManager  SaveData;
    public GameObject       StatusField;
    public AccountingManager AccountingManager;

    private int _AmountOfOrder;
    private int _Ore_stock;
    // Start is called before the first frame update
    void Start()
    {
        SetValueOnDisplay();
    }


    public void SetValueOnDisplay()
    {
        _AmountOfOrder  = AccountingManager.AmountOfOrder;
        _Ore_stock      = SaveData.savedata.numberOfOre;

        foreach (string TargetName in Enum.GetNames(typeof(StatusElement)))
        {
            Text TargetText = StatusField.transform.Find(TargetName).GetComponent<Text>();
            
            switch(TargetName)
            {
                case "Ore_stock"    :   TargetText.text =         _Ore_stock                  .ToString();    break;
                case "Ore_total"    :   TargetText.text = "- "               + _AmountOfOrder .ToString();    break;
                case "Ore_result"   :   TargetText.text =       ( _Ore_stock - _AmountOfOrder).ToString();    break;
                case "lv"           :   TargetText.text = "Missing";                                          break;
                case "hp"           :   TargetText.text = SaveData.savedata.MaxHP.ToString();                 break;
                case "mp"           :   TargetText.text = SaveData.savedata.MaxMP.ToString();                 break;
            }
        }
    }

    private enum StatusElement
    {
        Ore,
        Ore_stock,
        Ore_total,
        Ore_result,
        Status,
        Lv,
        HP,
        MP,
        lv,
        hp,
        mp
    }
}

