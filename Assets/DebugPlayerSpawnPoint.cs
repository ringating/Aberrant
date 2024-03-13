using UnityEngine;

public class DebugPlayerSpawnPoint : MonoBehaviour
{
    void Start()
    {
        Player.instance.transform.position = transform.position;
        Player.instance.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

	private void OnDrawGizmos()
	{
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawRay(transform.position, transform.forward);
	}
}
