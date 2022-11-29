using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.Save{
    public class SavePresenter : MonoBehaviour
    {
        [SerializeField] InputField thisInput;
        [SerializeField] Button saveB;
        [SerializeField] Button loadB;
        [SerializeField] Text infoText;
        [SerializeField] SaveManager manager;
        [SerializeField] AudioImport import;
        

        void Start(){
            
            thisInput.OnEndEditAsObservable()
            .Where(t => t!=null)
            .Where(t => t!="")
            .Subscribe(t =>{
                manager.SetName(t);
            })
            .AddTo(this);

            saveB.onClick.AsObservable()
            .Where(_ => !thisInput.text.Equals(""))
            .Where(_ => !AudioCheck.I.ClipIsNull())
            .Subscribe(_ =>manager.Save())
            .AddTo(this);

            saveB.onClick.AsObservable()
            .Where(_ => thisInput.text.Equals(""))
            .Where(_ => !AudioCheck.I.ClipIsNull())
            .Subscribe(_ =>infoText.text = "ファイル名を入力してください")
            .AddTo(this);

            loadB.onClick.AsObservable()
            .Where(_ => !AudioCheck.I.ClipIsNull())
            // .Where(_ => !AudioCheck.I.IsPlaying())
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
