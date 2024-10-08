using UnityEngine;

public class Oscilator : MonoBehaviour
{
    [SerializeField][Range(0, 1)] float movementFactor;
    [SerializeField] Vector3 movementVector;
    float period = 5f;

    Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) return;

        const float tau = Mathf.PI * 2;

        float cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
