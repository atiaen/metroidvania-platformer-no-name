using System.Collections;
using UnityEditor;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [HideInInspector]
    [Tooltip("This represents how fast the platform should move in a specified direction")]
    public float movementDistance;

    [HideInInspector]
    [Tooltip("If both 'is' conditions are checked this is to specify the direction to move the plaform")]
    public Vector2 moveVector;

    [SerializeField]
    [Tooltip("This represents how long the platfrom should wait")]
    float plaformWaitTime;

    [SerializeField]
    [Tooltip("This represents how far the platform should move in a specified direction")]
    float movementSpeed;

    [Tooltip("This represents the if the platform will move in the X axis")]
    public bool isMovingX;

    [Tooltip("This represents the if the platform will move in the Y axis")]
    public bool isMovingY;

    [SerializeField]
    bool isWaiting = false;
    bool hasReachedRight = false;
    bool hasReachedLeft = false;

    [SerializeField]
    bool hasReachedUp = false;

    [SerializeField]
    bool hasReachedDown = false;

    Vector3 origPos;

    // Start is called before the first frame update

    void Awake()
    {
    }
    void Start()
    {

        origPos = transform.position;
    }

    void FixedUpdate()
    {
        if (isMovingX)
        {
            if (!hasReachedRight)
            {
                if (!isWaiting && transform.position.x < origPos.x + movementDistance)
                {
                    MoveX(movementDistance);
                }
                else if (!isWaiting && transform.position.x >= origPos.x + movementDistance)
                {
                    hasReachedRight = true;
                    hasReachedLeft = false;
                }
            }
            else if (!hasReachedLeft)
            {
                if (!isWaiting && transform.position.x > origPos.x - movementDistance)
                {
                    MoveX(-movementDistance);
                }
                else if (!isWaiting && transform.position.x <= origPos.x - movementDistance)
                {
                    hasReachedRight = false;
                    hasReachedLeft = true;
                }
            }

            if (transform.position.x == origPos.x + movementDistance || transform.position.x == origPos.x - movementDistance)
            {
                StartCoroutine("waitSequence");
            }
            // else if(!isWaiting && transform.position.x >= origPos.x + movementDistance){
            //     MoveX(-movementDistance);
            // }
        }

        if (isMovingY)
        {
            if (!hasReachedUp)
            {
                if (!isWaiting && transform.position.y < origPos.y + movementDistance)
                {
                    MoveY(movementDistance);
                }
                else if (!isWaiting && transform.position.y >= origPos.y + movementDistance)
                {
                    hasReachedUp = true;
                    hasReachedDown = false;
                }
            }
            else if (!hasReachedDown)
            {
                if (!isWaiting && transform.position.y > origPos.y - movementDistance)
                {
                    MoveY(-movementDistance);
                }
                else if (!isWaiting && transform.position.y <= origPos.y - movementDistance)
                {
                    hasReachedUp = false;
                    hasReachedDown = true;
                }
            }

            if (transform.position.y == origPos.y + movementDistance || transform.position.y == origPos.y - movementDistance)
            {
                StartCoroutine("waitSequence");
            }


        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider);
        collision.collider.gameObject.transform.parent = transform;

    }
    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.collider);
        collision.collider.gameObject.transform.parent = null;

    }
    void MoveX(float distance)
    {
        var newPos = new Vector3(origPos.x + distance, origPos.y, origPos.z);
        transform.position = Vector3.MoveTowards(transform.position, newPos, movementSpeed * Time.deltaTime);

    }

    void MoveY(float distance)
    {
        var newPos = new Vector3(origPos.x, origPos.y + distance, origPos.z);
        transform.position = Vector3.MoveTowards(transform.position, newPos, movementSpeed * Time.deltaTime);

    }

    IEnumerator waitSequence()
    {
        isWaiting = true;
        yield return new WaitForSecondsRealtime(plaformWaitTime);
        isWaiting = false;
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(MovingPlatform))]
public class Platform : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        MovingPlatform script = (MovingPlatform)target;

        if (script.isMovingX)
        {
            script.isMovingY = false;
            script.movementDistance = EditorGUILayout.FloatField("Movement Distance", script.movementDistance);
        }

        if (script.isMovingY)
        {
            script.isMovingX = false;
            script.movementDistance = EditorGUILayout.FloatField("Movement Distance", script.movementDistance);
        }
    }
}
#endif