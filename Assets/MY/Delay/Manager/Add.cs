using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ken.Delay
{
    public class Add : MonoBehaviour
    {
        [SerializeField] GameObject SliderPrefab;

        public void Instant(){
            var t = Instantiate(SliderPrefab,this.transform.position,Quaternion.identity);
            t.transform.SetParent(this.gameObject.transform,false);
            t.transform.localPosition = new Vector3(-3f,0,0);
        }
    }
}

