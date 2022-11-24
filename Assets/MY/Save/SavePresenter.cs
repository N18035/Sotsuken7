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
        [SerializeField] SaveManager manager;

        void Start(){
            
            thisInput.OnEndEditAsObservable()
            .Where(t => t!=null)
            .Where(t => t!="")
            .Subscribe(t =>{
                manager.SetName(t);
            })
            .AddTo(this);

            saveB.onClick.AsObservable()
            .Subscribe(_ =>manager.Save())
            .AddTo(this);

            loadB.onClick.AsObservable()
            .Subscribe(_ =>manager.Load())
            .AddTo(this);
        }
    }
}
