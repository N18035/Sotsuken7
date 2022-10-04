using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PlaySpeedPresenter : MonoBehaviour
{
    [SerializeField] Dropdown _dropdown;
    [SerializeField] Ken.AudioControll _audioCon;
    [SerializeField] GameObject setting;
    [SerializeField] Ken.Setting.BPMSettingPresenter bPMSettingPresenter;

    void Start(){
        _dropdown.onValueChanged.AsObservable()
        .Subscribe(v => ChangeSpeed(v))
        .AddTo(this);
    }

    void ChangeSpeed(int v){
        float speed = float.Parse(_dropdown.options[v].text);
        _audioCon.ChangeSpeed(speed);
        bPMSettingPresenter.SpeedChanged(speed);
        // setting.SetActive(false);
    }
}
