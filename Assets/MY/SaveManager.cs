using System.IO;
using UnityEngine;
using Ken.Delay;

public class SaveManager : MonoBehaviour 
{
    // public SaveTest data;     // json変換するデータのクラス
    [SerializeField] CountPresenter count;
    [SerializeField] DelaySliderManager manager;
    string filepath;                            // jsonファイルのパス
    string fileName = "Data.json";              // jsonファイル名

    //-------------------------------------------------------------------
    //開始時にファイルチェック、読み込み
    void Start(){
        // パス名取得
        filepath = Application.dataPath + "/" + fileName;    
    }
    //-------------------------------------------------------------------
    // jsonとしてデータを保存
    public void Save()
    {
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
        //ファイルがあるならやる
        if (!File.Exists(filepath)) return;

        StreamReader rd = new StreamReader(filepath);               // ファイル読み込み指定
        string json = rd.ReadToEnd();                           // ファイル内容全て読み込む
        rd.Close();                                             // ファイル閉じる
                                                                
        var data = JsonUtility.FromJson<DelayData>(json);            // jsonファイルを型に戻して返す

        manager.DeCreateDelayTimeData(data);
        count.SetDelayData(data);
    }
}
