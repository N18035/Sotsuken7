using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.Save{
    public class SavePresenter : MonoBehaviour
    {
        [SerializeField] Button saveB;
        [SerializeField] Button loadB;
        [SerializeField] Text infoText;
        [SerializeField] SaveManager manager;
        [SerializeField] AudioImport import;
        

        void Start(){
            saveB.onClick.AsObservable()
            .Where(_ => !AudioCheck.I.ClipIsNull())
            .Subscribe(_ =>manager.Save())
            .AddTo(this);

            loadB.onClick.AsObservable()
            .Where(_ => !AudioCheck.I.ClipIsNull())
            .Subscribe(_ =>manager.Load())
            .AddTo(this);

            manager.Info
            .Subscribe(t => infoText.text = t)
            .AddTo(this);

            import.OnSelectMusic
            .Subscribe(_ => infoText.text = "")
            .AddTo(this);
        }
    }
}
