using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.Delay{
    public class SettingPresenter : MonoBehaviour
    {

        // private readonly ReactiveProperty<float> _delaySecond = new ReactiveProperty<float>();
        [SerializeField] Toggle toggle;
        [SerializeField] InputField bpmInputField;
        [SerializeField] Button buttonMinusBeat;
        [SerializeField] Button buttonMinusSeconds;
        [SerializeField] Button buttonPulsBeat;
        [SerializeField] Button buttonPulsSeconds;
        [SerializeField] Button nowTimeSet;
        [SerializeField] Button addSliderRight;
        [SerializeField] Button addSliderLeft;
        [SerializeField] Button removeSlider;
        

        [SerializeField] DelaySliderManager manager;

        void Start(){

            toggle.onValueChanged.AsObservable()
            .Subscribe(t => manager.allChange=t)
            .AddTo(this);

            buttonPulsBeat.onClick.AsObservable()
            .Subscribe(_ =>manager.DelayAdjustForBeat(PM.Plus))
            .AddTo(this);

            buttonPulsSeconds.onClick.AsObservable()
            .Subscribe(_ => manager.DelayAdjustForSecond(PM.Plus))
            .AddTo(this);

            buttonMinusBeat.onClick.AsObservable()
            .Subscribe(_ => manager.DelayAdjustForBeat(PM.Minus))
            .AddTo(this);

            buttonMinusSeconds.onClick.AsObservable()
            .Subscribe(_ => manager.DelayAdjustForSecond(PM.Minus))
            .AddTo(this);

            nowTimeSet.onClick.AsObservable()
            .Subscribe(_ => manager.DelaySetupForAudioTime())
            .AddTo(this);

            addSliderLeft.onClick.AsObservable()
            .Subscribe(_ => manager.AddSlider(PM.Minus))
            .AddTo(this);

            addSliderRight.onClick.AsObservable()
            .Subscribe(_ => manager.AddSlider(PM.Plus))
            .AddTo(this);

            removeSlider.onClick.AsObservable()
            .Subscribe(_ => manager.RemoveSlider())
            .AddTo(this);

            bpmInputField.OnEndEditAsObservable()
            .Where(t => t!=null)
            .Where(t => t!="")
            .Subscribe(t => manager.BPMSet(t))
            .AddTo(this);
        }

        public void SetBPM(string bpm)
        {
            bpmInputField.text = bpm;
        }
    }
}
