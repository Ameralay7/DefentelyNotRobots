using UnityEngine;
using System.Collections;

public class NewCameraFollow : MonoBehaviour {

    public Transform Target;

    public Transform Follow;

    public float rotSpeed = 2;
    public float cameraMoveDamp = 2;
    public float Yoffset = 3;
    public float Xoffset = 4;
    public float Zoffset = 4;

    public float X_moveOffset=  2;

    public float angleOffset = 20;

	void Start () {
        lastAngle = Quaternion.Angle(Follow.rotation, Target.rotation);
	}
    float lastAngle;
	void Update () {

        //1. Make the Gameobject on the player (the follow) look at the target smoothly
        Follow.rotation = Quaternion.Slerp(Follow.rotation, Quaternion.LookRotation(Target.position - Follow.position), Time.deltaTime * rotSpeed);

        

        //2. Make the Camera move arround the player in the taget location smoothly
        Quaternion RotationValue = Quaternion.Euler(Follow.rotation.eulerAngles.x, Follow.rotation.eulerAngles.y , 0); 

        //Debug.Log(RotationValue);

        Vector3 cameraDistination;

        //if(RotationValue.eulerAngles.z >= 0)
        //    cameraDistination = Follow.position + RotationValue * new Vector3 (Xoffset, Zoffset, -Yoffset);
        //else
        //{
        //    cameraDistination = Follow.position + RotationValue * new Vector3(-Xoffset, Zoffset, -Yoffset);
        //}

        //Xoffset = Xoffset * Quaternion.Angle(Follow.rotation, Target.rotation) / lastAngle;
        //X_moveOffset = X_moveOffset * Quaternion.Angle(Follow.rotation, Target.rotation) / lastAngle;

        //Debug.Log("Dist =  " + Vector3.Distance(Follow.parent.position, Target.position) + "  Angle =  " + Quaternion.Angle(Follow.rotation, Target.rotation));

        //lastAngle = Quaternion.Angle(Follow.rotation, Target.rotation);



        cameraDistination = Follow.position + RotationValue * new Vector3(Xoffset, Zoffset, -Yoffset);




        //Follow.position = Vector3.Lerp(Follow.position,  new Vector3(X_moveOffset, Follow.position.y, Follow.position.z), Time.deltaTime * cameraMoveDamp);




        transform.position = Vector3.Lerp(transform.position, cameraDistination , cameraMoveDamp * Time.deltaTime);


        //3. Make the Camera rotate at the target rotation (to look at the player follow object)

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Follow.position - transform.position), Time.deltaTime * rotSpeed);

        //Lets see what affects

        //Debug.Log("Dist =  " + Vector3.Distance(Follow.parent.position, Target.position) +"  Angle =  " + Quaternion.Angle(Follow.rotation, Target.rotation));

        Debug.DrawLine(transform.position, Follow.position, Color.red);
        Debug.DrawLine(Follow.position, Target.position, Color.blue);
	}
}
