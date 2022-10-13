using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using VContainer.Unity;
using Ken.Setting;

namespace Ken.Delay
{
    public class SliderPresenter : MonoBehaviour
    {
        public bool End=false;
        [SerializeField] AudioSource audioSource;
        [SerializeField] DelaySliderManager delaySliderManager;
        // [SerializeField] DelayBPMPresenter BPMPresenter;
        [SerializeField] SliderView view;
        [SerializeField] Music _music;
        [SerializeField] BPMSetting _bpmSetting;
        [SerializeField] SettingPresenter setting;
        private Slider thisSlider;
        [SerializeField]int BPM;
        [SerializeField]int ID;
        
        public void Ready(){
            thisSlider.maxValue = audioSource.clip.length;
            thisSlider.value = 0;
            End = false;
        }

        void Start()
        {
            BPM = 120;
            thisSlider = this.gameObject.GetComponent<Slider>();
            
            thisSlider.onValueChanged.AsObservable()
            .Throttle(TimeSpan.FromMilliseconds(100))
            .Subscribe(t => {
                delaySliderManager.ChangeNow(ID);
                view.SetColor(true);
                setting.SetBPM(BPM.ToString());
            })
            .AddTo(this);

            delaySliderManager.OnNowChanged
                .Where(now => now!=ID)
                .Subscribe(_ => view.SetColor(false))
                .AddTo(this);
                
        }

        void Update(){
            if(!audioSource.isPlaying) return;

            if(End) return;

            if(audioSource.time >= thisSlider.value){
            End = true;
            TestPublicDelay(BPM);
            }
        }

        public void SetID(int id)
        {
            ID = id;
        }

        public void SetBPM(int bpm)
        {
            BPM = bpm;
        }

        public void TestPublicDelay(int bpm){
            // 一般的には44100
            _music.EntryPointSample = (int)(audioSource.time * audioSource.clip.frequency);
            _bpmSetting.ChangeBPM(bpm);
            _bpmSetting.Apply();
            //TODO UIに指示
            DelayPresenter.I.GO();
        }
    }
}

