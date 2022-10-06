using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ken.Beat{
    public class BeatSound : MonoBehaviour
    {
        
        private AudioSource _audioSource;
        public AudioClip[] SEClips = new AudioClip[3];
        private int _beatSoundNum;

        void Start(){
            _audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if(!_audioSource.isPlaying) return;

            if(Music.IsJustChangedBeat()){
                if(_beatSoundNum==2) return;    
                _audioSource.Play();
            }
        }

        public void SetBeatSound(int n){
            _audioSource.clip =  SEClips[n];
            _beatSoundNum = n;
        } 
    
        #region 保留
        void PlayBeatSound(){
            // if(type==1){//音の変更アリ
            //     if(Music.IsJustChangedBeat()){
            //         // if(Music.Just.Beat%4==3)    Music.QuantizePlay(Click,1);//ボツ
            //         if(dd3or4.value==1){//3はく
            //             if(Music.Just.Beat%4==2)    Clickupkey.Play();
            //             else    Click.Play();
            //         }else if(dd3or4.value==0){//4はく
            //             if(Music.Just.Beat%4==3)    Clickupkey.Play();
            //             else    Click.Play();
            //         }
            //     }
            // }else if(type==2){//拍手
            //     if(Music.IsJustChangedBeat())   Clap.Play();
            // }else if(type==3){//声 //3はく未対応
            //     if(dd3or4.value==1){//3はく
            //     if(Music.Just.Beat%3==0)   two.Play();
            //     else if(Music.Just.Beat%3==1)   three.Play();
            //     else if(Music.Just.Beat%3==2)   one.Play();
            //     }else if(dd3or4.value==0){//4はく
            //     if(Music.Just.Beat%4==0)   two.Play();
            //     else if(Music.Just.Beat%4==1)   three.Play();
            //     else if(Music.Just.Beat%4==2)   four.Play();
            //     else if(Music.Just.Beat%4==3)   one.Play();
            //     }
            // }else if(type==4){//無音
            //     //無音
            // }else if(type==0){//一定
            //     if(Music.IsJustChangedBeat())   Click.Play();
            // }
        }
    #endregion

    }
}
