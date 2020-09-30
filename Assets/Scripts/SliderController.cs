using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public GameDirector GameDirector;
    public GameObject   HpSlider;
    public GameObject   MpSlider;

    private Slider _HpSlider;
    private Slider _MpSlider;

    private float _hpValue; // 0 - 1 の範囲で変化
    private float _hp;
    private float _maxhp;
    private float _mpValue; // 0 - 1 の範囲で変化
    private float _mp;
    private float _maxmp;

    void Start()
    {
        _HpSlider = HpSlider.GetComponent<Slider>();
        _MpSlider = MpSlider.GetComponent<Slider>();

        _HpSlider.value = 1;
        _MpSlider.value = 1;

        // Debug.Log("_Slider.name = " + _Slider.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHpValue()
    {
        _hp = GameDirector.HP;
        _maxhp = GameDirector.MaxHP;

        _hpValue = _hp / _maxhp;
        _HpSlider.value = _hpValue;

        // Debug.Log("GameDirector.MaxHP = " + GameDirector.MaxHP);
        // Debug.Log("_hpValue.value = " + _hpValue);
        // Debug.Log("_HpSlider.value = " + _HpSlider.value);
    }

    public void SetMpValue()
    {
        _mp = GameDirector.MP;
        _maxmp = GameDirector.MaxMP;

        _mpValue = _mp / _maxmp;
        _MpSlider.value = _mpValue;

        // Debug.Log("GameDirector.MaxMP = " + GameDirector.MaxMP);
        // Debug.Log("_mpValue.value = " + _mpValue);
        // Debug.Log("_MpSlider.value = " + _MpSlider.value);
    }
}
