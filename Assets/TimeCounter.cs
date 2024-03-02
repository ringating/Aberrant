using UnityEngine;

[RequireComponent(typeof(TimeWriter))]
public class TimeCounter : MonoBehaviour
{
    TimeWriter timeWriter;

    void Start()
    {
        timeWriter = GetComponent<TimeWriter>();
    }

    void Update()
    {
        timeWriter.time += Time.deltaTime;
    }
}
