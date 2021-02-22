using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SFB;
using TMPro;
using System;

public class MainManager : MonoBehaviour
{
    public Texture2D InputImageTexture;
    public TextMeshProUGUI InputFileText;
    public TextMeshProUGUI OutputFolderText;
    public TextMeshProUGUI FeedbackText;
    public GameObject Canvas;
    public RawImage InputImage;

    ExtensionFilter[] extensions = new [] {
        new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
        new ExtensionFilter("All Files", "*" ),
    };

    void Start() {
        FeedbackText.text = "";
    }

    public void DidTapSelectSaveFolder() {
        FeedbackText.text = "";
        var path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "output " + DateTime.Now + ".png", extensions);
        if (path.Length == 0) {
            FeedbackText.text = "Selected path is empty";
            return;
        }
        StartCoroutine(SaveScreenshot(path));
    }

    IEnumerator SaveScreenshot(string path) {
        Canvas.SetActive(false);

        yield return new WaitForEndOfFrame();
        OutputFolderText.text = path;
        ScreenCapture.CaptureScreenshot(path);

        yield return new WaitForSeconds(0.5f);
        Canvas.SetActive(true);
    }

    public void DidTapSelectImageFile()
    {
        FeedbackText.text = "";
        StartCoroutine("selectImage");
    }

    IEnumerator selectImage() {

        string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);
        
        if (paths.Length == 0 || paths[0].Length == 0) {
            FeedbackText.text = "Selected path is empty";
            yield break;
        }

        InputFileText.text = paths[0];

        WWW load = new WWW("file:///" + paths[0]);
        yield return load;
        if (!string.IsNullOrEmpty(load.error)) {
            Debug.LogWarning(paths + " error");
            FeedbackText.text = "Something went wrong: " + load.error;
        } else {
            InputImageTexture = load.texture;
            InputImage.texture = InputImageTexture;
        }
    }
}