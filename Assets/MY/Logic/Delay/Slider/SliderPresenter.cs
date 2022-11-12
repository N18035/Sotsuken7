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
        public bool Origin=true;
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
        [SerializeField] DelaySliderManager manager;
        [SerializeField] TimeView time;
        private Slider thisSlider;
        [SerializeField]int BPM;
        public int BPMs => BPM;
        [SerializeField]int ID;
        
        
        
        
        public void Ready(){
            thisSlider = this.gameObject.GetComponent<Slider>();
            thisSlider.maxValue = audioSource.clip.length;
            thisSlider.value = 0;
            Origin = false;
        }

        void Start()
        {
            BPM = 120;
            thisSlider = this.gameObject.GetComponent<Slider>();
            
            handlie.OnHandle
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

                if(_audioControl.Speed.Value == 1)  setting.SetBPMColor(Color.black);
                else setting.SetBPMColor(Color.red);
            })
            .AddTo(this);

            delaySliderManager.OnNowChanged
                .Where(now => now!=ID)
                .Subscribe(_ => view.SetColor(false))
                .AddTo(this);

            delaySliderManager.OnNowChanged
            .Where(now => now==ID)
            .Subscribe(_ => view.SetColor(true))
            .AddTo(this);


            // delaySliderManager.OnChangeClamp
            // .Subscribe(_ => {
            //     delaySliderManager.SetMinMax(ID,out var min, out var max);
            //     ClampMax = max;
            //     ClampMin = min;
            // })
            // .AddTo(this);

            thisSlider.onValueChanged.AsObservable()
            .Throttle(TimeSpan.FromMilliseconds(100))
            .Subscribe(t => {
                manager.CheckBatting();//被りがあれば警告
                count.PublicValidate();
                time.U();
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

