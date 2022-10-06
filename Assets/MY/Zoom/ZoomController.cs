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
        public int MaxZoomLevel=10;

        public void AddZoomLevel(){
            if(_zoomLevel.Value >= MaxZoomLevel) return;
            _zoomLevel.Value ++;
        }

        public void SubZoomLevel(){
            if(_zoomLevel.Value == 1) return;
            _zoomLevel.Value --;
        }

    }
}