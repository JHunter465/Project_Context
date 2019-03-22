using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ScreenManager : MonoBehaviour
{
    public bool RecordWebcam;
    public Text TimerText;
    float timer = 0;
    bool isTiming = false;
    bool isVideo = false;
    //VideoCapture videoCapture;

    //static readonly float MaxRecordingTime = 5.0f;
    //float m_stopRecordingTimer = float.MaxValue;

    [SerializeField] VideoPlayer vidPlayer;
    VideoClip currentVideoClip;
    [SerializeField] RawImage rawImage;

    public List<VideoClip> videoClips = new List<VideoClip>();

    private void Start()
    {
        currentVideoClip = videoClips[0];

        //if(RecordWebcam)
        //{
        //    Resolution cameraResolution = VideoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        //    Debug.Log(cameraResolution);

        //    float cameraFramerate = VideoCapture.GetSupportedFrameRatesForResolution(cameraResolution).OrderByDescending((fps) => fps).First();
        //    Debug.Log(cameraFramerate);

        //    VideoCapture.CreateAsync(false, delegate (VideoCapture _videoCapture)
        //    {
        //        Debug.Log("Test");
        //        if (_videoCapture != null)
        //        {
        //            videoCapture = _videoCapture;
        //            Debug.Log("Created VideoCapture Instance!");

        //            CameraParameters cameraParameters = new CameraParameters();
        //            cameraParameters.hologramOpacity = 0.0f;
        //            cameraParameters.frameRate = cameraFramerate;
        //            cameraParameters.cameraResolutionWidth = cameraResolution.width;
        //            cameraParameters.cameraResolutionHeight = cameraResolution.height;
        //            cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

        //            videoCapture.StartVideoModeAsync(cameraParameters,
        //                VideoCapture.AudioState.ApplicationAndMicAudio,
        //                OnStartedVideoCaptureMode);
        //        }
        //        else
        //        {
        //            Debug.LogError("Failed to create VideoCapture Instance!");
        //        }
        //    });

        //}
    }

    private void Update()
    {
        CheckKeyInput();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("enter"))
        {
            if (isVideo)
                StartVideoPlayOnly(vidPlayer);
            else
                StartWebCamVideo(rawImage);
        }

        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Escape))
        {
            ResetAllTheThings();
        }

        if (isTiming)
        {
            string minutes = Mathf.Floor(timer / 60).ToString("00");
            string seconds = (timer % 60).ToString("00");

            TimerText.text = string.Format("{0}:{1}", minutes, seconds);

            timer += Time.deltaTime;
        }

        //if (videoCapture == null || !videoCapture.IsRecording)
        //{
        //    return;
        //}

        //if (Time.time > m_stopRecordingTimer)
        //{
        //    videoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
        //}
    }

    void CheckKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentVideoClip = videoClips[0];

        if (Input.GetKeyDown(KeyCode.Alpha2))
            currentVideoClip = videoClips[1];

        if (Input.GetKeyDown(KeyCode.Alpha3))
            currentVideoClip = videoClips[2];

        if (Input.GetKeyDown(KeyCode.Alpha4))
            currentVideoClip = videoClips[3];

        if (Input.GetKeyDown(KeyCode.Alpha5))
            currentVideoClip = videoClips[4];

        if (Input.GetKeyDown(KeyCode.Alpha6))
            currentVideoClip = videoClips[5];

        if (Input.GetKeyDown(KeyCode.Alpha7))
            currentVideoClip = videoClips[6];

        if (Input.GetKeyDown(KeyCode.Alpha8))
            currentVideoClip = videoClips[7];

        if (Input.GetKeyDown(KeyCode.Alpha9))
            currentVideoClip = videoClips[8];
    }

    public void StartVideoPlayOnly(VideoPlayer _videoPlayer)
    {
        if (_videoPlayer != null)
        {
            _videoPlayer.clip = currentVideoClip;
            vidPlayer = _videoPlayer;

            if (!_videoPlayer.isPlaying)
                _videoPlayer.Play();
        }
    }

    public void StartVideoWithPause(VideoPlayer _videoPlayer)
    {
        if (_videoPlayer != null)
        {
            _videoPlayer.clip = currentVideoClip;
            vidPlayer = _videoPlayer;

            if (!_videoPlayer.isPlaying)
                _videoPlayer.Play();
            else
                _videoPlayer.Pause();
        }
    }

    public void StartWebCamVideo(RawImage _rawImage)
    {
        rawImage = _rawImage;

        WebCamTexture _webcamText = new WebCamTexture();
        _rawImage.gameObject.SetActive(true);
        _rawImage.texture = _webcamText;
        _rawImage.material.mainTexture = _webcamText;
        _webcamText.Play();

        isTiming = true;
    }

    public void ResetAllTheThings()
    {
        if (vidPlayer != null)
            vidPlayer.Stop();
        if (rawImage != null)
            rawImage.gameObject.SetActive(false);

        isTiming = false;
        timer = 0;
        TimerText.text = "00:00";
    }

    //void OnStartedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    //{
    //    Debug.Log("Started Video Capture Mode!");
    //    string timeStamp = Time.time.ToString().Replace(".", "").Replace(":", "");
    //    string filename = string.Format("TestVideo_{0}.mp4", timeStamp);
    //    string filepath = System.IO.Path.Combine(Application.persistentDataPath, filename);
    //    filepath = filepath.Replace("/", @"\");
    //    videoCapture.StartRecordingAsync(filepath, OnStartedRecordingVideo);
    //}

    //void OnStartedRecordingVideo(VideoCapture.VideoCaptureResult result)
    //{
    //    Debug.Log("Started Recording Video!");
    //    m_stopRecordingTimer = Time.time + MaxRecordingTime;
    //}

    //void OnStoppedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    //{
    //    Debug.Log("Stopped Video Capture Mode!");
    //}

    //void OnStoppedRecordingVideo(VideoCapture.VideoCaptureResult result)
    //{
    //    Debug.Log("Stopped Recording Video!");
    //    videoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);
    //}
}
