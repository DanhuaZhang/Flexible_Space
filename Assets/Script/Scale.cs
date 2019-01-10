using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour {
    public GameObject room1;
    public GameObject room2;
    public GameObject corner1;
    public GameObject corner2;
    public GameObject door1;
    public GameObject door2;
    public GameObject ground1;

    private Vector3 worldoffset;
    private Vector3 localoffset;
    private Vector3 initposition;
    private Vector3 lastposition_x;
    private Vector3 realposition;
    private Vector3 lastrealpos_x;
    private Vector3 lastposition_z;
    private Vector3 lastrealpos_z;

    private bool showroom2;
    private bool showroom1;
    private Vector3 origin1;
    private float scale;

	// Use this for initialization
	void Start () {
        origin1 = Vector3.zero;
        //worldoffset = new Vector3(-1, 0, 0);
        Transform eye = this.transform.Find("Camera (eye)");
        if (eye != null)
        {
            //lastrealpos_x = eye.position;
            //eye.position += worldoffset;
        }
        else
        {
            print("Cannot find Camera (eye)");
        }

        //this.transform.position = room1.transform.position + worldoffset;
        //lastposition_x = this.transform.position;
        initposition = this.transform.position;
        scale = 0.4f;
        lastposition_z = origin1;
        lastrealpos_z = origin1;
        lastposition_x = corner1.transform.position + new Vector3(1.8f, 0, 0);
        //this.transform.position -= new Vector3(0, 0.5f, 0);
    }
	
	// Update is called once per frame
	void LateUpdate () {
        Transform eye = this.transform.Find("Camera (eye)");
        if (eye != null)
        {
            //eye.localPosition = Vector3.zero;
            
            print("eye position: " + eye.position);
            print("eye local position: " + eye.localPosition);
            //this.transform.position = eye.position;
        }
        else
            print("Cannot find Camera (eye)");

        // in room1, start slope
        if (eye.transform.position.z < door1.transform.position.z)
        {
            float depth = Mathf.Abs(eye.transform.position.z - door1.transform.position.z);
            this.transform.position = initposition + new Vector3(0, depth * Mathf.Cos(75 * Mathf.PI / 180), 0);
            //print("lastposition_x:" + lastposition_x);
            //print("this.position:" + this.transform.position);

        }

        //scale z
        //float z_offset = lastposition_x.z + scale * (eye.position.z - lastrealpos_x.z);
        //transform.position = eye.transform.position + new Vector3(0, 0, z_offset);
        float delta = eye.position.z - lastposition_z.z;
        this.transform.position += new Vector3(0, 0, delta * scale);
        //transform.position = (lastposition_x) * scale;

        lastposition_z = eye.position;


        //scale x
        float x_offset = eye.position.x - lastposition_x.x;
        this.transform.position += new Vector3(x_offset * scale, 0, 0);
        //transform.position = (lastposition_x) * scale;

        lastposition_x = eye.position;

        if (eye.position.x >= corner1.transform.position.x)
        {
            print("room1");
            room2.active = false;
            room1.active = true;
            ground1.active = true;
            
        }
        else if(eye.position.x <= corner2.transform.position.x)
        {
            room2.active = true;
            room1.active = false;
            ground1.active = false;
        }
    }
}
;