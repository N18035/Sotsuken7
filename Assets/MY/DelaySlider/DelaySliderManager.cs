using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ken.Delay
{
    public class DelaySliderManager : MonoBehaviour,IClamp
    {
        public int now=0;

        [SerializeField] GameObject SliderPrefab;
        public List<GameObject> Sliders = new List<GameObject>(1);
        
        [SerializeField] AudioSource _musicEngine;
        [SerializeField] DelayModel model;


        public void AddSlider(){
            var t = Instantiate(SliderPrefab,this.transform.position,Quaternion.identity);
            t.transform.SetParent(this.gameObject.transform,false);
            t.transform.localPosition = new Vector3(-3f,0,0);
            Sliders.Add(t);

            t.GetComponent<Slider>().value = Sliders[now].GetComponent<Slider>().value + 0.5f;
            
            now = Sliders.Count -1;
            t.GetComponent<DelaySlider>().ID = now;
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
            Sliders[0].GetComponent<Slider>().maxValue = _musicEngine.clip.length;
            Sliders[0].GetComponent<Slider>().value = 0;
        }


        #region 保留機能
            public void Change(){
            // Handles[now].color = Color.white;

            // Debug.Log(now);
            now+=1;
            if(now == Sliders.Count) now = 0;

            Sliders[now].transform.SetAsLastSibling();
            // text.text = now.ToString();
            // Handles[now].color = Color.red;
        }

        #endregion
    }
}

