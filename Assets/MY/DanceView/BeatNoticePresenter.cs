using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.DanceView{
    public class BeatNoticePresenter : MonoBehaviour
    {
        [SerializeField] AudioImport _audioImport;
        [SerializeField] AudioControl _audioControl;

        [SerializeField] BeatNoticeView view;
        [SerializeField] AudioSource _audio;

        void Start(){
            _audioImport.OnSelectMusic
            .Subscribe(_ => view.DeleteNotice())
            .AddTo(this);

            this.UpdateAsObservable()
            .Where(_ => _audio.isPlaying)
            .Subscribe(_ => Check())
            .AddTo(this);

            _audioControl.OnSeek
            .Subscribe(_ => view.DeleteNotice())
            .AddTo(this);
        }

        void Check(){
            if(Music.Just.IsNull()) view.DeleteNotice();
            else view.PlayBeatNotice();
        }
    }
}

