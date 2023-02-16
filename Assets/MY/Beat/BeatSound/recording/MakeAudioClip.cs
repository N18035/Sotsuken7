using System;
using UnityEngine;
using System.Windows.Forms; //OpenFileDialog用に使う
using System.IO;

public class MakeAudioClip : MonoBehaviour
{
    public AudioSource mainBGM;
    public AudioSource beat;
    int frequency = 44100; //サンプリング周波数
    int BPM = 160;

    public void Make()
    {
        if(mainBGM==null || beat.clip == null)   return;

        float[] firstClipData = MakeSilence(10);

        //もし、他にもデータがあるなら実行
        float[] combinedClipData = firstClipData;

        for(int i=0;i<4;i++){
            if(!GetClipList(i,out var data)){
                Debug.Log("終了");
                break;
            }
            Debug.Log(i);
            combinedClipData = ConcatenateArrays(combinedClipData,data);
        }

        var clip = CombineClip(combinedClipData);

        //生成したのをファイルとして保存
        Save(clip);


        
        // float duration = mainBGM.clip.length;

        // int frequency = 44100; //サンプリング周波数

        // int sampleCount = (int)(frequency * duration); //音声データのサンプル数

        // float[] samples = new float[sampleCount]; //音声データ用の配列
        
        // beat.clip.GetData(samples, 0); //既存のAudioClipから音声データを取得

        // float beatInterval = 60f / (float)BPM; //1拍の長さ(秒)
        // int beatIntervalSampleCount = (int)(beatInterval * frequency); //1拍の長さのサンプル数

        // for (int i = 0; i < sampleCount; i++) {
        //     //音声データを切り出し
        //     samples[i] = samples[i % beatIntervalSampleCount];
        // }

        // AudioClip clip = AudioClip.Create("ExistingClip", sampleCount, 1, frequency, false);
        // clip.SetData(samples, 0);

    }

    private float[] MakeSilence(int lengthInSeconds){

        int lengthInSamples = lengthInSeconds * AudioSettings.outputSampleRate;
        float[] samples = new float[lengthInSamples];

        return samples;
    }

    //(BPM,曲の長さ)
    private float[] MakeSample(int BPM,float duration) {
        int sampleCount = (int)(frequency * duration); //音声データのサンプル数

        float[] samples = new float[sampleCount]; //音声データ用の配列

        beat.clip.GetData(samples, 0); //既存のAudioClipから音声データを取得

        float beatInterval = 60f / (float)BPM; //1拍の長さ(秒)
        int beatIntervalSampleCount = (int)(beatInterval * frequency); //1拍の長さのサンプル数

        for (int i = 0; i < sampleCount; i++) {
            //音声データを切り出し
            samples[i] = samples[i % beatIntervalSampleCount];
        }

        return samples;
    }

    bool GetClipList(int i,out float[] x){
        float[] secondClipData = MakeSample(BPM,5);
        x = secondClipData;
        
        if(i>=3) return false; 
        else return true;
    }

    private AudioClip CombineClip(float[] combinedClipData){
        AudioClip combinedClip = AudioClip.Create("CombinedClip", combinedClipData.Length, 1, frequency, false);
        combinedClip.SetData(combinedClipData, 0);
        return combinedClip;
    }

    //結合する
    public static float[] ConcatenateArrays(float[] array1, float[] array2)
    {
        int length1 = array1.Length;
        int length2 = array2.Length;
        float[] result = new float[length1 + length2];
        Array.Copy(array1, 0, result, 0, length1);
        Array.Copy(array2, 0, result, length1, length2);
        return result;
    }

    void Save(AudioClip clip){
        // ダイアログボックスの表示()
        SaveFileDialog sfd = new SaveFileDialog();

        sfd.FileName = "ファイル";
        sfd.InitialDirectory = "";
        sfd.Filter = "wavファイル|*.wav";
        sfd.FilterIndex = 2;
        sfd.Title = "保存先のファイルを選択してください";
        sfd.RestoreDirectory = true;//ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする

        if (sfd.ShowDialog() == DialogResult.OK)
        {
            File.WriteAllBytes(sfd.FileName, WavUtility.FromAudioClip(clip));
            SystemSEManager.I.Better();
            MessageBox.Show("新規保存しました"+sfd.FileName, "通知", MessageBoxButtons.OK);
        }
    }

}
