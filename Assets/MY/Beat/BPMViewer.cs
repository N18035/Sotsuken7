using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Ken.Setting;

namespace Ken.Beat{
    public class BPMViewer : MonoBehaviour
    {
        [SerializeField] Text text;
        [SerializeField] BPMSetting _bpmSetting;
    // Start is called before the first frame update
        void Start()
        {
            _bpmSetting.BPM
            .Subscribe(b => text.text = b.ToString())
            .AddTo(this);
        }
    }
}

