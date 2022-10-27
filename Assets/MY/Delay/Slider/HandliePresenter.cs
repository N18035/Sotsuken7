using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class HandliePresenter : MonoBehaviour
{
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        var eventTrigger = this.gameObject.AddComponent<ObservableEventTrigger>();
        image = this.gameObject.GetComponent<Image>();


        eventTrigger.OnPointerEnterAsObservable()
            .Subscribe(_ => BigImage())
            .AddTo(this);

    }

    void BigImage(){
        image.transform.localScale = new Vector3(3f, 3f, 3f);
    }

}
