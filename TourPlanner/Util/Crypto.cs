using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Util
{
    public class Crypto
    {
        public static string decrypt(string key)
        {
            //method returns decrypted license key

            //get the byte code of the string
            byte[] toDecryptArray = Convert.FromBase64String(key);
            byte[] keyArray;
            keyArray = Encoding.Default.GetBytes("D_phmpds_6k8oRPm");

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();

            return System.Text.Encoding.Default.GetString(resultArray);
        }
    }
}
