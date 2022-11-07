using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;

namespace Ken.Delay{
    public class CountPresenter : MonoBehaviour
    {

        [SerializeField] AudioControl audioControl;
        [SerializeField] DelaySliderManager manager;
        [SerializeField] Music _music;
        [SerializeField] Setting.BPMSetting _bpmSetting;
        [SerializeField] AudioSource audioSource;
 
        bool Flag=false;

        [SerializeField]Data data;

        int tmpIndex=0;
        int NowIndex=0;


        void Update()
        {
            if(!audioSource.isPlaying) return;

            for(int i=0;i<data.Time.Count;i++){
                //1:再生時間がdata[]よりも小さいと判定が出たら終了する
                if(audioSource.time < data.Time[i])    break;
                else    tmpIndex = i;
            }

            //2:それが今のindexと同じなら変更しない
            if(NowIndex == tmpIndex)    return;
            //ここまで来たという事はtmpが新しいdelayになっている
            ValidateDelay();
            Debug.Log("パブリッシャー");
        }

        void Start(){
            audioControl.OnPlayStart
            .Subscribe(_ =>{
                // data = manager.CreateDelayTimeData();
                // StarPublish();
                ValidateDelay();
            })
            .AddTo(this);
        }

        void ValidateDelay(){
            //データ取得
            data = manager.CreateDelayTimeData();
            // 一般的には44100
            _music.EntryPointSample = (int)(data.Time[tmpIndex] * audioSource.clip.frequency);
            _bpmSetting.ChangeBPM(data.BPM[tmpIndex]);
            _bpmSetting.Apply();
            NowIndex = tmpIndex;
            //TODO UIに指示
            DelayPresenter.I.GO();
        }

        public void PublicValidate(){
            ValidateDelay();
        }
    }


    [System.Serializable]
    public class Data
    {
        [SerializeField] public List<float> Time;
        [SerializeField] public List<int> BPM;

        public Data(List<float> t, List<int> b){
            Time = new List<float>(){0};
            BPM = new List<int>(){120};
            

            Time = t;
            BPM = b;
        }
    }
}
