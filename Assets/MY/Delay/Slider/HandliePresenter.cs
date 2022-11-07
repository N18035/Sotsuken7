using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class HandliePresenter : MonoBehaviour
{
    bool isGrag;
    public IObservable<Unit> OnView => _view;
    private Subject<Unit> _view = new Subject<Unit>();
    [SerializeField] SliderView view;
    
    void Start()
    {
        var eventTrigger = this.gameObject.GetComponent<ObservableEventTrigger>();


        eventTrigger.OnPointerEnterAsObservable()
            .Subscribe(_ => Selected())
            .AddTo(this);

        eventTrigger.OnPointerDownAsObservable()
            .Subscribe(_ =>{
                isGrag=true;
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
        _view.OnNext(Unit.Default);
        view.BigImage();
        view.SetColor(true);
    }
}
