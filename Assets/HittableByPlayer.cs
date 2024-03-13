using UnityEngine;

public class HittableByPlayer : MonoBehaviour
{
	public delegate void HitByPlayerAction();
	public event HitByPlayerAction OnHitByPlayer;

	public void Hit()
	{
		OnHitByPlayer?.Invoke();
	}
}
