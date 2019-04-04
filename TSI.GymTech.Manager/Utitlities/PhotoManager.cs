using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Manager.Result;

namespace TSI.GymTech.Manager.Utitlities
{
    public class PhotoManager
    {
        /// <summary>
        /// Create a private string with source path
        /// </summary>
        private string _pathSource;

        /// <summary>
        /// Create a constructor manager method 
        /// </summary>
        /// <param name="source"></param>
        public PhotoManager(string source)
        {
            _pathSource = source;
        }

        /// <summary>
        /// Create method to Capture Photo
        /// </summary>
        /// <param name="base64image"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ResultEnum CapturePhoto(string base64image, string fileName)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                var pictureObj = ConvertBase64ImageToString(base64image);

                if (SavePhotoToDirectory(pictureObj, fileName) == ResultEnum.Error)
                {
                    result = ResultEnum.Error;
                }
            }
            catch (Exception ex)
            {
                result = ResultEnum.Error;
                //Pending: error to the log file
            }

            return result;
        }

        /// <summary>
        /// Create method to Remove Photo
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ResultEnum RemovePhoto(string fileName)
        {
            ResultEnum result = ResultEnum.Success;
            try
            {
                if (DeletePhotoToDirectory(fileName))
                {
                    result = ResultEnum.Success;
                }
                else
                {
                    result = ResultEnum.Error;
                    //Pending: error to the log file
                }
            }
            catch (Exception ex)
            {
                result = ResultEnum.Error;
                //Pending: error to the log file
            }

            return result;
        }

        /// <summary>
        /// Create method to convert Base64 image to string
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        private string ConvertBase64ImageToString(string base64image)
        {
            var pictureObj = string.Empty;

            if (base64image.IndexOf("png;base64") > 0)
            {
                pictureObj = base64image.Replace("data:image/png;base64,", String.Empty);
            }
            else if (base64image.IndexOf("jpg;base64") > 0)
            {
                pictureObj = base64image.Replace("data:image/jpg;base64,", String.Empty);
            }
            else if (base64image.IndexOf("jpeg;base64") > 0)
            {
                pictureObj = base64image.Replace("data:image/jpeg;base64,", String.Empty);
            }
            
            return pictureObj.Replace('-', '+').Replace('_', '/');
        }

        /// <summary>
        /// Create method to save photo from directory
        /// </summary>
        private ResultEnum SavePhotoToDirectory(string pictureObj, string fileName)
        {
            ResultEnum result = ResultEnum.Success;
            var fullPath = _pathSource + fileName;

            try
            {    
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(pictureObj)))
                {
                    using (Bitmap bm1 = new Bitmap(ms))
                    {
                        bm1.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
            }
            catch (Exception ex)
            {
                result = ResultEnum.Error;
                //Pending: error to the log file
            }

            return result;
        }

        /// <summary>
        /// Create method to delete photo from directory
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool DeletePhotoToDirectory(string fileName)
        {
            try
            {
                var fullPath = _pathSource + fileName;

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
