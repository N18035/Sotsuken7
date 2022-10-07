using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using VContainer.Unity;

namespace Ken.Delay
{
    public class DelaySliderPresenter : MonoBehaviour
    {
        public bool End=false;
        [SerializeField] AudioSource audioSource;
        [SerializeField] DelayModel delay;
        private Slider thisSlider;
        
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
                // delay.TestPublicDelay();
            })
            .AddTo(this);
        }

        void Update(){
            if(!audioSource.isPlaying) return;

            if(End) return;

            if(audioSource.time >= thisSlider.value){
            End = true;
            delay.TestPublicDelay();

            }
        }
    }
}

