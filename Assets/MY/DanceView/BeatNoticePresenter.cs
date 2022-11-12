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
        [SerializeField] Music _music;
        [SerializeField] AudioSource _audioSource;


        void Start(){
            _audioImport.OnSelectMusic
            .Subscribe(_ => view.DeleteNotice())
            .AddTo(this);

            this.UpdateAsObservable()
            .Where(_ => _audioSource.isPlaying)
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

