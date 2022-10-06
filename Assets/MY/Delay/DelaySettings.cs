using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Ken.Delay
{
    public class DelaySettings : MonoBehaviour
    {
        public IObservable<Unit> OnSelectDelay => _selectDelay;
        private Subject<Unit> _selectDelay = new Subject<Unit>();
        [SerializeField] Music _music;
        [SerializeField] DelaySliderManager delaySliderManager;


        public void DelaySetupForAudioTime(){
            // if(!Audio.I.TryGetAudioTIme(out var time )) return;
            // _delaySlider.value = time;
            Debug.LogError("無効");
        }

        public void ChangeDelay(int i){
            // if(_delaySlider.value == _delaySlider.maxValue) return;
            //「60(1分)÷BPM(テンポ)で４分音符一拍分の長さ」(s)
            //例) 60 / 120 = 0.5s
            // float oneBeat = 60f / _music.myTempo;

            // if(i<0) _delaySlider.value -= oneBeat;
            // else    _delaySlider.value += oneBeat;
            Debug.LogError("無効");
        }

        public void ChangeDelay2(int i){
            // if(_delaySlider.value == _delaySlider.maxValue) return;
            // _delaySlider.value += 0.01f;

            // if(i<0) _delaySlider.value -= 0.01f;
            // else    _delaySlider.value += 0.01f;
            Debug.LogError("無効");
        }
    }
}

