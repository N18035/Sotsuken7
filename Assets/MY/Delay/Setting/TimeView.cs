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
            .Subscribe(_ => startTime.text= manager.GetNowValue().ToString("F2"))
            .AddTo(this);

            manager.OnChangeClamp
            .Subscribe(_ => startTime.text = manager.GetNowValue().ToString("F2"))
            .AddTo(this);
        }

    }
}