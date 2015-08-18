using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Text;
using Newtonsoft.Json;

namespace BVT.RepositoryWebApi
{
    public class RepositoryWebApi<T> : IRepositoryWebApi<T> where T : class
    {
        private readonly string _uribase = ConfigurationManager.AppSettings["WebApiUriBase"];
        private readonly RequestMetod _requestMethod = new RequestMetod();
        private readonly ISerialization _serializer = new JsonNetSerialization();
        private String _controllername;
        private const string Apptype = "application/json";


        public RepositoryWebApi(string controllername)
        {
            _controllername = _uribase + controllername + "/";
        }

        public void Delete(T item)
        {
            _requestMethod.GetResponseStream(
                _requestMethod.GetRequest("DELETE", Apptype, string.Format("{0}{1}", _controllername, item))
                              .GetResponse());
        }

        public void Delete<TProperty>(TProperty id)
        {
            _requestMethod.GetResponseStream(
                _requestMethod.GetRequest("DELETE", Apptype, string.Format("{0}{1}", _controllername, id)).GetResponse());
        }

        public void Delete2<TProperty>(TProperty id)
        {
            _requestMethod.GetResponseStream(
                _requestMethod.GetRequest("GET", Apptype, string.Format("{0}{1}", _controllername, id)).GetResponse());
        }


        public List<T> Get()
        {
            var responseStream =
                _requestMethod.GetResponseStream(
                    _requestMethod.GetRequest("GET", Apptype, _controllername).GetResponse());
            var items = JsonConvert.DeserializeObject<List<T>>(DeSerializeProduct(responseStream).ToString());
            return items;
        }

        public List<T> Get(string actionName)
        {
            _controllername = _controllername + actionName + "/";
            return Get();
        }

        public List<T> Get<TProperty>(string actionName, TProperty id)
        {
            _controllername = _controllername + actionName + "/" + id;
            return Get();
        }

        public List<T> Get<TProperty>(string actionName, object args)
        {
            var param = GetParams(args);
            _controllername = _controllername + actionName + "/" + param;
            return Get();
        }



        public T GetByParams(string actionName, object args)
        {
            var param = GetParams(args);
            _controllername = _controllername + actionName + "/" + param;
            var responseStream =
                _requestMethod.GetResponseStream(
                    _requestMethod.GetRequest("GET", Apptype, _controllername).GetResponse());
            var deserizalized = DeSerializeProduct(responseStream);
            if (deserizalized != null)
            {
                var item = JsonConvert.DeserializeObject<T>(deserizalized.ToString());
                return item;
            }
            return default(T);
        }


        public string GetByString(string actionName, object args)
        {
            var param = GetParams(args);
            _controllername = _controllername + actionName + "/" + param;
            var responseStream =
                _requestMethod.GetResponseStream(
                    _requestMethod.GetRequest("GET", Apptype, _controllername).GetResponse());
            var deserizalized = DeSerializeProduct(responseStream);
            return deserizalized != null ? deserizalized.ToString() : string.Empty;
        }

        public bool GetByBool(string actionName, object args)
        {
            var param = GetParams(args);
            _controllername = _controllername + actionName + "/" + param;
            var responseStream =
                _requestMethod.GetResponseStream(
                    _requestMethod.GetRequest("GET", Apptype, _controllername).GetResponse());
            var deserizalized = Convert.ToBoolean(DeSerializeProduct(responseStream).ToString());
            return deserizalized;
        }


        public T GetById<TProperty>(TProperty id)
        {
            var responseStream =
                _requestMethod.GetResponseStream(
                    _requestMethod.GetRequest("GET", Apptype, string.Format("{0}{1}", _controllername, id))
                                  .GetResponse());
            var deserizalized = DeSerializeProduct(responseStream).ToString();
            var item = JsonConvert.DeserializeObject<T>(deserizalized);
            return item;
        }

        public T GetById<TProperty>(string actionName, TProperty id)
        {
            _controllername = _controllername + actionName + "/" + id;
            return GetById(id);
        }

        public void Update<TProperty>(TProperty id, T item)
        {
            _requestMethod.GetRequest("PUT", Apptype, _controllername + id, SerializeEntity(item)).GetResponse();
        }


        public T Create(string actionName, T item)
        {
            _controllername = _controllername + actionName;
            var responseStream =
                _requestMethod.GetResponseStream(
                    _requestMethod.GetRequest("POST", Apptype, _controllername, SerializeEntity(item)).GetResponse());
            var deserizalized = DeSerializeProduct(responseStream).ToString();
            return JsonConvert.DeserializeObject<T>(deserizalized);
        }

        public string SerializeEntity(T item)
        {
            return _serializer.Serialize(item);
        }

        public object DeSerializeProduct(Stream stream)
        {
            return _serializer.DeSerialize(stream);
        }



        private static string GetParams(object args)
        {
            var param = new StringBuilder();
            param.Append("?");
            foreach (var prop in args.GetType().GetProperties())
            {
                param.Append(prop.Name).Append("=").Append(prop.GetValue(args, null)).Append("&");
            }
            return param.ToString().TrimEnd('&');
        }


    }
}