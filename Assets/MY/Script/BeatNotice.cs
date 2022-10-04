using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken{
    public class BeatNotice : MonoBehaviour
    {
        [SerializeField] AudioSource _musicEngine;
        [SerializeField] Ken.Setting.AudioImport _audioImport;
        [SerializeField] AudioControll _audioControl;
        [SerializeField] Music _music;


        public Image[] _beatNoticeImage = new Image[8];

        Color _onColor = new  Color32 (255, 255, 255, 255);
        Color _offColor = new Color32 (100 ,100 ,100, 150);

        void Start(){
            _audioImport.OnSelectMusic
            .Subscribe(_ => DeleteNotice())
            .AddTo(this);

            this.UpdateAsObservable()
            .Where(_ => _musicEngine.isPlaying)
            .Subscribe(_ => PlayBeatNotice())
            .AddTo(this);

            _audioControl.OnSeek
            .Subscribe(_ => FixBeatNotice())
            .AddTo(this);
        }

        public void FixBeatNotice(){
            DeleteNotice();

            //delayチェック
            if(_musicEngine.timeSamples < _music.EntryPointSample)  return;

            int i = Music.Just.Beat * 2 + Music.GetUnit % 2;
            _beatNoticeImage[i].color =  _onColor;
            // Debug.Log(Music.Just.Beat +":"+ Music.GetUnit % 2);
        }

        public void DeleteNotice(){
            for(int i=0;i<_beatNoticeImage.Length;i++){
                _beatNoticeImage[i].color =_offColor;
            }
        }

        void PlayBeatNotice(){
            if(Music.Just.Beat == 0){
                _beatNoticeImage[0].color = Music.GetUnit == 0 ? _onColor:_offColor;
                _beatNoticeImage[1].color = Music.GetUnit == 2 ? _onColor:_offColor;
            }else if(Music.Just.Beat == 1){
                _beatNoticeImage[2].color = Music.GetUnit == 0 ? _onColor:_offColor;
                _beatNoticeImage[3].color = Music.GetUnit == 2 ? _onColor:_offColor;
            }else if(Music.Just.Beat == 2){
                _beatNoticeImage[4].color = Music.GetUnit == 0 ? _onColor:_offColor;
                _beatNoticeImage[5].color = Music.GetUnit == 2 ? _onColor:_offColor;
            }else if(Music.Just.Beat == 3){
                _beatNoticeImage[6].color = Music.GetUnit == 0 ? _onColor:_offColor;
                _beatNoticeImage[7].color = Music.GetUnit == 2 ? _onColor:_offColor;
            }
        }
    }
}

