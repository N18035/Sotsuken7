using UnityEngine;
using UnityEngine.UI;

namespace Ken.Main.SeekBar
{
    public class SeekBarView : MonoBehaviour
    {
        [SerializeField] Slider _slider;

        public void Init(){
            //スライダーの単位は%なので、最大値は100になる
            _slider.maxValue = 100f;
            //初期化
            _slider.value = 0;
        }

        public void MoveSeekBar(float v){
            _slider.value = v;
        }
    }

}
