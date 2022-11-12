using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.Delay
{
public class HandliePresenter : MonoBehaviour
{
    bool isGrag;
    public IObservable<Unit> OnHandle => _handle;
    private Subject<Unit> _handle = new Subject<Unit>();
    [SerializeField] SliderView view;
    
    void Start()
    {
        var eventTrigger = this.gameObject.GetComponent<ObservableEventTrigger>();


        eventTrigger.OnPointerEnterAsObservable()
            .Subscribe(_ =>{
                Selected();
                _handle.OnNext(Unit.Default);
            })
            .AddTo(this);

        eventTrigger.OnPointerDownAsObservable()
            .Subscribe(_ =>{
                isGrag=true;
                _handle.OnNext(Unit.Default);
                Selected();
            })
            .AddTo(this);

        eventTrigger.OnPointerExitAsObservable()
            .Where(_ => !isGrag)
            .Subscribe(_ => view.SmallImage())
            .AddTo(this);
        
        eventTrigger.OnPointerUpAsObservable()
            .Subscribe(_ =>{
                view.SmallImage();
                isGrag=false;
            })
            .AddTo(this);
    }

    void Selected(){
        view.BigImage();
        view.SetColor(true);
    }
}

}
