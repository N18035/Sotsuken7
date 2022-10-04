using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Ken.Delay
{
    public class DelaySlider : MonoBehaviour
    {
        [SerializeField] DelayModel delay;
        public int ID;
        void Start()
        {
            this.gameObject.GetComponent<Slider>().onValueChanged.AsObservable()
            .Throttle(TimeSpan.FromMilliseconds(100))
            .Subscribe(t => {
                // delay.DelaySetup(ID,this.gameObject.GetComponent<Slider>().value);
                // delay.DelaySetup(ID,this.gameObject.GetComponent<Slider>().value);
            })
            .AddTo(this);
        }
    }
}

