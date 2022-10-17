using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

[RequireComponent(typeof(ObservableEventTrigger))]
public class SliderView : MonoBehaviour
{
    [SerializeField] Image handle;
    private static readonly Color red = Color.red;
    private static readonly Color white = Color.white;

    public IObservable<Unit> OnView => _view;
    private Subject<Unit> _view = new Subject<Unit>();

    void Start(){
        var eventTrigger = this.gameObject.GetComponent<ObservableEventTrigger>();
        eventTrigger.OnPointerDownAsObservable()
            .Subscribe(_ => _view.OnNext(Unit.Default))
            .AddTo(this);

        // this.gameObject.GetComponent<Slider>().onValueChanged.AsObservable()
        //     .Throttle(TimeSpan.FromMilliseconds(500))
        //     .Subscribe(t => {
        //     })
        //     .AddTo(this);
    }

    public void SetColor(bool on){
        if(on)  handle.color = red;
        else handle.color = white;
    }
}
