using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.Delay{
    public class DelayView : MonoBehaviour
    {

        // private readonly ReactiveProperty<float> _delaySecond = new ReactiveProperty<float>();

        [SerializeField] Button buttonPulsBeat;
        [SerializeField] Button buttonPulsSeconds;
        [SerializeField] Button buttonMinusBeat;
        [SerializeField] Button buttonMinusSeconds;
        [SerializeField] Button nowTimeSet;
        // [SerializeField] Text _delayStartText;
        [SerializeField] Button addSlider;
        [SerializeField] Button removeSlider;


        [SerializeField] DelaySettings _settings;
        [SerializeField] DelaySliderManager manager;

        void Start(){

            buttonPulsBeat.onClick.AsObservable()
            .Subscribe(_ =>_settings.ChangeDelay(1))
            .AddTo(this);

            buttonPulsSeconds.onClick.AsObservable()
            .Subscribe(_ => _settings.ChangeDelay2(1))
            .AddTo(this);

            buttonMinusBeat.onClick.AsObservable()
            .Subscribe(_ => _settings.ChangeDelay(-1))
            .AddTo(this);

            buttonMinusSeconds.onClick.AsObservable()
            .Subscribe(_ => _settings.ChangeDelay2(-1))
            .AddTo(this);

            nowTimeSet.onClick.AsObservable()
            .Subscribe(_ => _settings.DelaySetupForAudioTime())
            .AddTo(this);

            // _delaySecond
            // .Subscribe(t => _delayStartText.text = t.ToString("F2"))
            // .AddTo(this);

            addSlider.onClick.AsObservable()
            .Subscribe(_ => manager.AddSlider())
            .AddTo(this);

            removeSlider.onClick.AsObservable()
            .Subscribe(_ => manager.RemoveSlider())
            .AddTo(this);
        }
    }
}
