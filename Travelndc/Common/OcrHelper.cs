using OpenCvSharp;
using Sdcb.PaddleInference;
using Sdcb.PaddleOCR.Models.LocalV3;
using Sdcb.PaddleOCR.Models;
using Sdcb.PaddleOCR;

namespace Travelndc.Common
{
    public class OcrHelper
    {
        /// <summary>
        /// OCR获取验证码
        /// </summary>
        /// <param name="sampleImageData"></param>
        /// <returns></returns>
        public static string GetCode(byte[] sampleImageData)
        {
            FullOcrModel model = LocalFullModels.ChineseV3;
            string authCode = "";
            using (PaddleOcrAll all = new PaddleOcrAll(model, PaddleDevice.Mkldnn())
            {
                AllowRotateDetection = true, /* 允许识别有角度的文字 */
                Enable180Classification = false, /* 允许识别旋转角度大于90度的文字 */
            })
            {
                using (Mat src = Cv2.ImDecode(sampleImageData, ImreadModes.Color))
                {
                    PaddleOcrResult result = all.Run(src);
                    authCode = result.Text;
                }
            }
            return authCode;
        }
    }
}
