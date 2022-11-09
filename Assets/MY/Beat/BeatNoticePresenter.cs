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
        [SerializeField] AudioControl _audioControl;

        [SerializeField] BeatNoticeView view;
        [SerializeField] Music _music;
        [SerializeField] AudioSource _audioSource;


        void Start(){
            _audioImport.OnSelectMusic
            .Subscribe(_ => view.DeleteNotice())
            .AddTo(this);

            this.UpdateAsObservable()
            .Where(_ => _musicEngine.isPlaying)
            .Subscribe(_ => view.PlayBeatNotice())
            .AddTo(this);

            _audioControl.OnSeek
            // .Where(_ => _musicEngine.timeSamples < _music.EntryPointSample)
            .Where(_ => !Music.Just.IsNull())
            .Subscribe(_ => view.DeleteNotice())
            .AddTo(this);
        }


    void Update(){
            if(_audioSource.clip == null) return;

            if(Music.Just.IsNull())   view.DeleteNotice();
            
        }
    }
}

