using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Ken.Main;

namespace Ken.Zoom
{
    public class ZoomPresenter : MonoBehaviour
    {
        [SerializeField] Text text;
        [SerializeField] Button Plus;
        [SerializeField] Button Minus;
        [SerializeField] ZoomController _zoom;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] Image ViewPortMask;


        void Start(){
            _zoom.ZoomLevel
            .Subscribe(zl =>text.text=zl.ToString())
            .AddTo(this);

            Plus.onClick.AsObservable()
            .Where(_ => _audioSource.clip != null)
            .Subscribe(_ => _zoom.AddZoomLevel())
            .AddTo(this);

            Minus.onClick.AsObservable()
            .Where(_ => _audioSource.clip != null)
            .Subscribe(_ => _zoom.SubZoomLevel())
            .AddTo(this);

            _zoom.ZoomLevel
            .Where(l => l==1)
            .Subscribe(_ => ViewPortMask.enabled=true)
            .AddTo(this);

            _zoom.ZoomLevel
            .Where(l => l!=1)
            .Subscribe(_ => ViewPortMask.enabled=false)
            .AddTo(this);
        }
    }
}

