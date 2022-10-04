using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Ken.Main{

    public class ZoomController : MonoBehaviour
    {
        public IReadOnlyReactiveProperty<int> ZoomLevel => _zoomLevel;
        private readonly ReactiveProperty<int> _zoomLevel = new ReactiveProperty<int>(1);

        public int MaxZoomLevel => maxZoomLevel;
        private int maxZoomLevel=10;

        [SerializeField] Image ViewPortMask;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] Text text;

        #region  外部公開

        void Start(){
            _zoomLevel
            .Where(l => l==1)
            .Subscribe(_ => ViewPortMask.enabled=true)
            .AddTo(this);

            _zoomLevel
            .Where(l => l!=1)
            .Subscribe(_ => ViewPortMask.enabled=false)
            .AddTo(this);

            _zoomLevel
            .Subscribe(zl =>text.text=zl.ToString())
            .AddTo(this);
        }

        public void AddZoomLevel(){
            if(_zoomLevel.Value >= maxZoomLevel || _audioSource.clip == null) return;
            _zoomLevel.Value ++;
        }

        public void SubZoomLevel(){
            if(_zoomLevel.Value == 1 || _audioSource.clip == null) return;
            _zoomLevel.Value --;
        }
        #endregion
    }
}