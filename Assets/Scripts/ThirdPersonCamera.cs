using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

    public Transform follow;
    public Transform top;

    public Transform target;

    public float distanceAway;
    public float distanceUp;
    public float distanceRight;
    public float smooth;
    public float flipSmooth;

    public float Yoffset;


	void Start () {
	
	}
	
	void FixedUpdate () {
        if (follow)
        {
            follow.LookAt(target);


            Vector3 targetPosition = follow.position + follow.up * distanceUp - follow.forward * distanceAway + follow.right * distanceRight;

            Debug.DrawLine(follow.position, target.position, Color.blue);
            //Debug.DrawLine(follow.position, follow.parent.position, Color.yellow);
            Debug.DrawLine(follow.position, transform.position, Color.red);
            Debug.DrawLine(top.position, target.position, Color.green);

            Debug.DrawLine(top.position, follow.position, Color.yellow);



            
            //////
            Vector3 radius = new Vector3(distanceRight, Yoffset,0);
            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(0, follow.eulerAngles.y, 0);

            follow.position = Vector3.Lerp(follow.position, rotation * radius, Time.deltaTime * smooth);

           
            ///////

            Vector3 PlayerForward = follow.parent.forward.normalized;
            Vector3 PlayerToTargetDir = target.parent.position - follow.parent.position;

            Vector3 PlayerToFollowDirection = follow.position - follow.parent.position;



            Vector3 temp1 = transform.forward;
            Vector3 temp2 = follow.forward;
            //test
            //Vector3.OrthoNormalize(ref temp1, ref temp2);

            transform.forward = temp1;
            follow.forward = temp2;



            if (GetDirectionSign(PlayerForward, PlayerToTargetDir) > 0)
            {//other player on the right

                distanceRight *= (distanceRight > 0) ? 1 : -1;

                Vector3 temp = follow.position;

                temp.x *= (temp.x >= 0) ? 1 : -1;

                follow.position = Vector3.Lerp(follow.position, temp, Time.deltaTime * flipSmooth);

                //follow.position = temp;
            }
            else if (GetDirectionSign(PlayerForward, PlayerToTargetDir) < 0)
            {//other on the left
                distanceRight *= (distanceRight > 0) ? -1 : 1;
                Vector3 temp = follow.position;

                temp.x *= (temp.x >= 0) ? -1 : 1;

                follow.position = Vector3.Lerp(follow.position, temp, Time.deltaTime * flipSmooth);

                //follow.position = temp;
            }


            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);




            transform.LookAt(follow);
        }
	}

    private float GetDirectionSign(Vector3 from, Vector3 to)
    {
        return Vector3.Cross(from, to).y;
    }
}
