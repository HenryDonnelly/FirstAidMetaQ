using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class CPRVidController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoPanel;
    public RawImage videoScreen;

    void Start()
    {
        SetVideoVisible(false);
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    public void PlayVideo()
    {
        SetVideoVisible(true);
        videoPlayer.time = 0;
        videoPlayer.Play();
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
        SetVideoVisible(false);
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SetVideoVisible(false);
    }

    void SetVideoVisible(bool visible)
    {
        Color c = videoScreen.color;
        c.a = visible ? 1f : 0f;
        videoScreen.color = c;
    }
}