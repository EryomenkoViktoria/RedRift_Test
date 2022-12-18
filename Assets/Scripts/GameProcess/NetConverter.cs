using System;
using UnityEngine.Networking;

namespace RedRift.Test.GameMechanic.Buffering
{
    public class NetConverter
    {
        public event Action<byte[]> ImageAnswer;
        private string _urlPhotos;

        public NetConverter(string urlResources)
        {
            _urlPhotos = urlResources;
        }

        public void BufferingImages()
        {
            var www = new UnityWebRequest(_urlPhotos) 
            {
                downloadHandler = new DownloadHandlerBuffer()
            };
            var webRequest = www.SendWebRequest();

            webRequest.completed += operation =>
            {
                ImageAnswer?.Invoke(www.downloadHandler.data);
                www.Dispose();
            };
        }
    }
}