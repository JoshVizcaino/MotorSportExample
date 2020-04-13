using System.Collections;
using System.Collections.Generic;
using Client.Core.Models;
using Client.Core.Singletons;
using UnityEngine;
using UnityEngine.Networking;


namespace Client.Core
{

    public class InterviewClient : Singleton<InterviewClient>
    {
        private const string defaultContentType = "application/json";

        //Gets a URL and callback to add/execute in game code
        public IEnumerator HttpGet(string url, System.Action<Response> callback)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    callback(new Response
                    {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                    });
                }

                if (webRequest.isDone)
                {
                    string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                    Debug.Log("Data: " + data);
                    callback(new Response
                    {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                        Data = data
                    });
                }
            }
        }

        //Delete data that is stored. Takes URL and callback
        public IEnumerator HttpDelete(string url, System.Action<Response> callback)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Delete(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    callback(new Response
                    {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error
                    });
                }

                if (webRequest.isDone)
                {
                    callback(new Response
                    {
                        StatusCode = webRequest.responseCode
                    });
                }
            }
        }

        //Used to insert data. Takes url/endpoint to update body/json. Inlcudes call back and also header for client id and client secret
        public IEnumerator HttpPost(string url, string body, System.Action<Response> callback, IEnumerable<RequestHeader> headers = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Post(url, body))
            {
                if (headers != null)
                {
                    foreach (RequestHeader header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                //specify application type to post to
                webRequest.uploadHandler.contentType = defaultContentType;
                //include data to upload
                webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(body));

                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    callback(new Response
                    {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error
                    });
                }

                if (webRequest.isDone)
                {
                    string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                    callback(new Response
                    {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                        Data = data
                    });
                }
            }
        }

        //Update player/user data
        public IEnumerator HttpPut(string url, string body, System.Action<Response> callback, IEnumerable<RequestHeader> headers = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Put(url, body))
            {
                if (headers != null)
                {
                    foreach (RequestHeader header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                webRequest.uploadHandler.contentType = defaultContentType;
                webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(body));

                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    callback(new Response
                    {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                    });
                }

                if (webRequest.isDone)
                {
                    callback(new Response
                    {
                        StatusCode = webRequest.responseCode,
                    });
                }
            }
        }

        public IEnumerator HttpHead(string url, System.Action<Response> callback)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Head(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    callback(new Response
                    {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                    });
                }

                if (webRequest.isDone)
                {
                    var responseHeaders = webRequest.GetResponseHeaders();
                    callback(new Response
                    {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                        Headers = responseHeaders
                    });
                }
            }
        }
    }
    


}
