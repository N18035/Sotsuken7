using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Ken.Audio{
    public class PlayButtonPresenter : MonoBehaviour
    {
        [SerializeField] Button _button;
        [SerializeField] Image _image;
        [SerializeField] Sprite play;
        [SerializeField] Sprite pause;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioControl _audioControl;
        [SerializeField] Ken.Setting.AudioImport _audioImport;

        void Start(){
            _button.onClick.AsObservable()
            .Where(_ => _audioSource.clip != null)
            .Subscribe(_ => Click())
            .AddTo(this);

            _audioImport.OnSelectMusic
            .Subscribe(_ => _image.sprite = play)
            .AddTo(this);
        }

        void Click(){
            if(_audioSource.isPlaying){
                //停止処理
                _image.sprite = play;
                _audioControl.Pause();
            }
            else{
                //再生処理
                _image.sprite = pause;
                _audioControl.Play();
            } 
        }
    }
}



