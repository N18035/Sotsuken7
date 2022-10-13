using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.Delay{
    public class SettingPresenter : MonoBehaviour
    {

        // private readonly ReactiveProperty<float> _delaySecond = new ReactiveProperty<float>();
        [SerializeField] InputField bpmInputField;
        [SerializeField] Button buttonMinusBeat;
        [SerializeField] Button buttonMinusSeconds;
        [SerializeField] Button buttonPulsBeat;
        [SerializeField] Button buttonPulsSeconds;
        [SerializeField] Button nowTimeSet;
        // [SerializeField] Text _delayStartText;
        [SerializeField] Button addSlider;
        [SerializeField] Button removeSlider;
        

        [SerializeField] DelaySliderManager manager;

        void Start(){

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

            addSlider.onClick.AsObservable()
            .Subscribe(_ => manager.AddSlider())
            .AddTo(this);

            removeSlider.onClick.AsObservable()
            .Subscribe(_ => manager.RemoveSlider())
            .AddTo(this);

            bpmInputField.OnEndEditAsObservable()
            .Where(t => t!=null)
            .Subscribe(_ => manager.BPMSet(int.Parse(bpmInputField.text)))
            .AddTo(this);

            // _delaySecond
            // .Subscribe(t => _delayStartText.text = t.ToString("F2"))
            // .AddTo(this);
        }

        public void SetBPM(string bpm)
        {
            bpmInputField.text = bpm;
        }
    }
}
