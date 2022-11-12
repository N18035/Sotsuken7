using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Ken.Setting
{

    public class BeatSoundPresenter : MonoBehaviour{
        [SerializeField] Ken.Beat.BeatSound _beatSound;
        [SerializeField] Dropdown _dropdown;
        [SerializeField] Button button;
        [SerializeField] GameObject setting;
        [SerializeField] Text text;

        Dictionary<int, string> ClipNameDictionary = new Dictionary<int, string>()
        {
            {0, "電子音"},
            {1, "拍手"},
            {2, "音無し"},
        };

        void Start(){
            _dropdown.onValueChanged.AsObservable()
            .Subscribe(v => Change(v))
            .AddTo(this);

            button.onClick.AsObservable()
            .Where(_ => !setting.activeSelf)
            .Subscribe(_ => setting.SetActive(true))
            .AddTo(this);
        }

        public void Change(int v)
        {    
            _beatSound.SetBeatSound(v);
            text.text = ClipNameDictionary[_dropdown.value];
            setting.SetActive(false);
        }
    }
}
