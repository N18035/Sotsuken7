using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Ken.Setting
{
    public class BeatTypeSetting : MonoBehaviour
    {
        private ReactiveProperty<int> _beattype = new ReactiveProperty<int>();

        public IObservable<Unit> OnSelectBeatType => _selectBeatType;
        private Subject<Unit> _selectBeatType = new Subject<Unit>();

        [SerializeField] Text text;

        [SerializeField] Music music;

        void Start(){
            ChangeBeatType(4);

            _beattype
            .Subscribe(t => text.text = t.ToString())
            .AddTo(this);
        }

        public void ChangeBeatType(int a){//引数は3か4
            music.myBar=a*a;
            music.myBeat=a;

            _beattype.Value =a;
            _selectBeatType.OnNext(Unit.Default);
        }
    }
}