using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Ken.Setting
{
    public class AudioImportPresenter : MonoBehaviour
    {
        [SerializeField] Text text;
        [SerializeField] UnityEngine.UI.Button button;
        [SerializeField] AudioImport _audioImport;

        void Start(){
            _audioImport.ClipName
            .Subscribe(n => text.text=n)
            .AddTo(this);

            button.onClick.AsObservable()
            .Subscribe(_ => _audioImport.MusicSelect())
            .AddTo(this);
        }
    }
}

