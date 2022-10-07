using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Ken.Delay
{
    public enum PM{
        Plus,
        Minus
    }

    public class Setting : MonoBehaviour
    {
        [SerializeField] Music _music;

        public void DelaySetupForAudioTime(ref float value){
            if(AudioCheck.I.TryGetAudioTIme(out var time )) value = time;
            else    throw new Exception("えら-");
        }

        public void DelayAdjustForBeat(ref float value,PM pm){
            // 「60(1分)÷BPM(テンポ)で４分音符一拍分の長さ」(s)
            // 例) 60 / 120 = 0.5s
            float oneBeat = 60f / _music.myTempo;

            if(pm == PM.Plus) value += oneBeat;
            else    value -= oneBeat;
        }

        public void DelayAdjustForSecond(ref float value,PM pm){
            if(pm == PM.Plus) value += 0.01f;
            else    value -= 0.01f;
        }
    }
}

