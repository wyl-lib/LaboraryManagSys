using initView.Properties;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Input;
using static initView.Properties.funcSelectForm;
using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using LitJson;

namespace initView
{
    /// <summary>
    /// clearBill.xaml 的交互逻辑
    /// </summary>
    public partial class clearBill : Window
    {
        Config config = new Config();
        qrCoder qRCoder = new qrCoder();

        static borrowForm borrowForm;
        static feedBack feedBack;
        static Project proJect;
        static Warehouse wareHouse;
        public static DataSet getRecord { get; set; }
        ObservableCollection<kc_billStatistics> userBorrowList = new ObservableCollection<kc_billStatistics>();

        /*
         * 鼠标操作，拖动窗口
         */
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // 获取鼠标相对标题栏位置
            System.Windows.Point position = e.GetPosition(clearPayment);

            // 如果鼠标位置在标题栏内，允许拖动
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < clearPayment.ActualWidth && position.Y >= 0 && position.Y < clearPayment.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }

        public clearBill()
        {
            InitializeComponent();
        }

        public String SacnQrcode()
        {
            int uID = int.Parse(getRecord.Tables[0].Rows[0][0].ToString());
            String out_trade_no = Config.randomCode(uID + "");
            String money = funcSelectForm.mToltal + "";
            String json_QR = config.precreate(out_trade_no, money);
            //config.query();

            /**
             * Bitmap-->BitmapImage-->imageSource
             */
            Bitmap b = new Bitmap(qRCoder.createCode(json_QR));
            MemoryStream ms = new MemoryStream();
            b.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] bytes = ms.GetBuffer();  //byte[]   bytes=   ms.ToArray(); 这两句都可以
            ms.Close();
            //Convert it to BitmapImage
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new MemoryStream(bytes);
            image.EndInit();
            QRcodeImg.Source = image;
            /*如果无需还钱打开生成文字二维码，娱乐。*/
            /*！！！！最关键的是还完钱数据库进行更新，（可选：邮箱通告）*/

