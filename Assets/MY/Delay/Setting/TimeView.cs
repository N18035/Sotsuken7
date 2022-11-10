using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.Delay{
    public class TimeView : MonoBehaviour
    {
        Text startTime;
        [SerializeField] DelaySliderManager manager;

        private void Start() {
            startTime = this.gameObject.GetComponent<Text>();

            manager.OnNowChanged
            .Subscribe(_ => U())
            .AddTo(this);
        }

        public void U(){
            startTime.text= manager.GetNowValue().ToString("F3");
        }

    }
}