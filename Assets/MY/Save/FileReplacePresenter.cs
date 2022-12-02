using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Ken.Save
{
    public class FileReplacePresenter : MonoBehaviour
    {
        [SerializeField] Button Yes;
        [SerializeField] Button No;
        [SerializeField] FileReplaceView view;
        [SerializeField] SaveManager mangaer;

        void Start()
        {
            Yes.onClick.AsObservable()
            .Subscribe(_ =>{
                mangaer.WriteJson();
                view.Hide();
            })
            .AddTo(this);

            No.onClick.AsObservable()
            .Subscribe(_ => view.Hide())
            .AddTo(this);
        }


        public void Open(){
            view.Show();
        }
    
    }
}

