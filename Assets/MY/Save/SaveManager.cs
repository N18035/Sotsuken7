using System.IO;
using UnityEngine;
using Ken.Delay;
using System.Text;
using Sirenix.OdinInspector;//SerializedMonoBehaviourを使うのに必要
using System.Windows.Forms; //OpenFileDialog用に使う
using UniRx;
using System;
using System.Collections.Generic;

namespace Ken.Save
{
    public class SaveManager : MonoBehaviour 
    {
        [SerializeField] CountPresenter count;
        [SerializeField] DelaySliderManager manager;

        public IReactiveProperty<string> Info => _error;
        private readonly ReactiveProperty<string> _error = new ReactiveProperty<string>();
        public IObservable<Unit> OnLoad => load;
        private readonly Subject<Unit> load = new Subject<Unit>();

        //確認用
        [SerializeField]DelayData Cdata;
        
        // jsonとしてデータを保存
        public void Save()
        {
            // ダイアログボックスの表示()
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.FileName = "ファイル";
            sfd.InitialDirectory = "";
            sfd.Filter = "jsonファイル|*.json";
            sfd.FilterIndex = 2;
            sfd.Title = "保存先のファイルを選択してください";
            sfd.RestoreDirectory = true;//ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;
                DelayData data = count.GetDelayData();
                string json = JsonUtility.ToJson(data);// jsonとして変換
                
                File.WriteAllText(fileName, json);
            }
        }

        // jsonファイル読み込み
        public void Load()
        {
            OpenFileDialog open_file_dialog = new OpenFileDialog();

            //InputFieldの初期値を代入しておく(こうするとダイアログがその場所から開く)
            open_file_dialog.FileName = "";
            //jsonファイルを開くことを指定する
            open_file_dialog.Filter = "jsonファイル|*.json";
            //ファイルが実在しない場合は警告を出す(true)、警告を出さない(false)
            open_file_dialog.CheckFileExists = true;


            //ダイアログを開いてpathを取得
            open_file_dialog.ShowDialog();
            string path="";
            path = open_file_dialog.FileName;
            if(path == "") return;

            //ここから読み込み
            StreamReader rd = new StreamReader(path);

            string json = rd.ReadToEnd();                           // ファイル内容全て読み込む
            rd.Close();                                             // ファイル閉じる
                                                                    
            var data = JsonUtility.FromJson<DelayData>(json);            // jsonファイルを型に戻して返す
            Cdata = data;

            var loadComplete =  manager.JsonToDelayTimeData(data);
            if(loadComplete)   _error.Value = "ロード完了";
            else    _error.Value = "一部開始点が読み込めませんでした。ロードしたデータに間違いはありませんか？";

            load.OnNext(Unit.Default);
        }
    }
}

