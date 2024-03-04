using UnityEngine;

public class ImpacterTester : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Impacter.instance.Impact(Impacter.ImpactType.right);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Impacter.instance.Impact(Impacter.ImpactType.left);
        }
    }
}
