using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.Main.SeekBar
{
    public class SeekBarPresenter : MonoBehaviour
    {
        [SerializeField] Button zahyouButton;
        [SerializeField] Slider _slider;
        [SerializeField] Zahyou zahyou;
        // [SerializeField] SeekBar model;
        [SerializeField] SeekBarView view;

        [SerializeField] AudioSource _musicEngine;
        [SerializeField] AudioControll _audioController;
        [SerializeField] Music _music;

        //曲の長さ(秒)updateで使うからキャッシュ
        float _musicLength;

        void Start()
        {
            this.UpdateAsObservable()
            .Where(_ => _musicEngine.clip == null)
            .Subscribe(_ => _slider.value = 0)
            .AddTo(this);

            //停止、動作時クリック変換
            zahyouButton.onClick.AsObservable()
            .Where(_ => _musicEngine.clip != null)
            .Subscribe(_ =>{ 
                //マウスクリックはスクリーン座標
                zahyou.CalcNowMusicTime(Input.mousePosition);
                _music.LoadTiming();
            })
            .AddTo(this);

            //曲が停止しているときのスライダー操作
            _slider.onValueChanged.AsObservable()
            .Throttle(TimeSpan.FromMilliseconds(100))
            .Where(_ => !_musicEngine.isPlaying && _musicEngine.clip != null)
            .Subscribe(t =>{
                 _audioController.Seek(t/100f);
                 //FIXMEなぜかここじゃないと動かない
                 _music.LoadTiming();
            })
            .AddTo(this);

            //再生中でない時の反映
            _audioController.OnSeek
            .Subscribe(_=> View())
            .AddTo(this);
        }        

        void Update(){
            //楽曲停止時は止めないとスライダー操作が出来ない
            if(_musicEngine.isPlaying){
                // 今の再生時間 / 総再生時間 * 100 = 今全体の何％にいるか
                View();
            }
        }

        private void View(){
            view.MoveSeekBar(_musicEngine.time / _musicLength * 100);
        }

        //疑似Startで使用。初期化
        public void ReadySeekBar(){
            view.Init();

            //AudioClipの長さをキャッシュ
            _musicLength = _musicEngine.clip.length;
        }


    }
}

