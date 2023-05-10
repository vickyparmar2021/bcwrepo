using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Decrypt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write(DecryptStringAES("nfGcQ/9vlO/J5gB1DfrrzWuLLzObrLwZUdn0AvcW//0h++sA5rHzhwf9R68I7EWfLikZKZX0OoArNJ9yh87JSarye2LyrdKmi3kvhnMm3qI="));
        //EncryptMethod("Aamod Joshi");

        //Response.Write(Encrypt("Aamod Joshi"));
        //Response.Write("<br>" + Decryptt("QWFtb2QgSm9zaGk="));

        //Encrypt("aamod"); 
    }

    #region

    public static int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    public static byte[] EncryptStringAES(string plainText)
    {
        //var keybytes = Encoding.UTF8.GetBytes("d9t65s8uy2dkw4eh");
        //var iv = Encoding.UTF8.GetBytes("d9t65s8uy2dkw4eh");

        var keybytes = Encoding.UTF8.GetBytes("codastagcoduruco");
        var iv = Encoding.UTF8.GetBytes("codastagcoduruco");

        var plainttext = plainText;
        var encriptedFromJavascript = EncryptStringToBytes(plainttext, keybytes, iv);
        return encriptedFromJavascript;
    }

    private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
        {
            throw new ArgumentNullException("plainText");
        }
        if (key == null || key.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }
        if (iv == null || iv.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }
        byte[] encrypted;
        // Create a RijndaelManaged object
        // with the specified key and IV.
        using (var rijAlg = new RijndaelManaged())
        {
            rijAlg.Mode = CipherMode.CBC;
            rijAlg.Padding = PaddingMode.PKCS7;
            rijAlg.FeedbackSize = 128;

            rijAlg.Key = key;
            rijAlg.IV = iv;

            // Create a decrytor to perform the stream transform.
            var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for encryption.
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    public static string DecryptStringAES(string cipherText)
    {
        //var keybytes = Encoding.UTF8.GetBytes("d9t65s8uy2dkw4eh");
        //var iv = Encoding.UTF8.GetBytes("d9t65s8uy2dkw4eh");

        var keybytes = Encoding.UTF8.GetBytes("codastagcoduruco");
        var iv = Encoding.UTF8.GetBytes("codastagcoduruco");

        var encrypted = Convert.FromBase64String(cipherText);
        var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
        return string.Format(decriptedFromJavascript);
    }

    private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
        {
            throw new ArgumentNullException("cipherText");
        }
        if (key == null || key.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }
        if (iv == null || iv.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an RijndaelManaged object
        // with the specified key and IV.
        using (var rijAlg = new RijndaelManaged())
        {
            //Settings
            rijAlg.Mode = CipherMode.CBC;
            rijAlg.Padding = PaddingMode.PKCS7;
            rijAlg.FeedbackSize = 128;

            rijAlg.Key = key;
            rijAlg.IV = iv;

            // Create a decrytor to perform the stream transform.
            var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
            try
            {
                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {

                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();

                        }

                    }
                }
            }
            catch
            {
                plaintext = "keyError";
            }
        }

        return plaintext;
    }

    public static string Encrypt(string Id)
    {

        return Convert.ToBase64String(Encoding.UTF8.GetBytes(Id));

    }
    #endregion
}