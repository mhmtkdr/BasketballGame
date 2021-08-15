using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallControl : MonoBehaviour
{
    Vector2 startPos, endPos, direction;
    [SerializeField]
    float throwForceInXandY = 1f;

    [SerializeField]
    float throwForceInZ = 50f;

    float touchTimeStart, touchTimeFinish, timeInterval;
    public Rigidbody rb;
    public float moveSpeed=10f;
    public Text countText;

    private int count;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        count = 0;
        SetCountText();
    }

    
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchTimeStart = Time.time;
            startPos = Input.GetTouch(0).position;
        }

        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {

            
            touchTimeFinish = Time.time;

             
            timeInterval = touchTimeFinish - touchTimeStart;

            
            endPos = Input.GetTouch(0).position;

            
            direction = startPos - endPos;

            
            rb.isKinematic = false;
            rb.AddForce(-direction.x * throwForceInXandY, -direction.y * throwForceInXandY, throwForceInZ / timeInterval);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("score"))
        {
            count++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Score:" + count.ToString();
    }
}
