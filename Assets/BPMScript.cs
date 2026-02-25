using UnityEngine;
using UnityEngine.Video;

public class BPMScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoPanel;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPanel.SetActive(false);
    }

    public void PlayVideo()
    {
        videoPlayer.time = 0;
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