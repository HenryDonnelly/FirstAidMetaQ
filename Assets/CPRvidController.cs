using UnityEngine;
using UnityEngine.Video;

public class CPRVidController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoPanel;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    public void PlayVideo()
    {
        videoPanel.SetActive(true);
        videoPlayer.Play();
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
        videoPanel.SetActive(false);
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        videoPanel.SetActive(false);
    }
}