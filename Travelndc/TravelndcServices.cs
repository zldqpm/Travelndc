using HtmlAgilityPack;
using RestSharp;
using SqlSugar;
using System.Text;
using System.Text.Json;
using Travelndc.Common;
using Travelndc.Common.MemoryCache;
using Travelndc.Models;

namespace Travelndc
{
    public class TravelndcServices
    {
        private readonly ICaching _cache;
        private readonly HttpHelper httpHelper;
        protected ISqlSugarClient _client { get; set; }
        public TravelndcServices(ICaching cache, int timeout, ISqlSugarClient client)
        {
            _cache = cache;
            httpHelper = new HttpHelper(timeout);
            _client = client;
        }

        public async Task<string> Login()
        {
            var userId = "wangzhen@Travelndc.com";
            var passWord = "b7OEQfwYl7rb/X6hBlpBRYEQDFeM+7x9LZqitaMY23k"; ;
            //验证码请求地址
            var codeUrl = "http://www.Travelndc.com.cn/ndc/authCoo.do?0.30830960128460316";
            //登录请求地址
            var loginUrl = "http://www.Travelndc.com.cn/ndc/toNdc.do";
            RestResponse response = await httpHelper.GetAsync(codeUrl);
            if (!response.IsSuccessful)
            {
                return "获取验证码失败";
            }
            //获取图片验证码
            string authCode = OcrHelper.GetCode(response.RawBytes);
            //获取cookie
            var cookies = response.Cookies.ToList();
            StringBuilder sb_cookie = new StringBuilder();
            foreach (var item in cookies)
            {
                sb_cookie.Append(item.Name);
                sb_cookie.Append("=");
                sb_cookie.Append(item.Value);
                sb_cookie.Append(";");
            }
            string cookie = sb_cookie.ToString();
            //登录
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Host", "www.Travelndc.com.cn" },
                { "Origin", "http://www.Travelndc.com.cn" },
                { "Referer", "http://www.Travelndc.com.cn/ndc/login.do" },
                { "Content-Type","application/x-www-form-urlencoded" },
                { "Cookie", cookie }
            };
            Dictionary<string, string> parameter = new Dictionary<string, string>
            {
                { "userId", "wangzhen@Travelndc.com" },
                { "password", "b7OEQfwYl7rb/X6hBlpBRYEQDFeM+7x9LZqitaMY23k" },
                { "authCode", authCode }
            };
            response = await httpHelper.PostAsync(loginUrl, headers, parameter);
            if (!response.IsSuccessful)
            {
                return "登录失败,请重试";
            }
            //缓存
            _cache.Set("userId", userId, 30);
            _cache.Set("password", passWord, 30);
            _cache.Set("authCode", authCode, 30);
            _cache.Set("cookie", cookie, 30);
            return "登录成功";
        }

