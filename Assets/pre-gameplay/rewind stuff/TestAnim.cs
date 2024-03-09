using UnityEngine;

public class TestAnim : MonoBehaviour
{
    [SerializeField] Animation anim;
    [SerializeField] KeyCode key;

    void Update()
    {
        if (Input.GetKeyDown(key)) 
        {
            anim.Stop();
            anim.Play();
        }
    }
}
