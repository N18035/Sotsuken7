using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ken{
    public class BeatSound : MonoBehaviour
    {
        //タスク
        //音無しの判断が別々でされていて、かつintで行われているから無駄が発生している
        
        private AudioSource _audioSource;
        [SerializeField] AudioSource _musicEngine;

        public AudioClip[] SEClips = new AudioClip[3];

        private int _beatSoundNum;

        void Start(){
            _audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if(_musicEngine.isPlaying){
                PlayBeatSound();
            }
        }
        void PlayBeatSound(){
            if(Music.IsJustChangedBeat()){
                if(_beatSoundNum!=2)    _audioSource.Play();
            }

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

        public void SetBeatSound(int n){
            // Debug.Log("びーとさうんど"+n);
            if(n!=2)    _audioSource.clip =  SEClips[n];
            _beatSoundNum = n;
        } 
    }
}
