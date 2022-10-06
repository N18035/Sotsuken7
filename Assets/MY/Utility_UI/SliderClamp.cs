using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;

interface IClamp
{
    public void SetMinMax(out float min,out float max);
    public void Change();
    IObservable<Unit> OnChangeClamp { get; }
}

public class SliderClamp : MonoBehaviour
{
    Slider s;

    [SerializeField]float Max;
    [SerializeField] float Min;
    [SerializeField] GameObject Parent;

    void Start()
    {
        // s = this.gameObject.GetComponent<Slider>();

        // //上限
        // s.onValueChanged.AsObservable()
        // .Subscribe(_ => {
        //     s.value = Mathf.Clamp(s.value, Min, Max);
        //     Parent.GetComponent<IClamp>().Change();
        // })
        // .AddTo(this);

        // Parent.GetComponent<IClamp>().OnChangeClamp
        // .Subscribe(_ => {
        //     Parent.GetComponent<IClamp>().SetMinMax(out var min, out var max);
        //     Max = max;
        //     Min = min;
        // })
        // .AddTo(this);

    }
}
