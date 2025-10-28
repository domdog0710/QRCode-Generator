using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing.QrCode;
using ZXing;
using UnityEngine.UI;

public class QRCodeGenerator : MonoBehaviour
{
    [Space]
    [Header("Background Color")]
    [SerializeField]
    Color32 BackgroundColor;

    [Space]
    [Header("Foreground Color")]
    [SerializeField]
    Color32 ForegroundColor;

    public Texture2D QRCode(string url)
    {
        Texture2D qrcode = new Texture2D(800, 800);//設定QR Code圖片大小;

        Color32[] color32 = useEncode(url, qrcode.width, qrcode.height);//儲存產生的QR Code
        qrcode.SetPixels32(color32);//設定要顯示的圖片像素
        qrcode.Apply();//申請顯示圖片

        //SaveTextureAsPNG(qrcode, Application.streamingAssetsPath + "/Setting Json/1.png");

        return qrcode;
    }

    /// <summary>
    /// 將字串進行編碼動作(字串轉QR Code)，回傳值為Color32[]
    /// </summary>
    /// <param name="textForEncoding">要被轉換成QR Code的字串</param>
    /// <param name="width">QR Code的寬度</param>
    /// <param name="height">QR Code的高度</param>
    /// <returns></returns>
    private Color32[] useEncode(string textForEncoding, int width, int height)
    {
        //開始進行編碼動作
        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,//設定格式為QR Code
            Options = new QrCodeEncodingOptions//設定QR Code圖片寬度和高度
            {
                Height = height,
                Width = width
            },
            Renderer = new Color32Renderer
            {
                Background = BackgroundColor
                //Background = BackgroundColor,
                //Foreground = ForegroundColor
            }
        };
        return writer.Write(textForEncoding);//將字串寫入，同時回傳轉換後的QR Code
    }

    public void SaveTextureAsPNG(Texture2D _texture, string _fullPath)
    {
        byte[] _bytes = _texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(_fullPath, _bytes);
        //Debug.Log(_bytes.Length / 1024 + "Kb was saved as: " + _fullPath);
    }

}