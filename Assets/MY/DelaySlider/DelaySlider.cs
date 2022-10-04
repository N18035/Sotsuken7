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

        public bool End=false;
        [SerializeField] AudioSource audioSource;

        Slider thisSlider;
        
        public void Ready(){
            thisSlider.maxValue = audioSource.clip.length;
            thisSlider.value = 0;
            End = false;
        }

        void Start()
        {
            thisSlider = this.gameObject.GetComponent<Slider>();
            
            thisSlider.onValueChanged.AsObservable()
            .Throttle(TimeSpan.FromMilliseconds(100))
            .Subscribe(t => {
                delay.TestPublicDelay();
                // delay.DelaySetup(ID,this.gameObject.GetComponent<Slider>().value);
                // delay.DelaySetup(ID,this.gameObject.GetComponent<Slider>().value);
                delay.SetDelay(thisSlider.value);
            })
            .AddTo(this);
        }

        void Update(){
            if(!audioSource.isPlaying) return;

            if(End) return;

            if(audioSource.time >= thisSlider.value){
            Debug.Log(ID);
            End = true;
            delay.TestPublicDelay();
            }
        }
    }
}

