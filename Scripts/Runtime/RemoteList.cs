using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace CodySource
{
    public class RemoteList : MonoBehaviour
    {

        #region PROPERTIES

        public static RemoteList instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectsOfType<RemoteList>()?[0];
                    if (_instance != null) return _instance;
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = obj.AddComponent<RemoteList>();
                }
                return _instance;
            }
        }
        private static RemoteList _instance;

        public string defaultURL = "";
        public UnityEvent<string> onRequestFailed = new UnityEvent<string>();
        public UnityEvent<Response> onRequestComplete = new UnityEvent<Response>();

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Loads the manifest of the streaming assets contents
        /// </summary>
        public void RequestList(string pURL = "", string pFilter = "*", string pKey = "") => StartCoroutine(_Request((pURL=="")?defaultURL:pURL, pFilter, pKey));

        #endregion

        #region INTERNAL METHODS

        /// <summary>
        /// Perform the request
        /// </summary>
        internal IEnumerator _Request(string pURL, string pFilter = "*", string pKey = "")
        {
            WWWForm form = new WWWForm();
            if (pKey != "") form.AddField("key", pKey);
            form.AddField("f", $"{pFilter}");
            using (UnityWebRequest www = UnityWebRequest.Post($"{pURL}", form))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success) onRequestFailed?.Invoke(www.error);
                else onRequestComplete?.Invoke(JsonConvert.DeserializeObject<Response>(www.downloadHandler.text));
            }
        }

        #endregion

        #region PUBLIC STRUCTS

        [System.Serializable]
        public struct Response
        {
            public string error;
            public string[] contents;
        }

        #endregion

    }
}