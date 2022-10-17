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
        [SerializeField] Ken.Setting.BPMSetting _bpmSetting;
        [SerializeField] AudioSource audioSource;
 
        bool Flag=false;

        [SerializeField]Data data;

        void Start(){
            audioControl.OnPlayStart
            .Subscribe(_ =>{
                data = manager.CreateDelayTimeData();
               Enf().Forget();
            })
            .AddTo(this);
        }


        //FIXMEキャンセルを呼べ
        async UniTaskVoid Enf(){
            int i=0;

            while(i !=data.Time.Count){
                await UniTask.Delay(data.Time[i]);
                PublicDelay(data.BPM[i]);
                i++;
            }
        }

        void PublicDelay(int bpm){
            // 一般的には44100
            _music.EntryPointSample = (int)(audioSource.time * audioSource.clip.frequency);
            _bpmSetting.ChangeBPM(bpm);
            _bpmSetting.Apply();
            //TODO UIに指示
            DelayPresenter.I.GO();
        }
    }


    [System.Serializable]
    public class Data
    {
        [SerializeField] public List<int> Time;
        [SerializeField] public List<int> BPM;

        public Data(List<float> t, List<int> b){
            foreach(int num in t)
            {
                // Console.WriteLine(num * 2);
                Time[num] = (int)(t[num] * 1000);
                if(num!=0){
                    var tmp = Time[num]-Time[num-1];
                    Time[num] = tmp;
                }
            }

            BPM = b;
        }
    }
}
