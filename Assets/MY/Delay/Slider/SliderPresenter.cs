using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
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
        [SerializeField] HandliePresenter handlie;
        [SerializeField] Music _music;
        [SerializeField] BPMSetting _bpmSetting;
        [SerializeField] SettingPresenter setting;
        [SerializeField]AudioControl _audioControl;
        [SerializeField] CountPresenter count;
        private Slider thisSlider;
        [SerializeField]int BPM;
        public int BPMs => BPM;
        [SerializeField]int ID;
        [SerializeField] float ClampMax;
        [SerializeField] float ClampMin;
        
        
        
        public void Ready(){
            thisSlider.maxValue = audioSource.clip.length;
            thisSlider.value = 0;
            End = false;
        }

        void Start()
        {
            BPM = 120;
            thisSlider = this.gameObject.GetComponent<Slider>();
            
            handlie.OnView
            .Subscribe(_ => {
                delaySliderManager.ChangeNow(ID);
                int text = (int)((float)BPM * _audioControl.Speed.Value);
                setting.SetBPM(text.ToString());
            })
            .AddTo(this);

            _audioControl.Speed
            .Subscribe(_ =>{
                int text = (int)((float)BPM * _audioControl.Speed.Value);
                setting.SetBPM(text.ToString());
            })
            .AddTo(this);

            delaySliderManager.OnNowChanged
                .Where(now => now!=ID)
                .Subscribe(_ => view.SetColor(false))
                .AddTo(this);

            //以下CLAMP
            thisSlider.onValueChanged.AsObservable()
            .Subscribe(_ => {
                thisSlider.value = Mathf.Clamp(thisSlider.value, ClampMin, ClampMax);
                delaySliderManager.Change();
            })
            .AddTo(this);

            delaySliderManager.OnChangeClamp
            .Subscribe(_ => {
                delaySliderManager.SetMinMax(ID,out var min, out var max);
                ClampMax = max;
                ClampMin = min;
            })
            .AddTo(this);

            thisSlider.onValueChanged.AsObservable()
            .Throttle(TimeSpan.FromMilliseconds(100))
            .Subscribe(t => {
                count.PublicValidate();
            })
            .AddTo(this);

        }

        // void Update(){
        //     if(!audioSource.isPlaying) return;

        //     if(End) return;

        //     if(audioSource.time >= thisSlider.value){
        //     End = true;
        //     TestPublicDelay(BPM);
        //     }
        // }

        public void SetID(int id)
        {
            ID = id;
        }

        public void SetBPM(int bpm)
        {
            BPM = bpm;
        }

        // void TestPublicDelay(int bpm){
        //     // 一般的には44100
        //     _music.EntryPointSample = (int)(audioSource.time * audioSource.clip.frequency);
        //     _bpmSetting.ChangeBPM(bpm);
        //     _bpmSetting.Apply();
        //     //TODO UIに指示
        //     DelayPresenter.I.GO();
        // }
    }
}

