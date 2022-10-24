using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ken
{
    public class AudioCheck : Singleton<AudioCheck>
    {
        [SerializeField] AudioSource _audio;

        public bool IsPlaying(){
            return _audio.isPlaying;
        }
        public bool IsNull(){
            if(_audio.clip == null) return true;
            return false;
        }

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
}

