using UnityEngine;

public class SpinY : MonoBehaviour
{
    [SerializeField] float degreesPerSecond;

    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 
                                              transform.rotation.eulerAngles.y + Time.deltaTime * degreesPerSecond, 
                                              transform.rotation.eulerAngles.z);
    }
}
