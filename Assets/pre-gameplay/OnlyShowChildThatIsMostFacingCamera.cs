using UnityEngine;

public class OnlyShowChildThatIsMostFacingCamera : MonoBehaviour
{
    public enum BillboardStyle
    {
        matchVectorToCamera,
        matchCameraNegativeZ
    }

    public Camera cam;
    public BillboardStyle billboardStyle;

    void Update()
    {
        EnableBestChildAndHideTheRest();
    }

    void EnableBestChildAndHideTheRest()
    {
        if (transform.childCount == 0)
        {
            return;
        }

        Transform bestChild = transform.GetChild(0);
        float bestChildFacingness = -1;

        // find best child
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            float facingness = GetCameraFacingness(child);
            if (facingness > bestChildFacingness) 
            {
                bestChild = child;
                bestChildFacingness = facingness;
            }
        }

        // disable all bad children
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            if (bestChild != child)
            {
                if (child.gameObject.activeSelf)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        // enable best child
        if (!bestChild.gameObject.activeSelf)
        {
            bestChild.gameObject.SetActive(true);
        }
    }
    
    // returns a value from 0-1, matching method depends on billboardStyle
    float GetCameraFacingness(Transform ofThisTransform)
    {
        Vector3 tForward = ofThisTransform.forward;
        Vector3 directionToMatch;

        if (billboardStyle == BillboardStyle.matchCameraNegativeZ)
        {
            directionToMatch = -cam.transform.forward;
        }
        else
        {
            directionToMatch = Vector3.Normalize(cam.transform.position - ofThisTransform.position);
        }

        return (Vector3.Dot(directionToMatch, tForward) + 1) / 2;
    }
}
