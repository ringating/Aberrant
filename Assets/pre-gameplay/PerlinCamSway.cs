using UnityEngine;

public class PerlinCamSway : MonoBehaviour
{
    public Vector3 baseLocalRotEulers;
    [SerializeField] bool inheritTransformRotationOnEnable = true;
    
    [Header("--- magnitudes ---")]
    [SerializeField] float xMagnitude;
    [SerializeField] float yMagnitude;
    [SerializeField] float zMagnitude;

    [Header("--- frequencies ---")]
    [SerializeField] float xFrequency;
    [SerializeField] float yFrequency;
    [SerializeField] float zFrequency;

    PerlinAtRandomY xPerlin;
    PerlinAtRandomY yPerlin;
    PerlinAtRandomY zPerlin;

    float xPos;
    float yPos;
    float zPos;

    void Start()
    {
        xPerlin = new PerlinAtRandomY();
        yPerlin = new PerlinAtRandomY();
        zPerlin = new PerlinAtRandomY();

        xPos = 0;
        yPos = 0;
        zPos = 0;
    }

	private void OnEnable()
	{
        if (inheritTransformRotationOnEnable)
        {
            baseLocalRotEulers = transform.localRotation.eulerAngles;
        }
    }

	void Update()
    {
        xPos += Time.deltaTime * xFrequency;
        yPos += Time.deltaTime * yFrequency;
        zPos += Time.deltaTime * zFrequency;

        float xShift = xMagnitude * (xPerlin.Perlin(xPos) * 2 - 1);
        float yShift = yMagnitude * (yPerlin.Perlin(yPos) * 2 - 1);
        float zShift = zMagnitude * (zPerlin.Perlin(zPos) * 2 - 1);

        Quaternion shift = Quaternion.Euler(xShift, yShift, zShift);

        transform.localRotation = Quaternion.Euler(baseLocalRotEulers.x + shift.eulerAngles.x, 
                                                   baseLocalRotEulers.y + shift.eulerAngles.y, 
                                                   baseLocalRotEulers.z + shift.eulerAngles.z);
    }
}

public class PerlinAtRandomY
{
    public float yOffset { get; private set; }

    public PerlinAtRandomY()
    {
        yOffset = Random.Range(1, 1000000000) + 0.5f; // perlin noise always returns the same value at integer grid values, so avoid having near-integer offsets
    }

    public PerlinAtRandomY(float specificYValue)
    {
        yOffset = specificYValue;
    }

    public float Perlin(float x)
    {
        return Mathf.PerlinNoise(x, yOffset);
    }
}
