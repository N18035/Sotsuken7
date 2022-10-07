using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Ken.Delay
{
    public class DelayLifeScope : LifetimeScope
    {
        [SerializeField] private SliderClamp sliderClamp;
        protected override void Configure(IContainerBuilder builder)
        {
            //入れる================
            // インスタンスを注入するクラスを指定する
            builder.RegisterEntryPoint<IClamp>(Lifetime.Singleton);


            //入れられる====================
            // SampleModelのインスタンスをISampleModelの型でDIコンテナに登録する
            // builder.Register<ISampleModel, SampleModel>(Lifetime.Singleton);
            
            // MonoBehaviorを継承しているクラスはこのようにDIコンテナに登録する
            // builder.RegisterComponentInHierarchy<SampleView>(); と記述するとヒエラルキーから探してきてくれる
            builder.RegisterComponent(sliderClamp);
        }
    }
}

