using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Ken.Main{
    public class Content : MonoBehaviour
    {
        #region  変数類
        //単位はUIのRect座標
        public readonly float _originStart=-382.96f;
        public readonly float _originalEnd=388.62f;
        //contentオブジェクトのwidthを見るといいよ
        [SerializeField] private int _originalSoundWaveLength=780;
        private int _nowSoundWaveLength;
        //多分音声波形の縦に関する部分だと思う
        int tate=50;

        public float NowStart => _nowStart;
        public float NowEnd => _nowEnd;
        [SerializeField]private float _nowStart;
        [SerializeField]private float _nowEnd;
        
        #endregion

        #region  外部参照
        [SerializeField] GameObject content;
        [SerializeField] ZoomController _zoomController;
        [SerializeField] Scrollbar _scrollBar;
        #endregion

        public void ReadyStartAndEnd()
        {
            _nowStart = _originStart;
            _nowEnd = _originalEnd;
            _nowSoundWaveLength = _originalSoundWaveLength;
        }


        // Start is called before the first frame update
        void Start()
        {
            _zoomController.ZoomLevel
            .Subscribe(zl =>{
                //Content引き伸ばす
                _nowSoundWaveLength = _originalSoundWaveLength * zl;
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(_nowSoundWaveLength, tate);

                Changed();
            })
            .AddTo(this);

            _scrollBar.OnValueChangedAsObservable()
            .Subscribe(_ => Changed())
            .AddTo(this);
        }

        private void Changed(){
            //倍率によってずれる大きさ
            var zoomLevel = _zoomController.ZoomLevel.Value -1;
            var zoomIncrement =  _originalSoundWaveLength* zoomLevel;
            var sliderIncrement = -1 * zoomIncrement * _scrollBar.value;

            //倍率とスライダーに合わせてstartとendを変更してクリックの場所を特定出来るようにする
            _nowStart = _originStart + sliderIncrement;
            _nowEnd = (_originalEnd + zoomIncrement) + sliderIncrement;
        }
    }
}
