using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Ken.Delay
{
    public class DelaySliderManager : MonoBehaviour
    {
        public int now=0;

        public List<GameObject> Sliders = new List<GameObject>(1);
        
        [SerializeField] AudioSource _musicEngine;
        [SerializeField] CountPresenter count;

        public IObservable<Unit> OnChangeClamp => _clampChange;
        private Subject<Unit> _clampChange = new Subject<Unit>();

        public IReactiveProperty<int> OnNowChanged => _nowChange;
        private readonly ReactiveProperty<int> _nowChange = new ReactiveProperty<int>();
        [SerializeField]AudioControl _audioControl;

        public bool allChange=false;

        //委譲
        Add add;

        void Start(){
            add = this.GetComponent<Add>();

        }


        public void AddSlider(PM pm){
            //FIXMEホントは極端に端っこやと弾く
            if(AudioCheck.I.IsNull()) return;
            
            // var obj = add.Instant();
            // int tmp;
            // //右に追加
            // // if(pm==PM.Plus){
            //     // if(now==Sliders.Count-1){
            //     //     tmp=Sliders.Count-1;
            //     //     Sliders.Add(obj);
            //     // }else{
            //     //     tmp=now+1;
            //     //     Sliders.Insert(now+1,obj);
            //     // }
            //     // obj.GetComponent<Slider>().value = Sliders[tmp].GetComponent<Slider>().value + 0.5f;
            // // }
            // // else{
            // //     if(now==0){
            // //         Destroy(obj);
            // //         return;
            // //     }else{
            // //         tmp=now-1;
            // //         Sliders.Insert(now-1,obj);
            // //     }
            // //     obj.GetComponent<Slider>().value = Sliders[tmp].GetComponent<Slider>().value - 0.5f;
            // // }
            // //被り防止はいったん無し
            // Sliders[tmp].GetComponent<SliderPresenter>().SetID(tmp);
            // ChangeNow(tmp);

            var obj = add.Instant();
            Sliders.Add(obj);
            //被り防止はいったん無し
            obj.GetComponent<Slider>().value = Sliders[now].GetComponent<Slider>().value + 0.5f;
            now = Sliders.Count -1;
            Sliders[now].GetComponent<SliderPresenter>().SetID(now);
        }

        public void RemoveSlider(){
            if(AudioCheck.I.IsNull()) return;
            if(now <= 0) return;

            Sliders.RemoveAt(now);
            Destroy(Sliders[now]);
            now--;
        }

        //SliderにClampを与えます
        public void SetMinMax(int ID,out float min,out float max){
            min = 0;
            max = _musicEngine.clip.length;

            if(Sliders.Count == 1) return;
            if(ID==0)   {
                min = 0;
                max=Sliders[ID+1].GetComponent<Slider>().value;
            } 
            else if(ID==Sliders.Count-1){
                min = Sliders[ID-1].GetComponent<Slider>().value;
                max = _musicEngine.clip.length;
            } 
            else{
                min = Sliders[ID-1].GetComponent<Slider>().value;
                max = Sliders[ID+1].GetComponent<Slider>().value;
            }

            //となり合うvalueをCLAMPにぶち込みます
            // if(Sliders.Count != 1){
                // for(int a=0;a<Sliders.Count;a++){
                    // if(a==0)   {
                    //     min = 0;
                    //     max=Sliders[a+1].GetComponent<Slider>().value;
                    // } 
                    // else if(a==Sliders.Count-1){
                    //     min = Sliders[a-1].GetComponent<Slider>().value;
                    //     max = _musicEngine.clip.length;
                    // } 
                    // else{
                    //     min = Sliders[a-1].GetComponent<Slider>().value;
                    //     max = Sliders[a+1].GetComponent<Slider>().value;
                    // }
                // }
            // }
            return;
        }

        //clampのやつ
        public void Change(){
            _clampChange.OnNext(Unit.Default);
        }



        //初期化
        public void Reset(){
            //消す
            for(int i=1;i<Sliders.Count;i++){
                Destroy(Sliders[i]);
                Sliders.RemoveAt(i);
            }
            
            //初期値代入
            Sliders[0].GetComponent<SliderPresenter>().Ready();
        }

        public void DelaySetupForAudioTime(){
            if(AudioCheck.I.TryGetAudioTIme(out var time ))
                Sliders[now].GetComponent<Slider>().value = time;
            else    throw new Exception("えら-");
        }

        public void DelayAdjustForBeat(PM pm){
            if(AudioCheck.I.IsNull()) return;
            //多分clampしてくれる
            // if(Sliders[now].GetComponent<Slider>().value == _delaySlider.maxValue) return;
            
            // 「60(1分)÷BPM(テンポ)で４分音符一拍分の長さ」(s)
            // 例) 60 / 120 = 0.5s
            float oneBeat = 60f / Sliders[now].GetComponent<SliderPresenter>().BPMs;

            if(pm == PM.Plus) Sliders[now].GetComponent<Slider>().value += oneBeat;
            else    Sliders[now].GetComponent<Slider>().value -= oneBeat;
        }

        public void DelayAdjustForSecond(PM pm){
            if(AudioCheck.I.IsNull()) return;
            //多分clampしてくれる
            // if(Slider[now].value == _delaySlider.maxValue) return;
            if(pm == PM.Plus) Sliders[now].GetComponent<Slider>().value += 0.01f;
            else    Sliders[now].GetComponent<Slider>().value -= 0.01f;
        }

        public void BPMSet(string v)
        {
            //見た目から現実に変換する必要がある
            int bpm = (int)(float.Parse(v) / _audioControl.Speed.Value);

            if(allChange){
                for(int i=0;i<Sliders.Count;i++){
                    Sliders[i].GetComponent<SliderPresenter>().SetBPM(bpm);
                }
            }else            Sliders[now].GetComponent<SliderPresenter>().SetBPM(bpm);

            count.PublicValidate();
        }

       public void ChangeNow(int id){
            now=id;
            //Sliders[now].transform.SetAsLastSibling();
            _nowChange.Value = id;
            _clampChange.OnNext(Unit.Default);
       }

       //テスト段階
        public Data CreateDelayTimeData(){
            List<float> time = new List<float>(){0};
            List<int> bpm = new List<int>(){0};
            // time[0] = Sliders[0].GetComponent<Slider>().value;
            time[0] = Sliders[0].GetComponent<Slider>().value;
            bpm[0] = Sliders[0].GetComponent<SliderPresenter>().BPMs;

            for(int i=1;i<Sliders.Count;i++){
                time.Add(Sliders[i].GetComponent<Slider>().value);
                bpm.Add(Sliders[i].GetComponent<SliderPresenter>().BPMs);
            }
            Data data = new Data(time,bpm);
            return data;
        }

        public float GetNowValue(){
            return Sliders[now].GetComponent<Slider>().value;
        }


        #region 保留機能
        #endregion
    }

    public enum PM{
        Plus,
        Minus
    }
}
