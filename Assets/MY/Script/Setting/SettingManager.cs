using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;


public class SettingManager : MonoBehaviour
{
    //uGUIのデフォルトのUnityイベントの名前をしたObservableが用意されている
    // [SerializeField] Button BPMButton;
    // [SerializeField] Button BeatTypeButton;
    // [SerializeField] Button BeatSoundButton;

    public Button[] buttons = new Button[3];
    // public Button[] Applybuttons = new Button[2];
    public GameObject[] DetailSetting = new GameObject[3];

    void Start()
    {
        buttons[0].OnClickAsObservable()
        .Subscribe(_ => ButtonManagement(0))
        .AddTo(this);

        buttons[1].OnClickAsObservable()
        .Subscribe(_ => ButtonManagement(1))
        .AddTo(this);

        buttons[2].OnClickAsObservable()
        .Subscribe(_ => ButtonManagement(2))
        .AddTo(this);

        // Applybuttons[0].OnClickAsObservable()
        // .Subscribe(_ => Apply(0))
        // .AddTo(this);

        // Applybuttons[1].OnClickAsObservable()
        // .Subscribe(_ => Apply(1))
        // .AddTo(this);
    }

    private void ButtonManagement(int b){
        DetailSetting[b].SetActive(true);

        for(int i=0;i<buttons.Length;i++){
            if(i!=b){
                buttons[i].enabled = false;
            }
        }
    }
    
    public void Apply(int b){
        DetailSetting[b].SetActive(false);
        buttons[0].enabled = true;
        buttons[1].enabled = true;
        buttons[2].enabled = true;

    }

}
