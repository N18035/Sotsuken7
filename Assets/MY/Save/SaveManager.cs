using System.IO;
using UnityEngine;
using Ken.Delay;
using System.Text;
using Sirenix.OdinInspector;//SerializedMonoBehaviourを使うのに必要
using System.Windows.Forms; //OpenFileDialog用に使う
using UniRx;
using System;

namespace Ken.Save
{
    public class SaveManager : MonoBehaviour 
    {
        // public SaveTest data;     // json変換するデータのクラス
        [SerializeField] CountPresenter count;
        [SerializeField] DelaySliderManager manager;
        [SerializeField]FileReplacePresenter replace;
        string filepath;                            // jsonファイルのパス
        [ReadOnly] public string fileName = ".json";              // jsonファイル名

        public IReactiveProperty<string> Info => _error;
        private readonly ReactiveProperty<string> _error = new ReactiveProperty<string>();

        public IObservable<Unit> OnLoad => load;
        private readonly Subject<Unit> load = new Subject<Unit>();


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

            //旧バージョン
            filepath = UnityEngine.Application.dataPath + "/" + fileName;
        }

        //-------------------------------------------------------------------
        // jsonとしてデータを保存
        public void Save()
        {
            // Path();
            SaveFile();
            filepath = filepath + "/" + fileName;

            //ファイルがあるならやらない
            if (File.Exists(filepath)){
                _error.Value = "そのファイルは既に存在しています";
                replace.Open();
                return;
            }

            WriteJson();

        }

        // jsonファイル読み込み
        public void Load(out string s)
        {
            LoadFile();
            //ファイルがあるならやる
            if (!File.Exists(filepath)){
                _error.Value = "ファイルが存在しません";
                s="";
                return;
            }

            StreamReader rd = new StreamReader(filepath);               // ファイル読み込み指定
            s = filepath;
            string json = rd.ReadToEnd();                           // ファイル内容全て読み込む
            rd.Close();                                             // ファイル閉じる
                                                                    
            var data = JsonUtility.FromJson<DelayData>(json);            // jsonファイルを型に戻して返す

            Cdata = data;

            var loadComplete =  manager.JsonToDelayTimeData(data);
            if(loadComplete)   _error.Value = "ロード完了";
            else    _error.Value = "一部開始点が読み込めませんでした。ロードしたデータに間違いはありませんか？";

            load.OnNext(Unit.Default);
        }

        public void SetName(string s){
            if(s=="") return;

            StringBuilder sb = new StringBuilder(s);
            sb.Append(".json");
            fileName = sb.ToString();
        }

        public void WriteJson(){
            Debug.Log("セーブ");
            DelayData data = count.GetDelayData();

            string json = JsonUtility.ToJson(data);                 // jsonとして変換
            StreamWriter wr = new StreamWriter(filepath, false);    // ファイル書き込み指定
            wr.WriteLine(json);                                     // json変換した情報を書き込み
            wr.Close();                                             // ファイル閉じる

            _error.Value = "保存しました:"+filepath;
        }


        void SaveFile(){
            string path="";

            // OpenFileDialog open_file_dialog = new OpenFileDialog();
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();

            // ダイアログの説明文を指定する
            fbDialog.Description = "ダイアログの説明文";

            // デフォルトのフォルダを指定する
            // fbDialog.SelectedPath = @"C:";
            fbDialog.SelectedPath = path;

            // 「新しいフォルダーの作成する」ボタンを表示する
            fbDialog.ShowNewFolderButton = true;

            //フォルダを選択するダイアログを表示する
            if (fbDialog.ShowDialog() == DialogResult.OK)
            {
                Debug.Log(fbDialog.SelectedPath);
            }
            else
            {
                Debug.Log("キャンセルされました");
            }
 
            // オブジェクトを破棄する
            fbDialog.Dispose();

            filepath = fbDialog.SelectedPath;
            Debug.Log(filepath);
        }


        void LoadFile(){
            string path="";

            OpenFileDialog open_file_dialog = new OpenFileDialog();

            //InputFieldの初期値を代入しておく(こうするとダイアログがその場所から開く)
            open_file_dialog.FileName = path;

            //jsonファイルを開くことを指定する
            open_file_dialog.Filter = "jsonファイル|*.json";

            //ファイルが実在しない場合は警告を出す(true)、警告を出さない(false)
            open_file_dialog.CheckFileExists = false;

            //ダイアログを開く
            open_file_dialog.ShowDialog();

            path = open_file_dialog.FileName;

            //FIXME正確にはファイルが見つからないかどうかで判定したい
            if(path == "") return;

            filepath = path;
        }
    }
}