            return out_trade_no;
        }


        /**
         * 在支付完成后单击进行支付信息核实
         * 如果状态正常则更新数据库
         */
        private void payFinished_Click(object sender, RoutedEventArgs e)
        {
            String out_trade_no = SacnQrcode();
            string message = MessageBox.Show("总金额为" + mToltal + "请确认!", "操作提示", MessageBoxButton.OKCancel, MessageBoxImage.Information).ToString().Trim();
            if (message.Equals("Cancel"))
            {
                return;
            }
            else
            {
                config.queryTrade_status(out_trade_no);
            }
        }


        public class Config
        {
            private IAopClient alipayClient;

            public Config()
            {
                alipayClient = new DefaultAopClient(
                    serverUrl,
                    appId,
                    merchant_private_key,
                    format,
                    version,
                    sign_type,
                    alipay_public_key
                    );
            }

            /**
             * Method:统一下单并支付界面接口
             * Return:返回json格式的付款二维码
             * Param:订单号,支付金额
             */
            public String precreate(String out_trade_no, String money)
            {
                try
                {
                    //IAopClient client = new DefaultAopClient(serverUrl, appId, merchant_private_key, format, version, sign_type, alipay_public_key, charset, false);
                    AlipayTradePrecreateRequest request = new AlipayTradePrecreateRequest();
                    request.BizContent = "{" +
                   "\"out_trade_no\":\"" + out_trade_no + "\"," +
                    //"\"seller_id\":\"2088102146225135\"," +
                    "\"total_amount\":" + money + "," +
                    "\"subject\":\"实验室个人账单结算请求\"" +
                    "  }";
                    AlipayTradePrecreateResponse response = alipayClient.Execute(request);
                    JsonData jsonData = JsonMapper.ToObject(response.Body);//转化为JSON数据
                    if (!response.IsError)
                    {
                        Console.WriteLine("Finished!");
                    }

                    JsonData alipayData = jsonData["alipay_trade_precreate_response"];
                    String qr_code = alipayData["qr_code"].ToString();

                    return qr_code;
                }
                catch
                {
                    MessageBox.Show("异常捕获!");
                    return "网络状态异常";
                }


            }

            /**
             * Method:查询交易状态
             * 响应参数为,trade_status:
             *      WAIT_BUYER_PAY（交易创建，等待买家付款）、TRADE_CLOSED（未付款交易超时关闭，或支付完成后全额退款）、
             *      TRADE_SUCCESS（交易支付成功）、TRADE_FINISHED（交易结束，不可退款） 
             * total_amount(交易金额)
             * 
             * 
             */
            public void queryTrade_status(String out_trade_no)
            {
                IAopClient client = new DefaultAopClient(serverUrl, appId, merchant_private_key, format, version, sign_type, alipay_public_key, charset, false);
                AlipayTradeQueryRequest request = new AlipayTradeQueryRequest();
                request.BizContent = "{" +
                "\"out_trade_no\":\"" + out_trade_no + "\"," +
                "\"org_pid\":\"2088101117952222\"," +
                "      \"query_options\":[" +
                "        \"trade_settle_info\"" +
                "      ]" +
                "  }";
                AlipayTradeQueryResponse response = client.Execute(request);
                Console.WriteLine(response.Body);
            }

            public static String randomCode(String userID)
            {
                var time = DateTime.Now.ToString("yyyyMMddHHmmss");
                String code = time + userID + new Random().Next(100000000, 999999999).ToString();

                return code;
            }

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
                    pubkey = pubkey.Replace("-----BEGIN PUBLIC KEY-----", "");
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


            /*
              沙盒环境配置
                
            public static string appId = "2016101500693357";
            public static string pid = "2088102179707248";
            public static string merchant_private_key = @"MIIEowIBAAKCAQEAo0xY3eH+JsIiucY7iby3RhR0MgjOpe8VHZsYAfOTkKLIhAtFFn830JGN5opch+C8e0q9b4cLYkoTQbQx271qiXBArIQzfCB5zgm88SyL69SSIY/rXZg4PHFXr9IoVZhJkkNGJdlBMg+KIhl7KTaOoRFQBI1ITcLp456YiV+OTXjol1EmZJVJr9fEYRQecygA/4JOa7FF7ecE7RC4yZlOKghH9KpyF5Bz+RAuao6IAawzbVaCb19rWZfSzkyn0/wrDo/fqjoTwSiSoVzSsijlQxgFeeXVdPXGFzOb6GdM0VtW/a42SzdU0pHvPKAm/EZ9zubpbIxHy+156aXa2AGKJQIDAQABAoIBAHgZhqJpu8o1reSD7vX2XbSlBnBmGdXgaN9FWfrVcgpGLsMuprlNB3fWFU8hI7yrhPQInBqSb8TyRgdkx+adAOXkMSywbk00dWEbuGKIKMBhrrgQaUKE2ZdapOsi3ZdWUYXJBKaBinmPBEDgDLza6zt6yCcQ4cSPPgWydu1R4g8HV6Tv5QFZSKpt/5D4pJ6YhsUhfpqKrFlPdgtmomqeLlqUkQIpiFukqUu/J65yhkaQND1Orpmetsyl/KcE3evNx8mTPB8jw6tnFYlkgvj9NOSUOqI6zGsRbDiEqrxiqFWTvAaTR4uKP+cDdEkxpmzsn9VzIv7xqRH9HKyQ/GJkKWECgYEA4RZZCTuw05z6J5shJRn15PMLTAjiymRire2dVzvWvB2SKrSguXN7eUPuAnXvcGceoi8D5dWH7MU8vDpssomkM4At926QXTVBhgmUenT6wj9i+xsO6W9FZ7Wf6bHVx3RqufgpwVa0RrH6WJoSXKu3RsvjAt5k9HaXBpD4I0bxPzsCgYEAubmcPbM/a7qfLz2OafMxfHhYOhxk/Jl5CoBW4dLCkLo6e3TWzifbrVTvQDxf7XK/kBEkBbf7LvqEl/l1n3UyLLbRB57KtmxnMWLqJKt7eqk9phpnAu96c/+BlAo5HhHL77GwZq9UTF4vLIOBQgP6F/DbPFSu0ypOLBUXCzX2hh8CgYAby6OxFTLv3tmxXGHKcBtCa5UAjbRb4+ufqNEePaB12Hz9UxLp883SmsXgfenUipdo1b94eVbwsSaf4+MaZQ7m4TRL8tZW/EWcJMC2Io34wNUbRysfPoNmnDIkLLbGi6TGVFPrsSZP+Jh+qoAL+5SFZJYi+42h9qcNw4cw7AjMkwKBgQCJN5076McerM8Xc/9YQBl7wN5OcNQb3LgVDiUiyzh8/VpgBD8AKWqgcXQinXiyUmk946wdnYSzfNuh/OjuWwQrDRb67lMO3KCRWGdtmFSul4O4ySD6hjyOn2P5IGR61uM5wKp382nw0lVAKhjB0XZhYc5YkKhp04PWBe70lXYCOQKBgAN1TIinK53kN5LXijNwb+TET+E1T5PcvDJ6Awq0m39a3SSxn8KvHXY5P+wGhRVOpgLWXR7rmQ0imM53LixuFpal6y1Xo5v0C37yze0IHM5bSTAhBGNEYR7i0nhBVhW8Yt72lq6IA30td8BfGgN7Mi3IGciiifwj8i3aLYwHtLXb";
            public static string merchant_public_key = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAo0xY3eH+JsIiucY7iby3RhR0MgjOpe8VHZsYAfOTkKLIhAtFFn830JGN5opch+C8e0q9b4cLYkoTQbQx271qiXBArIQzfCB5zgm88SyL69SSIY/rXZg4PHFXr9IoVZhJkkNGJdlBMg+KIhl7KTaOoRFQBI1ITcLp456YiV+OTXjol1EmZJVJr9fEYRQecygA/4JOa7FF7ecE7RC4yZlOKghH9KpyF5Bz+RAuao6IAawzbVaCb19rWZfSzkyn0/wrDo/fqjoTwSiSoVzSsijlQxgFeeXVdPXGFzOb6GdM0VtW/a42SzdU0pHvPKAm/EZ9zubpbIxHy+156aXa2AGKJQIDAQAB";
            public static string serverUrl = "https://openapi.alipaydev.com/gateway.do";
            public static string mapiUrl = "https://mapi.alipay.com/gateway.do";
            public static string monitorUrl = "http://mcloudmonitor.com/gateway.do";
            */
            public static string alipay_public_key = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAkWiOf8M8dz5hdz/lQ4gjLYIixhenkon+u8LuXtOt4zk5vv6L1u69F+N0XK0eGRxKhIbKlimpuQGcjZqC3d3gix9kSXLLOuXefigxMsa/9CycKg1VKHLRmN1eC8T84wWe+GbzMGYFb7J3Br0oNjTqt7hGCTQ/qWc1nCSPWM5MIWQT3YYvbYEFRx44Xa98NmLdl8Vtlk/DI8u5EEo0meCLLWsbo9p0jxwUbhJsJQshTi3Z/IWeOmqlxgnEI54KSJms1tA9WadENwYJMrGl2GW8WgJxKOOFBHJrL3qvZrMm0N6ZOzbBWrM1mMGmFzAn+JG4eI3k5A1U9+cZyDf9x5ED4wIDAQAB";
            /*
              sh开发者私、公钥
                */
            public static string appId = "2021001109698779";
            public static string pid = "2088812838037592";    //合作伙伴ID
            public static string serverUrl = "https://openapi.alipay.com/gateway.do";     //支付宝网关
            //sys开发者私、公钥    
            public static string merchant_private_key = @"MIIEowIBAAKCAQEAs1lN1hpU2UPAJdr63dSqQOz9PoDr8kW2TWKCUf1yBLMT9DgZhGysG1N14T4moaw6EakhAGHTn3EjgwstxhH7tP0nbtOJ1ySnNCiD8U03RmSsfgaOXwxZHWRS/2ogAY/f/qzVp4mCn1WfWOTePOwx6xEZzS2Bo43yKpu1g3dHSuSGiM4ttqfv4AeT9luEsL3ne2ZuOA21B2CSoWrjDCB3PWfRb9R53gV+Zm7N0ZFYgZInGp5i5NWdqTDqAqyWFcAFA7UfL3Pqp+JyZQRPfKDd2Co7j4H5b0yr1sMptzJ08v2uy0I15itvaZfCQ0hxbJkJeKp4M5j2pABVkRQEThPPmwIDAQABAoIBAEKMeqNddeqAh0YEsV2q0dnxli6hK+vkDjc8vFqsDqjLwjW2s1E9+cbR+0WvCCqpe4qeFU7M/feFxcWUWlHKznlkDkPDXrxkL17dQchB/Ka0KIVC7YU7Ub3kQh/gQywRNO6NN2JJVhzFarFTi1tT0VoGKGrH3fjQbPF+gaXclqmk7tkfanlSX0zLxLWiw3rILpMVqKQHhpKkkHljM1jt8BDE+RdL1VdYzENVjn5nOdETpy/IbFYir28NvA3jOKHUNfoqHwlM3FW5geP4QE809UjjO3OFljnGkUQ9zndKsW0lTZWgYL9Oe9k5NJXfbJviH9hiUuSqRJ1tm8Yq9QW/07ECgYEA6jHve04Mttlom72C/RUoUzyYcBFUDy8VnkU+FX55KQZgrhs7nziSRnOF+TaKTxqnc2/MlZIWI990texfiwU0co7jKANvII4UVexkTBBTbqu05t4zsZ1iegvpT8leX6QAfhofcTM27e9LRxbbr0y+F2xNB+GwHzAFKjDI0uXpAXcCgYEAxAwaY+kCC7RfEygnvtvzd55+iVhOdCeifJ/Y310sQNvqKy5PfnU2i0J0bDftdA9ros8EHucrovO58GFYqOXsG7909KwtfcsrMlTEmliwAXCgfRvqBbdLM7iLHx+GjbL38KKA4J7V0UF7pRo72Ayf+Z9dYhkM77X79PP2GYt8y/0CgYBHs6wvo1DPNrcJX7dYBv9GpLnDLfr/fxnuUCLW79bmkZZOF/ZkJCzctwfgqiskVvPHqwYHESgIKaURAeKqrJhHLqormQmP/RAK0opOo3z32EhVmthW2+nHlhgDnpJvU8X21cakoNqDdI0mob/25tcYUscuz5FdqxUZur/xmgSBtwKBgCnzjRIkIlPu3Ql0ZkzpNEg8mkc5plAkyxhv/DCP95BJfuqOlmQxIOLb7Z3aue1D/3xvYCpcvXG5ReiybA54ZPXrGlPhJDru5ud2TDvbDo7AUxdyibl/Z5BBxUN7s4dO7M+JdpqaYpbMtHC4FGY/DVyVRZ5gTexdvPTF37vHpYThAoGBALQtZj/OsI8qZGX2gXPWO3Xtf6i2vlrYQR+TumM5GjUiUctnKTtMKlJqEbW77QuFu8L1DCYrJlBYm0NxNiMPX3bbhBj4Q0FYsrONlb1oE7LxqP+z8/WCJ36/yYFFNSOZfeubQzse586e4bhoxcSui4ioPsfAuivni/vPZxgInZzC";
            public static string merchant_public_key = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAs1lN1hpU2UPAJdr63dSqQOz9PoDr8kW2TWKCUf1yBLMT9DgZhGysG1N14T4moaw6EakhAGHTn3EjgwstxhH7tP0nbtOJ1ySnNCiD8U03RmSsfgaOXwxZHWRS/2ogAY/f/qzVp4mCn1WfWOTePOwx6xEZzS2Bo43yKpu1g3dHSuSGiM4ttqfv4AeT9luEsL3ne2ZuOA21B2CSoWrjDCB3PWfRb9R53gV+Zm7N0ZFYgZInGp5i5NWdqTDqAqyWFcAFA7UfL3Pqp+JyZQRPfKDd2Co7j4H5b0yr1sMptzJ08v2uy0I15itvaZfCQ0hxbJkJeKp4M5j2pABVkRQEThPPmwIDAQAB";

            //编码，无需修改
            public static string charset = "UTF-8";
            //签名类型，支持RSA2
            public static string sign_type = "RSA2";
            //版本号，无需修改
            public static string version = "1.0";
            public static string format = "JSON";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageBox.Show("请再次确认!", "操作提示", MessageBoxButton.OKCancel, MessageBoxImage.Information).ToString().Trim();
            if (message.Equals("Cancel"))
            {
                return;
            }
            //关闭当前窗口
            this.Close();
        }

    }
}
