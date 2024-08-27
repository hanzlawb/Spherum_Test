using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using TMPro;

public class ImageDownloader : MonoBehaviour
{
    public TMP_InputField urlInputField; // Reference to the Input Field
    public ParticleSystem particleSystem; // Reference to the Particle System
    public Button downloadButton;
    void Start()
    {
        downloadButton.onClick.AddListener(OnDownloadButtonClicked);
    }

    void OnDownloadButtonClicked()
    {
        string url = urlInputField.text;
        if (IsValidURL(url))
        {
            StartCoroutine(DownloadImage(url));
        }
        else
        {
            Debug.LogError("Invalid URL.");
        }
    }

    bool IsValidURL(string url)
    {
        string pattern = @"^https?:\/\/.+\.(jpg|jpeg|png)$";
        return Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase);
    }

    IEnumerator DownloadImage(string url)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                ApplyTextureToParticleSystem(texture);
            }
        }
    }

    void ApplyTextureToParticleSystem(Texture2D texture)
    {
        var renderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        renderer.material.mainTexture = texture;

        var textureSheetAnimation = particleSystem.textureSheetAnimation;
        textureSheetAnimation.SetSprite(0, Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f)));
    }
}
