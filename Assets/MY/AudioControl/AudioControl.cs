using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

namespace Ken
{
    public class AudioControl : MonoBehaviour
    {
        [SerializeField] Music _music;
        [SerializeField] AudioSource _audioSource;

        public IObservable<Unit> OnSeek =>_change;
        private Subject<Unit> _change = new Subject<Unit>();

        public IObservable<Unit> OnPlayStart =>_play;
        private Subject<Unit> _play = new Subject<Unit>();

        private int loopStart=0;
        private int loopEnd=0;
        [SerializeField]private bool loopFlag=false;

        void Start(){
            this.UpdateAsObservable()
            .Where(_ => _audioSource.time > (float)loopEnd && loopFlag)
            .Subscribe(_ =>{                 
                _audioSource.time = loopStart;
                _change.OnNext(Unit.Default);
            })
            .AddTo(this);
        }

        public void Play(){
            if(_audioSource.clip == null) return;

            //FIXME 色々残ってる
            //audioのplayから直で呼べないから仮でこうしてる
            _music.Play("musicengine","");
        }
        public void Pause(){
            if(_audioSource.clip == null) return;
            Music.Pause();
        }
        public void ReStart(){
            if(_audioSource.clip == null) return;
            _audioSource.time = 0f;
            _change.OnNext(Unit.Default);
        } 
        public void Forward10(){
            if(_audioSource.clip == null) return;
            if(_audioSource.time + 10f > _audioSource.clip.length) return;
            _audioSource.time += 10f;
            _change.OnNext(Unit.Default);
        }

        public void BackForward10(){
            if(_audioSource.clip == null) return;
            if(_audioSource.time - 10f < 0) return;
            _audioSource.time -= 10f;
            _change.OnNext(Unit.Default);
        }
        public void Seek(float lengthPar){
            if(_audioSource.clip == null) return;

            _audioSource.time = lengthPar * _audioSource.clip.length;
            _change.OnNext(Unit.Default);
        }

        public void Loop(int start,int end){
            loopStart = start;
            Mathf.Clamp(end,1,_audioSource.clip.length);
            loopEnd = end;
            
            if(loopFlag)    loopFlag = false;
            else loopFlag = true;
        }

        public void ChangeSpeed(float v){
            //ピッチ変えるだけならnullチェック不要
            _audioSource.pitch = v;
        }

        public void ReadyAudioTime(){
            _audioSource.time = 0f;
        }
    }
}
