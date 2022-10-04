using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ken.Delay;
using UnityEngine.UI;

public class DelayModel : MonoBehaviour
{
    // [SerializeField] bool[] DelayFlag = new bool[5];
    int DelayFlag=0;
    int a=0;
    int b=1;
    int c=2;
    int now=0;

    [SerializeField]bool endFlag=false;
    [SerializeField]int i=0;

    [SerializeField] AudioSource audioSource;
    // [SerializeField] DelaySettings delay;
    [SerializeField]  DelaySliderManager manager;
    [SerializeField] Music _music;

        public void TestPublicDelay(){
            // 一般的には44100
            _music.EntryPointSample = (int)(audioSource.time * audioSource.clip.frequency);
            //TODO UIに指示
            DelayCore.I.GO();
        }

        public void SetDelay(float value){
            // 一般的には44100
            _music.EntryPointSample = (int)(value * audioSource.clip.frequency);
            //TODO UIに指示
            DelayCore.I.GO();
        }

    void Update()
    {
        if(!audioSource.isPlaying) return;

        if(endFlag) return;
        
        // if(audioSource.time >= manager.Sliders[i].GetComponent<Slider>().value)    {
        //     Debug.Log(i);
        //     i++;
        //     TestPublicDelay();

        //     if(i == manager.Sliders.Count) endFlag=true;
        // }

        // if(audioSource.time >= manager.Sliders[now].GetComponent<Slider>().value)    {
        //     Debug.Log("0");
        //     now++;
        //     now = Mathf.Clamp(now, 0, manager.Sliders.Count);
        //     TestPublicDelay();
        // }

        // if(audioSource.time >= manager.Sliders[0].GetComponent<Slider>().value)    {
        //     Debug.Log("0");
        //     DelayFlag++;
        //     TestPublicDelay();
        // }

        // if(audioSource.time >= manager.Sliders[a].GetComponent<Slider>().value && DelayFlag==now)    {
        //     Debug.Log("0");
        //     DelayFlag++;
        //     TestPublicDelay();
        // }
        // else
        // if(audioSource.time >= manager.Sliders[b].GetComponent<Slider>().value && DelayFlag==b)    {
        //     Debug.Log("1");
        //     DelayFlag++;
        //     TestPublicDelay();
        // }
        // else
        // if(audioSource.time >= manager.Sliders[c].GetComponent<Slider>().value && DelayFlag==c)    {
        //     Debug.Log("2");
        //     DelayFlag++;
        // }
    }
}
