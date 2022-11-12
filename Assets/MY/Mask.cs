using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ken.Setting
{
    public class Mask : MonoBehaviour
    {
        [SerializeField] Image mask;
        public void LoadMask(){
            mask.enabled = mask.enabled == true ? false:true;
            // Debug.Log(mask.enabled);
        }
    }
}

