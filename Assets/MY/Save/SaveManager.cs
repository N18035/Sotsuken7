using System.IO;
using UnityEngine;
using Ken.Delay;
using System.Text;
using Sirenix.OdinInspector;//SerializedMonoBehaviourを使うのに必要

namespace Ken.Save
{
    public class SaveManager : MonoBehaviour 
    {
        // public SaveTest data;     // json変換するデータのクラス
        [SerializeField] CountPresenter count;
        [SerializeField] DelaySliderManager manager;
        string filepath;                            // jsonファイルのパス
        [ReadOnly] public string fileName = "Test.json";              // jsonファイル名


        //確認用
        [SerializeField]DelayData Cdata;

        //-------------------------------------------------------------------
        //開始時にファイルチェック、読み込み
        void Start(){
            Path();
        }

        void Path(){
            // パス名取得
            // Application.dataPath : /Assets = C:\build\My project_Data
            // Application.streamingAssetsPath : /Assets/StreamingAssets
            // Application.persistentDataPath : C:/Users/xxxx/AppData/LocalLow/CompanyName/ProductName

            filepath = Application.dataPath + "/" + fileName;    
        }

        //-------------------------------------------------------------------
        // jsonとしてデータを保存
        public void Save()
        {
            Path();

            //ファイルがあるならやらない
            if (File.Exists(filepath)) return;

            Debug.Log("セーブ");
            DelayData data = count.GetDelayData();

            string json = JsonUtility.ToJson(data);                 // jsonとして変換
            StreamWriter wr = new StreamWriter(filepath, false);    // ファイル書き込み指定
            wr.WriteLine(json);                                     // json変換した情報を書き込み
            wr.Close();                                             // ファイル閉じる
        }

        // jsonファイル読み込み
        public void Load()
        {
            Path();
            //ファイルがあるならやる
            if (!File.Exists(filepath)) return;

            StreamReader rd = new StreamReader(filepath);               // ファイル読み込み指定
            string json = rd.ReadToEnd();                           // ファイル内容全て読み込む
            rd.Close();                                             // ファイル閉じる
                                                                    
            var data = JsonUtility.FromJson<DelayData>(json);            // jsonファイルを型に戻して返す

            Cdata = data;

            manager.JsonToDelayTimeData(data);

            for (int i = 0; i < data.GetCount(); i++)
            {
                manager.ChangeNow(i);
                manager.BPMSet(data.GetBPM(i));
            }
        }

        public void SetName(string s){
            if(s=="") return;

            StringBuilder sb = new StringBuilder(s);
            sb.Append(".json");
            fileName = sb.ToString();
        }
    }
}

