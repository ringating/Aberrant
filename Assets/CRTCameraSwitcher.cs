using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CRTCameraSwitcher : MonoBehaviour
{
    public static CRTCameraSwitcher instance { get; set; }

    public float switchNoiseDuration = 0.5f;
    public Canvas CRTCanvas;
    public Camera noiseCamera;
    [Tooltip("if not set, the crt will start on the noise camera")]
    public Camera currentCamera;
    [Tooltip("where to teleport the player for the duration of the transition")]
    public Transform safeBoxTeleport;

    public AudioClip noiseSound;
    public float noiseVolume = 1f;

    float timer;
    Camera nextCamera;
    Transform nextTeleport;

    private void Awake()
    {
        // singleton stuff
        if (instance)
        {
            Debug.LogError("An instance of CRTCameraSwitcher already exists! Destroying this instance!");
            Destroy(this);
            return;
        }
        instance = this;
    }

	private void Start()
	{
        // set initial values

        if (!currentCamera)
        {
            currentCamera = noiseCamera;
            RawSwitch(currentCamera, false);
        }
        else
        {
            RawSwitch(currentCamera, true);
        }

        timer = -1f;
    }

	void Update()
	{
        // timer for switching between cameras

        if (timer >= 0)
        {
            if (timer > switchNoiseDuration)
            {
                timer = -1;

                RawSwitch(nextCamera, true);

                Player.instance.cc.enabled = false;
                Player.instance.transform.position = nextTeleport.position + Vector3.up;
                Player.instance.transform.rotation = nextTeleport.rotation;
                Player.instance.cc.enabled = true;
            }

            timer += Time.deltaTime;
        }
        else
        {
            // timer is disabled for negative values
        }
	}

    public void SwitchTo(Camera nextCamera, Transform playerTeleport)
    {
        if (nextCamera == Camera.main)
        {
            Debug.LogWarning("don't switch to the main camera!");
            return;
        }

        if (nextCamera == currentCamera)
        {
            Debug.LogWarning("no need to switch to the camera that's already active!");
            return;
        }

        // raw switch to noise cam
        RawSwitch(noiseCamera, false);

        // set nextCamera
        this.nextCamera = nextCamera;

        // set timer to 0 (specifically after RawSwitch, since that sets the timer to -1)
        timer = 0;

        // teleport player to the safety box
        Player.instance.cc.enabled = false;
        Player.instance.transform.position = safeBoxTeleport.transform.position + Vector3.up;
        Player.instance.cc.enabled = true;

        // play noise sound
        Audio2DSingleton.instance.audioSource.PlayOneShot(noiseSound, noiseVolume);

        // set next teleport
        nextTeleport = playerTeleport;

        // clear interact stuff, since it's otherwise not behaving...
        InteractableTrigger.active = null;
        InteractText.instance.tmp.text = "";
    }

    public void RawSwitch(Camera cam, bool applyUI) // "raw" here means no noise transition
    {
        currentCamera = cam;
        StutterRender.currentCamera = cam;

        if (applyUI)
        {
            CRTCanvas.worldCamera = currentCamera; // hopefully this works...

            CRTCamera crtc = cam.gameObject.GetComponent<CRTCamera>();
            if (crtc)
            {
                CameraNumberText.instance.SetNumber(crtc.myCount);
            }
            else 
            {
                Debug.LogWarning("applyUI was true, but the camera lacks a CRTCamera component so the camera string can't be updated!");
                CameraNumberText.instance.SetUnknown();
            }
        }

        timer = -1; // prevents timer code from switching to nextCamera (unless other code sets it to a positive value after this)
    }
}