        public async Task<(bool, string)> GetUserInfo()
        {
            string cookie = "";
            bool isSuccess = false;
            if (_cache.Get("cookie") is null)
            {
                return (isSuccess, "请先登录");
            }
            cookie = _cache.Get("cookie").ToString()!;
            //获取人员信息请求地址
            var userUrl = "http://www.Travelndc.com.cn/ndc/selectUser.do";
            //获取人员信息
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Host", "www.Travelndc.com.cn" },
                { "Origin", "http://www.Travelndc.com.cn" },
                { "Referer", "http://www.Travelndc.com.cn/ndc/search.do" },
                { "Cookie",cookie }
            };
            var response = await httpHelper.GetAsync(userUrl, headers);
            if (!response.IsSuccessful)
            {
                return (isSuccess, "获取人员信息失败，请重试");
            }
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(response.Content);
            var node = doc.DocumentNode.SelectNodes("//*[@id='content2']/div[1]/table/tr[3]/td[3]");
            if (node != null && node.Count > 0)
            {
                isSuccess = true;
                return (isSuccess, node[0].InnerText);
            }
            return (isSuccess, "获取手机号失败，请重试");
        }

        public async Task<(bool, string)> GetFlight()
        {
            string cookie = "";
            bool isSuccess = false;
            if (_cache.Get("cookie") is null)
            {
                return (isSuccess, "请先登录");
            }
            cookie = _cache.Get("cookie").ToString()!;

            var url = "https://www.Travelndc.com.cn/ndc/AirShopping.do";
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Accept", "application/json, text/javascript, */*; q=0.01" },
                { "Accept-Encoding", "gzip, deflate, br" },
                { "Accept-Language", "zh-CN,zh;q=0.9" },
                { "Connection", "keep-alive" },
                { "Content-Length", "351" },
                { "Content-Type", "application/json" },
                { "Host", "www.Travelndc.com.cn" },
                { "Origin", "https://www.Travelndc.com.cn" },
                { "Referer", "https://www.Travelndc.com.cn/ndc/toAirShoppingPage.do" },
                { "Sec-Fetch-Dest", "empty" },
                { "Sec-Fetch-Mode", "cors" },
                { "Sec-Fetch-Site", "same-origin" },
                { "X-Requested-With", "XMLHttpRequest" },
                { "sec-ch-ua", "\"Google Chrome\";v=\"111\", \"Not(A:Brand\";v=\"8\", \"Chromium\";v=\"111\"" },
                { "sec-ch-ua-mobile", "?0" },
                { "sec-ch-ua-platform", "\"Windows\"" },
                { "Cookie",cookie }
            };

            var body = @"{""adt"":0,""anonymousTravelerList"":[{}],""arrivalAirportCode"":""BJS, 北京, 中国"",""arrivalDate"":""2023-05-10"",""cabinType"":""5"",""calendar"":""0"",""chd"":0,""company"":""ALL"",""departureAirportCode"":""SHA, 上海, 中国"",""departureDate"":""2023-05-08"",""fareType"":"""",""flightType"":""1"",""from"":""0"",""inf"":0,""isSplitPage"":""Y"",""passengerTypeStr"":""{ADT:1}"",""type"":1,""yth"":0}"; var response = await httpHelper.PostAsync(url, headers, body);
            if (!response.IsSuccessful)
            {
                return (isSuccess, "获取航班信息失败,请重试");
            }
            List<Flight> list = new List<Flight>();
            AirShopping air = JsonSerializer.Deserialize<AirShopping>(response.Content);
            if (air.successed)
            {
                foreach (var item in air.airOfferGroupListBySplit)
                {
                    foreach (var airitem in item)
                    {
                        Flight flight = new Flight();
                        flight.price = airitem.price;
                        flight.flyTime = airitem.flyTime[0];
                        flight.departureDate = airitem.flightSegmentGroup[0][0].departureDate;
                        flight.departureTime = airitem.flightSegmentGroup[0][0].departureTime;
                        flight.departureAirportName = airitem.flightSegmentGroup[0][0].departureAirportName;
                        flight.departureTerminal = airitem.flightSegmentGroup[0][0].departureTerminal;
                        flight.arrivalDate = airitem.flightSegmentGroup[0][0].arrivalDate;
                        flight.arrivalTime = airitem.flightSegmentGroup[0][0].arrivalTime;
                        flight.arrivalAirportName = airitem.flightSegmentGroup[0][0].arrivalAirportName;
                        flight.arrivalTerminal = airitem.flightSegmentGroup[0][0].arrivalTerminal;
                        flight.airlineName = airitem.flightSegmentGroup[0][0].airlineName;
                        flight.flightNo = airitem.flightSegmentGroup[0][0].flightNo;
                        flight.aircraftCode = airitem.flightSegmentGroup[0][0].aircraftCode;
                        flight.createTime = DateTime.Now;
                        list.Add(flight);
                    }
                }
                Console.WriteLine("出发地-目的地   出发时间-到达时间   价格   飞行时间");
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        Console.WriteLine($"{item.departureAirportName + item.departureTerminal}-{item.arrivalAirportName + item.arrivalTerminal}   {item.departureDate + " " + item.departureTime}-{item.arrivalDate + " " + item.arrivalTime}   {item.price}   {item.flyTime}");
                    }
                    await _client.Insertable(list).ExecuteReturnEntityAsync();
                }
            }
            isSuccess = true;
            return (isSuccess, "获取上海到北京的");
        }

        private string proxyIP = "";
        private int proxyIPCount = 0;

        public async Task Main()
        {
            if (string.IsNullOrEmpty(proxyIP))
            {
                Console.WriteLine("请输入代理IP，格式为xxx:xxxx 比如61.160.194.106:8918");
                proxyIP = Console.ReadLine();
            }
            if (proxyIPCount > 0)
            {
                Console.WriteLine("是否更换代理IP，输入y是 n否");
                string isIP = Console.ReadLine();
                if (!string.IsNullOrEmpty(isIP) && isIP == "y")
                {
                    Console.WriteLine("请输入代理IP，格式为xxx:xxxx 比如61.160.194.106:8918");
                    proxyIP = Console.ReadLine();
                }
            }
            httpHelper.proxyIP = proxyIP;
            proxyIPCount++;
            string result = "";
            Console.WriteLine("请输入题目编号，1.1、1.2、2.1、3.1");
            string method = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(method))
            {
                Console.WriteLine("请输入题目编号，1.1、1.2、2.1、3.1");
            }
            switch (method)
            {
                case "1.1":
                    (result) = await Login();
                    Console.WriteLine(result);
                    break;
                case "1.2":
                    (_, result) = await GetUserInfo();
                    Console.WriteLine(result);
                    break;
                case "2.1":
                    Console.WriteLine("查询的是2023-05-08 上海到北京");
                    (_, result) = await GetFlight();
                    Console.WriteLine(result);
                    break;
                case "3.1":
                    Console.WriteLine("功能和2.1一样查询的是2023-05-08 上海到北京");
                    (_, result) = await GetFlight();
                    Console.WriteLine(result);
                    break;
                default:
                    Console.WriteLine("请输入正确的题目编号，1.1、1.2、2.1、3.1");
                    break;
            }
            await Main();
        }
    }
}
