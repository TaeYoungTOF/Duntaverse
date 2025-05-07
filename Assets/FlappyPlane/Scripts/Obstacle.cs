using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float highPosY = 1f;
    [SerializeField] private float lowPosY = -1f;

    [SerializeField] private float holeSizeMin = 1f;
    [SerializeField] private float holeSizeMax = 3f;

    [SerializeField] private Transform topObject;
    [SerializeField] private Transform bottomObject;

    [SerializeField] private float widthPadding = 4f;

    FlappyPlaneManager flappyPlaneManager;

    private void Start()
    {
        flappyPlaneManager = FlappyPlaneManager.Instance;
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2f;
        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);
        
        transform.position = placePosition;
        
        return placePosition;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Plane plane = collision.GetComponent<Plane>();
        if (plane != null)
            flappyPlaneManager.AddScore(1);
    }
}