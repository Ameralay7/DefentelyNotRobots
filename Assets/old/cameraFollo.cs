using UnityEngine;
using System.Collections;

public class cameraFollo : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform playerFollow;
    [SerializeField]
    private float OffsetY;
    [SerializeField]
    private float target_Y_Offset = 5;
    [SerializeField]
    private float xAngle_maxClamp;
    [SerializeField]
    private float xAngle_minClamp;

    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private Vector3 offset = new Vector3(0f, 1.5f, 0f);
    [SerializeField]
	private float distanceUp;
    [SerializeField]
    public float targetingSmooth;
    [SerializeField]
    private float targettingRotationSmooth;
    [SerializeField]
    public float smooth;
    [SerializeField]
    private float rotationSmooth;

	// Use this for initialization
	void Start () {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("target").transform;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
       // if (CharacterMotorController.Instance.currentEnemyTarget)
        //if (target && (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputController>().CanLookAtTarget) 
        //    && !(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputController>().isRotating))
        //{
            //float rotXValue = playerFollow.rotation.eulerAngles.x;

            //if (rotXValue > xAngle_maxClamp)
            //{
            //    playerFollow.rotation = Quaternion.Euler(xAngle_maxClamp, playerFollow.rotation.eulerAngles.y, 0);
            //}
            ////if (rotXValue < xAngle_minClamp)
            ////{
            ////    playerFollow.rotation = Quaternion.Euler(xAngle_minClamp, playerFollow.rotation.eulerAngles.y, 0);
            ////}
            
            playerFollow.rotation = Quaternion.Slerp(playerFollow.rotation,
                                                          Quaternion.LookRotation(target.transform.position - playerFollow.position), 2 * Time.deltaTime);

            

            Vector3 direction = new Vector3(0, 0, -OffsetY);
            Quaternion rotation = Quaternion.Euler(playerFollow.eulerAngles.x + target_Y_Offset, playerFollow.eulerAngles.y, 0);

            

            Vector3 disPos = playerFollow.position + rotation * direction;
           

            transform.position = Vector3.Lerp(transform.position, disPos, Time.deltaTime * targetingSmooth);

            Quaternion targetRotation = Quaternion.LookRotation(playerFollow.transform.position - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, targettingRotationSmooth * Time.deltaTime);


            //transform.position, disPos;
            //transform.LookAt(playerFollow);

        //}
        //else if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputController>().isRotating)
        //{
        //    float rotXValue = playerFollow.rotation.eulerAngles.x;

        //    if (rotXValue > xAngle_maxClamp)
        //    {
        //        playerFollow.rotation = Quaternion.Euler(xAngle_maxClamp, playerFollow.rotation.eulerAngles.y, 0);
        //    }
        //    //if (rotXValue < xAngle_minClamp)
        //    //{
        //    //    playerFollow.rotation = Quaternion.Euler(xAngle_minClamp, playerFollow.rotation.eulerAngles.y, 0);
        //    //}

        //    playerFollow.rotation = Quaternion.Slerp(playerFollow.rotation,
        //                                                  Quaternion.LookRotation(target.transform.position - playerFollow.position), 2 * Time.deltaTime);



        //    Vector3 direction = new Vector3(0, 0, -OffsetY);
        //    Quaternion rotation = Quaternion.Euler(playerFollow.eulerAngles.x + target_Y_Offset, playerFollow.eulerAngles.y, 0);



        //    Vector3 disPos = playerFollow.position + rotation * direction;


        //    transform.position = Vector3.Lerp(transform.position, disPos, Time.deltaTime * smooth);

        //    Quaternion targetRotation = Quaternion.LookRotation(playerFollow.transform.position - transform.position);

        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmooth * Time.deltaTime);

        //}

        //else
        //{
        //    Quaternion targetRotation = Quaternion.LookRotation(playerFollow.transform.position - transform.position);

        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmooth * Time.deltaTime);

        //    //transform.LookAt(playerFollow);


        //    //Vector3 characterOffset = playerFollow.position + offset;

        //    //Vector3 lookDir;
        //    //lookDir = characterOffset - this.transform.position;
        //    //lookDir.y = 0;
        //    //lookDir.Normalize();

        //    //Vector3 targetPosition;
        //    //targetPosition = characterOffset + playerFollow.up * distanceUp - lookDir * distanceAway;

        //    ////CompensateForWalls(characterOffset, ref targetPosition);

        //    //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);



        //    ////smoothPosition(this.transform.position, targetPosition);
        //    //Quaternion targetRotation = Quaternion.LookRotation(playerFollow.transform.position - transform.position);


        //    //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);
        //}

	}
}
