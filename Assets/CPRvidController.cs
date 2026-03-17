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

    public void PauseVideo()
    {
        if (videoPlayer.isPlaying)
            videoPlayer.Pause();
    }

    public void ResumeVideo()
    {
        if (!videoPlayer.isPlaying)
            videoPlayer.Play();
    }

    public void Rewind10()
    {
        double newTime = videoPlayer.time - 10.0;

        if (newTime < 0)
            newTime = 0;

    videoPlayer.time = newTime;
    }

    public void FastForward10()
    {
        double newTime = videoPlayer.time + 10.0;

        if (newTime > videoPlayer.length)
            newTime = videoPlayer.length - 0.1;

        videoPlayer.time = newTime;
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