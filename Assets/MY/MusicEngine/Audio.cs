using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : Singleton<Audio>
{
    [SerializeField] AudioSource _audio;

    public bool TryGetAudioTIme(out float time){
        if(_audio.clip == null){
            time = 0;
            return false;
        }else{
            time = _audio.time;
            return true;
        }
        
    }

    public bool TryGetAudioLength(out float time){
        if(_audio.clip == null){
            time = 0;
            return false;
        }else{
            time = _audio.clip.length;
            return true;
        }
        
    }
}
