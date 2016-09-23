using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour {

    public bool inRangedMode;
    
    public Transform target;
    private Animator anim;

    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float forwardSpeed;
    [SerializeField]
    private float backwardDistance;
    [SerializeField]
    private float rightSpeed;
    [SerializeField]
    private float straf_Distance;


    private Vector3 nextPosition;
    private bool dashForward;
    private bool dashBackward;


    private float speed;
    private float strafSpeed;

    private float Z_beforebackwards;
    public float X_beforeRight;
    public float X_beforeLeft;

    public bool CanLookAtTarget;
    public bool isRotating;

	void Start () {
        if(!target)
         target = GameObject.FindGameObjectWithTag("target").transform;
        anim = GetComponent<Animator>();

        CanLookAtTarget = true;
	}

    public void DashForward()
    {
        if (speed != 0)
        {
            speed = 0;
            return;
        }
        if (!inRangedMode)
        {
            if (Vector3.Distance(nextPosition, transform.position) <= .5f)
            {
                return;
            }
            speed = forwardSpeed;
            nextPosition = target.FindChild("closerPoint").position;
            anim.SetFloat("Speed", speed);
        }
        else
        {
            if (Vector3.Distance(nextPosition, transform.position) <= .5f)
            {
                return;
            }
            speed = forwardSpeed;
            nextPosition = target.FindChild("fartherPoint").position;
            anim.SetFloat("Speed", speed);
        }

        
    }

    public void DashBackward()
    {
        
        if (speed != 0)
        {
            speed = 0;
            return;
        }

        

        speed = -forwardSpeed;
        Z_beforebackwards = backwardDistance;
        //nextPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z-backwardDistance);
     
        anim.SetFloat("Speed", speed);
    }


    public void DashRight()
    {
        

        if (strafSpeed != 0)
        {
            strafSpeed = 0;
            return;
        }

        CanLookAtTarget = false;
        transform.RotateAround(transform.position, transform.up, 90f);

        strafSpeed = rightSpeed;
        X_beforeRight = straf_Distance;
        //nextPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z-backwardDistance);

        anim.SetFloat("StrafSpeed", strafSpeed);
    }

    public void DashLeft()
    {

        if (strafSpeed != 0)
        {
            strafSpeed = 0;
            return;
        }

        CanLookAtTarget = false;
        transform.RotateAround(transform.position, transform.up, -90f);

        strafSpeed = rightSpeed;
        X_beforeLeft = straf_Distance;
        //nextPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z-backwardDistance);

        anim.SetFloat("StrafSpeed", strafSpeed);
    }

    void FixedUpdate()
    {
        Vector3 Direction = (target.position - transform.position).normalized;

        Vector3 MovmentVector = Vector3.zero;
        

        if (speed != 0)
        {
            MovmentVector += new Vector3(0, 0, speed);
            transform.Translate(MovmentVector * Time.deltaTime);
        }
            
        

        if (Vector3.Distance(nextPosition, transform.position) <= 1 )
        {
            speed = 0;
        }

        if (speed < 0)
        {
            Z_beforebackwards -= Time.deltaTime * forwardSpeed;
            if (Z_beforebackwards <= 0)
            {
                speed = 0;
            }
        }

        anim.SetFloat("Speed", speed);

        //right and left
        if (strafSpeed != 0)
        {
            
            MovmentVector += new Vector3(0, 0, strafSpeed);
            transform.Translate(MovmentVector * Time.deltaTime);
        }

        if (strafSpeed > 0)
        {
            X_beforeRight -= Time.deltaTime * rightSpeed;
            if (X_beforeRight <= 0)
            {
                if (IsInvoking("fff"))
                    CancelInvoke("fff");
                Invoke("fff", 5);
                CanLookAtTarget = true;
                isRotating = true;
                strafSpeed = 0;
            }
        }


        if (strafSpeed < 0)
        {
            X_beforeLeft -= Time.deltaTime * rightSpeed;
            if (X_beforeLeft <= 0)
            {
                if (IsInvoking("fff"))
                    CancelInvoke("fff");
                Invoke("fff", 5);
                CanLookAtTarget = true;
                isRotating = true;
                strafSpeed = 0;
            }
        }

        anim.SetFloat("StrafSpeed", strafSpeed);


        if (target && CanLookAtTarget)
        {
            
            Vector3 newRot = new Vector3(target.position.x, transform.position.y, target.position.z);
            Quaternion targetRotation = Quaternion.LookRotation(newRot - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            //transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

        }



    }

    void fff()
    {
        //CanLookAtTarget = true;
        isRotating = false;
    }
}
