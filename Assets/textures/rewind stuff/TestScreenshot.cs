using UnityEngine;

public class TestScreenshot : MonoBehaviour
{
    [SerializeField] RenderTexture renderTexture;
    [SerializeField] Camera cam;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            cam.targetTexture = renderTexture;
            cam.Render();
            cam.targetTexture = null;
        }
    }
}
