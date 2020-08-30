using System;
using System.Drawing;
using ThoughtWorks.QRCode.Codec;
using System.Text;

namespace initView.Properties
{
    class qrCoder
    {
        /**
         * 将json做成bitmap类型二维码
         * 返回：从json解析的qrcode
         */
        public Bitmap createCode(String data_Param)
        {
            QRCodeEncoder endocder = new QRCodeEncoder();
            //二维码背景颜色
            endocder.QRCodeBackgroundColor = System.Drawing.Color.White;
            //二维码编码方式
            endocder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //每个小方格的宽度
            endocder.QRCodeScale = 10;
            //二维码版本号
            endocder.QRCodeVersion = 5;
            //纠错等级
            endocder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //将json做成二维码
            //Bitmap bitmap = endocder.Encode(new JavaScriptSerializer().Serialize(data_Param));
            QRCodeEncoder qRCodeEncoder = new QRCodeEncoder();
            var qrcode = qRCodeEncoder.Encode(data_Param, Encoding.UTF8);

            return qrcode;
        }
    }
}