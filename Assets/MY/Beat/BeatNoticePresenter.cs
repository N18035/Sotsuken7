using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.Beat{
    public class BeatNoticePresenter : MonoBehaviour
    {
        [SerializeField] AudioSource _musicEngine;
        [SerializeField] Ken.Setting.AudioImport _audioImport;
        [SerializeField] AudioControll _audioControl;

        [SerializeField] BeatNoticeView view; 


        void Start(){
            _audioImport.OnSelectMusic
            .Subscribe(_ => view.DeleteNotice())
            .AddTo(this);

            this.UpdateAsObservable()
            .Where(_ => _musicEngine.isPlaying)
            .Subscribe(_ => view.PlayBeatNotice())
            .AddTo(this);

            _audioControl.OnSeek
            .Subscribe(_ => view.FixBeatNotice())
            .AddTo(this);
        }
    }
}

