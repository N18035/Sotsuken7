using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ken.DanceView
{
    public class TimeViewer : MonoBehaviour
    {
        [SerializeField] Text _musicTime;
        [SerializeField] Text _audioTime;
        [SerializeField] AudioSource _audioSource;

        void Update(){
            if(_audioSource.clip == null) return;

            _audioTime.text=_audioSource.time.ToString("F2");

            if(Music.Just.IsNull())   _musicTime.text="---";
            else                     _musicTime.text=Music.Just.ToString();
            
        }

    }
}

