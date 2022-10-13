using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Ken.Delay
{
    public class DelaySliderManager : MonoBehaviour,IClamp
    {
        public int now=0;

        public List<GameObject> Sliders = new List<GameObject>(1);
        
        [SerializeField] AudioSource _musicEngine;

        public IObservable<Unit> OnChangeClamp => _clampChange;
        private Subject<Unit> _clampChange = new Subject<Unit>();

        public IReactiveProperty<int> OnNowChanged => _nowChange;
        private readonly ReactiveProperty<int> _nowChange = new ReactiveProperty<int>();

        //委譲
        Add add;
        Setting setting;

        void Start(){
            add = this.GetComponent<Add>();
            setting = new Setting();
        }


        public void AddSlider(){
            var obj = add.Instant();
            Sliders.Add(obj);
            //被り防止はいったん無し
            // t.GetComponent<Slider>().value = Sliders[now].GetComponent<Slider>().value + 0.5f;
            now = Sliders.Count -1;
            Sliders[now].GetComponent<SliderPresenter>().SetID(now);
        }

        public void RemoveSlider(){
            if(now <= 0) return;

            Sliders.RemoveAt(now);
            Destroy(Sliders[now]);
            now--;
        }

        //SliderにClampを与えます
        public void SetMinMax(out float min,out float max){
            min = 0;
            max = _musicEngine.clip.length;

            //となり合うvalueをCLAMPにぶち込みます
            if(Sliders.Count != 1){
                for(int a=0;a<Sliders.Count;a++){
                    if(a==0)   {
                        min = 0;
                        max=Sliders[a+1].GetComponent<Slider>().value;
                    } 
                    else if(a==Sliders.Count-1){
                        min = Sliders[a-1].GetComponent<Slider>().value;
                        max = _musicEngine.clip.length;
                    } 
                    else{
                        min = Sliders[a-1].GetComponent<Slider>().value;
                        max = Sliders[a+1].GetComponent<Slider>().value;
                    }
                }
            }
            return;
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
            // setting.DelaySetupForAudioTime(Slider[now].value)
        }

        public void DelayAdjustForBeat(PM pm){
            //FIXME BPMを追加しようね
            // if(Slider[now].value == _delaySlider.maxValue) return;
            // setting.DelayAdjustForBeat(Slider[now].value,pm);
        }

        public void DelayAdjustForSecond(PM pm){
            // if(Slider[now].value == _delaySlider.maxValue) return;
            // setting.DelayAdjustForSecond(Slider[now].value,pm);
        }

        public void BPMSet(int v)
        {
            Sliders[now].GetComponent<SliderPresenter>().SetBPM(v);
        }

       public void ChangeNow(int id){
            now+=id;
            //Sliders[now].transform.SetAsLastSibling();
            _nowChange.Value = id;
       }


        #region 保留機能


        //clampのやつ
        public void Change(){
            _clampChange.OnNext(Unit.Default);
        }
        #endregion
    }
}

