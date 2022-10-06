using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

// public interface

namespace Ken.Delay
{
    public class DelayCore : Singleton<DelayCore>
    {
        public IObservable<Unit> OnSelectDelay => _selectDelay;
        private Subject<Unit> _selectDelay = new Subject<Unit>();

        // [SerializeField] DelayModel delay;
        [SerializeField] DelaySliderManager manager;
        [SerializeField] Music _music;
        
        public void ReadyDelay(){
            manager.Reset();
        }

        public void GO(){
            _selectDelay.OnNext(Unit.Default);
            _music.LoadTiming();
        }
    }
}

