using System.IO;
using System.Net;
using System.Text;

namespace BVT.RepositoryWebApi
{
    public class RequestMetod
    {
        public WebRequest GetRequest(string method, string contentType, string endPoint, string content)
        {
            var request = this.GetRequest(method, contentType, endPoint);
            var dataArray = Encoding.UTF8.GetBytes(content.ToString());
            request.ContentLength = dataArray.Length;
            var requestStream = request.GetRequestStream();
            requestStream.Write(dataArray, 0, dataArray.Length);
            requestStream.Flush();
            requestStream.Close();

            return request;
        }

        public WebRequest GetRequest(string method, string contentType, string endPoint)
        {
            var request = WebRequest.Create(endPoint);
            request.Method = method;
            request.ContentType = contentType;

            return request;
        }


        public Stream GetResponseStream(WebResponse response)
        {
            var r = response.GetResponseStream();     
            return r;
        }

        public StreamReader GetResponseReader(WebResponse response)
        {
            return new StreamReader(GetResponseStream(response));
        }

        public string UnPackResponse(WebResponse response)
        {
            return GetResponseReader(response).ReadToEnd();
        }

    }
}