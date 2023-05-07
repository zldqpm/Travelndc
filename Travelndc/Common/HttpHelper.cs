using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Threading;

namespace Travelndc.Common
{
    public class HttpHelper
    {
        public int timeout = 5;
        public string proxyIP;
        public HttpHelper(int _timeout)
        {
            timeout = _timeout;
        }
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="headers">请求头</param>
        /// <returns></returns>
        public async Task<RestResponse> GetAsync(string url, Dictionary<string, string> headers)
        {
            try
            {
                var options = new RestClientOptions();
                if (!string.IsNullOrEmpty(proxyIP))
                {
                    var webProxy = new WebProxy(proxyIP);
                    options.Proxy = webProxy;
                }
                options.MaxTimeout = timeout;
                var client = new RestClient(options);
                var request = new RestRequest(url, Method.Get);
                foreach (var item in headers) request.AddHeader(item.Key, item.Value);
                return await client.ExecuteAsync(request);
            }
            catch (Exception ex)
            {
                LogHelper.Error("get请求出错：" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        ///Post
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="headers">请求头</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public async Task<RestResponse> PostAsync(string url, Dictionary<string, string> headers, Dictionary<string, string> parameter)
        {
            try
            {
                var options = new RestClientOptions();
                if (!string.IsNullOrEmpty(proxyIP))
                {
                    var webProxy = new WebProxy(proxyIP);
                    options.Proxy = webProxy;
                }
                options.MaxTimeout = timeout;
                var client = new RestClient(options);
                var request = new RestRequest(url, Method.Post);
                foreach (var item in headers) request.AddHeader(item.Key, item.Value);
                foreach (var item in parameter) request.AddParameter(item.Key, item.Value);
                return await client.ExecuteAsync(request);
            }
            catch (Exception ex)
            {
                LogHelper.Error("post请求出错：" + ex.Message.ToString());
                return null;
            }
        }
        public async Task<RestResponse> PostAsync(string url, Dictionary<string, string> headers, string body)
        {
            var options = new RestClientOptions();
            if (!string.IsNullOrEmpty(proxyIP))
            {
                var webProxy = new WebProxy(proxyIP);
                options.Proxy = webProxy;
            }
            options.MaxTimeout = timeout;
            var client = new RestClient(options);
            var request = new RestRequest(url, Method.Post);
            foreach (var item in headers) request.AddHeader(item.Key, item.Value);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            return await client.ExecuteAsync(request);
        }

        /// <summary>
        /// Get请求没有返回值的
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns></returns>
        public async Task<RestResponse> GetAsync(string url)
        {
            try
            {
                var options = new RestClientOptions();
                if (!string.IsNullOrEmpty(proxyIP))
                {
                    var webProxy = new WebProxy(proxyIP);
                    options.Proxy = webProxy;
                }
                options.MaxTimeout = timeout;
                var client = new RestClient(options);
                var request = new RestRequest(url, Method.Get);
                var response = await client.ExecuteAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetResponse出错：" + ex.Message.ToString());
                return null;
            }
        }
    }
}
