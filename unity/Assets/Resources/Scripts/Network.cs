using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Net;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Network : MonoBehaviour
{
    [SerializeField]
    // Flask server endpoint
    const private string serverUrl = "http://127.0.0.1:5000/upload";

    public Panel_Info panelInfo;
    public Panel_Loading panelLoading;
    public Panel_Notice panelNotice;

    public Spawner spawner;

    Coroutine corRequest = null;

    private void Awake()
    {
        CloseAllPanel();
    }

    public void Classification()
    {
        if (corRequest != null)
        {
            // notice panel 
            return;
        }
        corRequest = StartCoroutine(GetRequest());
    }

    string OpenPanelInfo(string response)
    {
        OpenPanel(panelInfo.gameObject);
        return panelInfo.Init(response);
    }

    void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);    
    }

    void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    void CloseAllPanel()
    {
        ClosePanel(panelInfo.gameObject);
        ClosePanel(panelNotice.gameObject);
        ClosePanel(panelLoading.gameObject);
    }

    IEnumerator GetRequest()
    {
        // loading panel
        OpenPanel(panelLoading.gameObject);
        spawner.Eliminate();
        yield return new WaitForEndOfFrame();

        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        byte[] imageBytes = tex.EncodeToPNG();

        WWWForm form = new WWWForm();
        form.AddBinaryData("file", imageBytes, "image.jpg", "image/jpg");

        UnityWebRequest wwwUpload = UnityWebRequest.Post(serverUrl, form);
        yield return wwwUpload.SendWebRequest();

        ClosePanel(panelLoading.gameObject);
        if (wwwUpload.result != UnityWebRequest.Result.Success)
        {
            OpenPanel(panelNotice.gameObject);
            Debug.Log(wwwUpload.error);
        }
        else
        {
            string response = wwwUpload.downloadHandler.text;
            response = response.Trim(new char[] { '[', ']' });
            if(response.Length == 0)
            {
                // panel notice "Have NO results......";
                OpenPanel(panelNotice.gameObject);
                Debug.LogWarning( "(response empty)Have NO results......");
                corRequest = null;
                yield break;
            }

            string tag = OpenPanelInfo(response);
            Debug.Log(tag);
            if (string.IsNullOrEmpty(tag) == false) 
            {
                SpawnObject(tag);
            }
            else
            {
                // panel notice "Have NO results......";
                OpenPanel(panelNotice.gameObject);
            }
        }

        corRequest = null;
    }

    void SpawnObject(string name)
    {
        spawner.Spawn(name);
    }
}

