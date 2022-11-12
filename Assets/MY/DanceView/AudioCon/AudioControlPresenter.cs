using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Ken.Setting;

namespace Ken.DanceView
{
    public class AudioControlPresenter : MonoBehaviour
    {
        [SerializeField] Button Restart;
        [SerializeField] Button forward;
        [SerializeField] Button backForward;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioControl _audioControl;

        void Start()
        {
            Restart.onClick.AsObservable()
            .Where(_ => _audioSource.clip != null)
            .Subscribe(_ => _audioControl.ReStart())
            .AddTo(this);

            forward.onClick.AsObservable()
            .Where(_ => _audioSource.clip != null)
            .Subscribe(_ => _audioControl.Forward10())
            .AddTo(this);


            backForward.onClick.AsObservable()
            .Where(_ => _audioSource.clip != null)
            .Subscribe(_ => _audioControl.BackForward10())
            .AddTo(this);
        }
    }
}

