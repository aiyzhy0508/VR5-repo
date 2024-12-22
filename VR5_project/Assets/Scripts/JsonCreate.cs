using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class JsonCreate : MonoBehaviour
{
    // Start is called before the first frame update
    public Text nm;
    public Text lvl;
    public Slider dat;
    public Text dat1;
    public string jsonURL;
    public JsonClass jsnData;
    void Start()
    {
        dat.interactable = false;
        StartCoroutine(getdata());
    }
    void FixedUpdate()
    {

    }
    IEnumerator getdata()
    {
        Debug.Log("Loading...");
        var uvr = new UnityWebRequest(jsonURL);
        uvr.method = UnityWebRequest.kHttpVerbGET;
        var resultFile = Path.Combine(Application.persistentDataPath, "result.json");
        var dh = new DownloadHandlerFile(resultFile);
        dh.removeFileOnAbort = true;
        uvr.downloadHandler = dh;
        yield return uvr.SendWebRequest();
        if (uvr.result != UnityWebRequest.Result.Success)
        {
            nm.text = "Error!";
        }
        else
        {
            Debug.Log("File saved on directory:" + resultFile);
            jsnData = JsonUtility.FromJson<JsonClass>(File.ReadAllText(Application.persistentDataPath + "/result.json"));
            nm.text = jsnData.Name.ToString();
            lvl.text = jsnData.Level.ToString();
            dat.value = jsnData.TestParam;
            dat1.text = jsnData.TestParam.ToString();
            yield return StartCoroutine(getdata());
        }
    }
    [System.Serializable]
    public class JsonClass
    {
        public string Name;
        public int Level;
        public int TestParam;
    }
}