using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Transform target;
    private Animator anim;
    private Rigidbody rigidBody;
    private Transform Body;

    public float forwardSpeed = 3;
    public float forwardDistanceMove = 10;
    public float dashDelay = .5f;
    public float stopMoveSmooth = 2;
    public float backwardSpeed = 2;
    public float backwardDistanceMove = 12;
    public float rightSpeed = 3;
    public float rightDistanceMove = 6;
    public float rightMoveAngle = 30;

    public Vector3 Distination;

    public bool dashForward;
    public bool dashBackward;
    public bool dashRight;
    public bool dashLeft;

    public bool isMovingBack;

    public float speed;
	void Start () {
        anim = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        Body = transform.FindChild("playerModel");
        target = GameObject.FindGameObjectWithTag("target").transform;
	}
	
	void FixedUpdate () {
        if (dashForward)
        {
            rigidBody.AddForce(transform.forward * speed);
            //transform.Translate(transform.forward * Time.deltaTime * forwardSpeed, Space.Self);
            if (Vector3.Distance(transform.position, Distination) < 1f)
            {
                speed = 0;
                //rigidBody.velocity = Vector3.zero;
               
                EndDash();
            }
        }

        if (dashBackward)
        {
            rigidBody.AddForce(transform.forward * speed);
            //transform.Translate(transform.forward * Time.deltaTime * forwardSpeed, Space.Self);
            if (Vector3.Distance(transform.position, Distination) < 1f)
            {
                speed = 0;
                //rigidBody.velocity = Vector3.zero;

                EndRollBack();
            }
        }

        if (dashRight)
        {
            Vector3 direction = Distination - transform.position;
            rigidBody.AddForce(direction * speed);
            //transform.Translate(transform.forward * Time.deltaTime * forwardSpeed, Space.Self);
            if (Vector3.Distance(transform.position, Distination) < 1f)
            {
                speed = 0;
                //rigidBody.velocity = Vector3.zero;

                EndDash();
            }
        }


        if (speed == 0)
        {
            rigidBody.velocity = Vector3.Lerp( rigidBody.velocity, Vector3.zero, Time.deltaTime * stopMoveSmooth);
        }
	}

    public void StepForward()
    {
        anim.SetTrigger("startDash");
        Invoke("StartDelayedDashFwd", dashDelay);
        
    }

    void StartDelayedDashFwd()
    {
        speed = forwardSpeed;
        dashForward = true;
        Distination = transform.position + new Vector3(0, 0, forwardDistanceMove);
    }

    private void EndDash()
    {
        anim.SetTrigger("endDash");
        dashForward = false;
        dashRight = false;

        transform.LookAt(target);
        Body.LookAt(target);
    }

    public void StepBackward()
    {
        speed = -backwardSpeed;
        anim.SetBool("rollBack", true);
        dashBackward = true;
        isMovingBack = true;
        Distination = transform.position - new Vector3(0, 0, backwardDistanceMove);

        //Invoke("HoldRollBack", 1.5f);
        CancelInvoke("DelayBackwardForCamera");
    }

   

    private void EndRollBack()
    {
        anim.SetBool("rollBack", false);
        dashBackward = false;
        if (IsInvoking("DelayBackwardForCamera"))
            CancelInvoke("DelayBackwardForCamera");
        Invoke("DelayBackwardForCamera", 1);
    }
    void DelayBackwardForCamera()
    {
        isMovingBack = false;
    }

    public void StepRight(){

        float playerTargetDistance = Vector3.Distance(target.position, transform.position);
        float zValue = (Mathf.Sin(rightMoveAngle * Mathf.Deg2Rad / 2)) * (Mathf.Sqrt(2 * Mathf.Pow(playerTargetDistance, 2) - 2 * Mathf.Pow(playerTargetDistance, 2)*(Mathf.Cos(rightMoveAngle))));
        // zValue = (sin rightMoveAngle/2 ) * k
        // k sq = 2 L sq - 2 L sq * cos rightMoveAngle;

        Distination = transform.position + new Vector3(rightDistanceMove, 0, zValue);

        //Vector3 radius = new Vector3(Vector3.Distance(target.position, transform.position)/2, 0, rightDistanceMove);
        //Quaternion rotation = transform.rotation;
        //rotation.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        //Distination = rotation * radius;
       // Distination.y = transform.position.y;

        //Distination = transform.position + new Vector3(rightDistanceMove, 0, 0);


        Vector3 direction = Distination - transform.position;

        Body.forward = direction;
        anim.SetTrigger("startDash");
        Invoke("StartDelayedDashRight", dashDelay);

    }

    void StartDelayedDashRight()
    {
        speed = rightSpeed;
        dashRight = true;

        
    }


    
}
