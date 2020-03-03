using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
using System.Web;

/// <summary>
/// 基础配置类
/// </summary>
namespace Com.Alipay
{
    public class Config
    {
        //public static string alipay_public_key = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAkWiOf8M8dz5hdz/lQ4gjLYIixhenkon+u8LuXtOt4zk5vv6L1u69F+N0XK0eGRxKhIbKlimpuQGcjZqC3d3gix9kSXLLOuXefigxMsa/9CycKg1VKHLRmN1eC8T84wWe+GbzMGYFb7J3Br0oNjTqt7hGCTQ/qWc1nCSPWM5MIWQT3YYvbYEFRx44Xa98NmLdl8Vtlk/DI8u5EEo0meCLLWsbo9p0jxwUbhJsJQshTi3Z/IWeOmqlxgnEI54KSJms1tA9WadENwYJMrGl2GW8WgJxKOOFBHJrL3qvZrMm0N6ZOzbBWrM1mMGmFzAn+JG4eI3k5A1U9+cZyDf9x5ED4wIDAQAB";
        //沙盒
        public static string alipay_public_key = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAnke+VwSp5kU5Y6BbyKENluTitj0gF0Scj5CUNb52k9MoXxpuoCt6uZ7GIzvyYEdlP6jkaRRt9f5m3Msvc5TtrAJ6iMCxK5iLDbEy0fur73ZMMLt2JDdUd2p/Z8Y6PpkxdTBloQwf+ABAK2EPAyfq1FqaNW4p04Jn7Okd94dr9DcQof2kTxvWvWUj66MVR46OAfkDSxw6oGdt6nKA4S2ioPmHTJDs/XDvyBiZotRbmW1lN3UEzAejmvhVAPyJLBSxapzo0lKm6drKpCcIIOz/1LcGT0hi21PyEoF8nZERHq7/fiCAcShrjf8mutTsN6+ot3hTqG2DDWdoriEaaFIPXQIDAQAB";
        //开发者私钥
        public static string merchant_private_key = @"MIIEpQIBAAKCAQEAq4nWMtNHPLqRifgH4oSUslwUOoJUY+3aXGPjOGRhyvD4Ps9CFtKC7sBa++Yp35n9oHP7ww6oHVqrnahxVPyJ6cKNH8QeE3k0GxHyJSfY3IuMXyT0thn+mOjuoIaPlooz3J353XSwbd7bW4nd+60lRqftgc7U2fifGunvXv4WJakNPoPN/rcpfqBmaDNp8w8PSYdzaJl/Pk9GPWFUQUTRFOti+pJG8F+DCkIqRUeViv9BlXIv7K7NqWF7Z8/kwKnQhMLE2m+7+ScH/ixt5QkzQ1WxEAGMTU2TBrq4r6IDZ/+uY5tiYUIbJUtGUhYtgEpJLiaTUOYj43H2KN+Q7xBD6wIDAQABAoIBAAszMbgZ0WHmsI/4kNM6YtcOagez8LyhkAtOPky1SSlN+HdpYuBImGHsT0R5L6y4yjWKvRpsP4/DFsdxcwzgfES1/i7fyDLvS/akglHouyETP3QK3qtxIQ1sz//NKVzj4yT5b5IFB2v1DA96/fJryK5HUpuSHJWqxQCl24oxPtLwjhqHx6mP/8j5W3eL6RJxmUw1qlpRkfxLtdiMN7WByH+DwfTQp01d0jEZuMSUqFtOm1rhvpeGSf92U2ICdb/KIM9bwVnG9pXrRy/+Dn+zusHmwzEBtVIyAc/cpLh8vDpNUcKx23xp5Tvf+pWiTEo9DuZ1aHze+u2Vdaf/ebiezqkCgYEA8NTnS8CaTfILtQEFmS1tuIo+KiwaNoY/IjOCnznLF4IDOshKZ0qFVIA4FXq2GZjbSXS6gHL1Bqun6si5tLhBRaI2nn5jG19SnZtvuPbRSIWB0vkZcRt01LL86JYvhfyplGe41C2XMvcwnpGx+Ey2LfV4l45NZN1PGhc+RqW9iXUCgYEAtlerktqj/HSkOLzUKFZucQXTGA82dNHOj9Tb/+OrYYzoqtMPNduCgHJ5Aiq+qEeitAPvS9jq7IR+8ZS++WVTvyUKlPKs0pBD/JY7z1Qma4LtOok9/4901PMX7/q6ievAlwWKPWtIsScmxc7RGP2nTXH5ukrqH33EYNuJ2tKni98CgYEAtTV9U+J4OG4HoQpDzB4CkPh0DgdhMeWRW4qmnmg+CFIrW++ZDF9mM07G5GKQ4nHBjmPmcQAOnhZLIr4l/XUmp96Gente1xnCcBSk2+fbUVYjeTs1iYW/QsvqwJdA6vBSKPhiekRToGWej7HQ16Pg8HPjaSYJ1fsvqdITadUL6IECgYEAkaChKc8uyvCJO7sroe+tkty5A+0aa+Aondy9u7ej52v3Yn7gFf5zUoyaMm3bCWD1A9pkomni7fS/wccvYtVXNkCqG91Yw25uret6ZxJpANJ3DpoPhuyWvYBMrN/NXVuI29ST3IuzabDAGZkcjF4A4SxW6z7rjy0NyuFrTvWQ1HcCgYEA7fxTnjiKJ0IU70ESYrVmYVAr/H2R6XiZe6K6KYjmzkDXtGGSvUIHQ3ca/Lzlr+o637M321mqe8If9LvTNKv7knr02/dGGDubQLx3kB1YNnNifb0y5pBMdDmKtnw9Ec18rZmRZ8/hg7AR7Ei+IJQUKA9LEkrCU6sW0s9suN4Pkk8=";
       
