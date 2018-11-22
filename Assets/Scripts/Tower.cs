using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private int direction = -1;

    private float lastBulletTime;
    private float defaultRotationSpeed;

    // public Bullet prefab; // don't work
    public Transform startBulletPosition;
    public Player player;
    public GameObject prefab;

    public float period = 5f;
    public float rotationSpeed = 0f;
    public float drawOffset = 1.5f;
    public float maxDistance = 10;
    public float maxAngle = 30f;
    public float bulletSpeed = 10f;

    // Use this for initialization
    void Start()
    {
        lastBulletTime = 0;
		defaultRotationSpeed = rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isAlive){
        Tracking2();
        DrawLines();
		StartRotating();
        }else return;
    }

    void Tracking2()
    {
        // transform.right - red axis
        Vector3 directionToTarget = transform.position - player.transform.position; // distance
        float angle = Vector3.Angle(transform.right, directionToTarget); // angle between player and tower
        float distance = directionToTarget.magnitude; // length of vector
        print(angle);

		// if player is visible
        if (Mathf.Abs(angle) < maxAngle && distance < maxDistance)
        {
			rotationSpeed = 0; // stop rotation
            StartShooting(); // start shooting
        }
		else {
			rotationSpeed = defaultRotationSpeed; // start rotation
		}
    }

    void StartShooting()
    {   
	lastBulletTime += Time.deltaTime;

        if (lastBulletTime > period)
        {
            var newBullet = Instantiate(prefab, startBulletPosition.transform.position, transform.rotation);
        	newBullet.GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * bulletSpeed; // distance between player and tower, and bullet speed
			lastBulletTime = 0;
        }
    }

    void StartRotating()
    {
        float rotation = rotationSpeed * Time.deltaTime * direction;
        Vector3 rotationVector = new Vector3(0, 0, maxAngle);
        transform.Rotate(rotationVector * rotation);
    }

	// Debug lines
    void DrawLines()
    {
        var line = transform.position + ((transform.right * -1) * maxDistance);
        var rotatedLine = Quaternion.AngleAxis(0, transform.up) * line;
        Debug.DrawLine(transform.position, rotatedLine, Color.red);

        var lineTop = transform.position + ((transform.right * -1) * 25); // 25 - length offset, don't work correctly
        var rotatedLineTop = Quaternion.AngleAxis((maxAngle * drawOffset) * -1, transform.forward) * lineTop;
        Debug.DrawLine(transform.position, rotatedLineTop, Color.blue);

        var lineBottom = transform.position + ((transform.right * -1) * 25);
        var rotatedLineBottom = Quaternion.AngleAxis(maxAngle * drawOffset, transform.forward) * lineBottom;
        Debug.DrawLine(transform.position, rotatedLineBottom, Color.blue);
    }


}