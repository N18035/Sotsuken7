using UnityEngine;

namespace Ken.Main.SeekBar
{
    public class Zahyou : MonoBehaviour
    {
        //外部
        [SerializeField] RectTransform canvasRect;
        [SerializeField] Content _contentZoom;


        public void CalcNowMusicTime(Vector3 mousePos ,out float now){
            //引数はスクリーン座標

            //キャンバスと画面サイズの倍率を取る
            var magnification = canvasRect.sizeDelta.x / Screen.width;

            //スクリーン座標は画面左はしが0,0でCanvasは中心が0,0なのでこの差を解消する
            // 倍率をかけてキャンバス座標にして、起点を揃えた部分がこれ。
            mousePos.x = mousePos.x * magnification - canvasRect.sizeDelta.x / 2;
            mousePos.y = mousePos.y * magnification - canvasRect.sizeDelta.y / 2;
            mousePos.z = transform.localPosition.z;

            //(マウスの場所)/(全体)) = 楽曲のパーセント
            now = ((mousePos.x - _contentZoom.NowStart) / (_contentZoom.NowEnd - _contentZoom.NowStart));
        }
    }
}