        //开发者公钥
        //public static string merchant_public_key = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAq4nWMtNHPLqRifgH4oSUslwUOoJUY+3aXGPjOGRhyvD4Ps9CFtKC7sBa++Yp35n9oHP7ww6oHVqrnahxVPyJ6cKNH8QeE3k0GxHyJSfY3IuMXyT0thn+mOjuoIaPlooz3J353XSwbd7bW4nd+60lRqftgc7U2fifGunvXv4WJakNPoPN/rcpfqBmaDNp8w8PSYdzaJl/Pk9GPWFUQUTRFOti+pJG8F+DCkIqRUeViv9BlXIv7K7NqWF7Z8/kwKnQhMLE2m+7+ScH/ixt5QkzQ1WxEAGMTU2TBrq4r6IDZ/+uY5tiYUIbJUtGUhYtgEpJLiaTUOYj43H2KN+Q7xBD6wIDAQAB";
        //沙盒
        public static string merchant_public_key = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAq4nWMtNHPLqRifgH4oSUslwUOoJUY+3aXGPjOGRhyvD4Ps9CFtKC7sBa++Yp35n9oHP7ww6oHVqrnahxVPyJ6cKNH8QeE3k0GxHyJSfY3IuMXyT0thn+mOjuoIaPlooz3J353XSwbd7bW4nd+60lRqftgc7U2fifGunvXv4WJakNPoPN/rcpfqBmaDNp8w8PSYdzaJl/Pk9GPWFUQUTRFOti+pJG8F+DCkIqRUeViv9BlXIv7K7NqWF7Z8/kwKnQhMLE2m+7+ScH/ixt5QkzQ1WxEAGMTU2TBrq4r6IDZ/+uY5tiYUIbJUtGUhYtgEpJLiaTUOYj43H2KN+Q7xBD6wIDAQAB";
        
        //应用ID
        public static string appId = "2016101500693357";
        //沙盒：2016101500693357
        //实验室个人账单支付：2021001109698779

        //合作伙伴ID：partnerID
        //public static string pid = "2088812838037592";
        //沙盒
        public static string pid = "2088102179707248";


        //支付宝网关
        //public static string serverUrl = "https://openapi.alipay.com/gateway.do";
        //沙盒
        public static string serverUrl = "https://openapi.alipaydev.com/gateway.do";
        public static string mapiUrl = "https://mapi.alipay.com/gateway.do";
        public static string monitorUrl = "http://mcloudmonitor.com/gateway.do";

        //编码，无需修改
        public static string charset = "utf-8";
        //签名类型，支持RSA2（推荐！）、RSA
        //public static string sign_type = "RSA2";
        public static string sign_type = "RSA2";
        //版本号，无需修改
        public static string version = "1.0";
         

        /// <summary>
        /// 公钥文件类型转换成纯文本类型
        /// </summary>
        /// <returns>过滤后的字符串类型公钥</returns>
        public static string getMerchantPublicKeyStr()
        {
            StreamReader sr = new StreamReader(merchant_public_key);
            string pubkey = sr.ReadToEnd();
            sr.Close();
            if (pubkey != null)
            {
              pubkey=  pubkey.Replace("-----BEGIN PUBLIC KEY-----", "");
              pubkey = pubkey.Replace("-----END PUBLIC KEY-----", "");
              pubkey = pubkey.Replace("\r", "");
              pubkey = pubkey.Replace("\n", "");
            }
            return pubkey;
        }

        /// <summary>
        /// 私钥文件类型转换成纯文本类型
        /// </summary>
        /// <returns>过滤后的字符串类型私钥</returns>
        public static string getMerchantPriveteKeyStr()
        {
            StreamReader sr = new StreamReader(merchant_private_key);
            string pubkey = sr.ReadToEnd();
            sr.Close();
            if (pubkey != null)
            {
                pubkey = pubkey.Replace("-----BEGIN PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("-----END PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("\r", "");
                pubkey = pubkey.Replace("\n", "");
            }
            return pubkey;
        }

    }
}