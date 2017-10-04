using System;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : NetworkBehaviour {
    [SerializeField]
    [Range(0,100)]
    private float speed = 20f;
    private Camera cam;
    public GameObject pew;
    public Transform sp;
    public float bulletSpeed;
    private Vector3 camOffset;
    private float cooldown = 0.5f;
    private float curTime = 0f;
    bool canShoot = true;

    // Use this for initialization
    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
    }
    void Start () {

        name = "Bro " + GetComponent<NetworkIdentity>().netId.ToString();
        if (!isLocalPlayer)
        {
            return;
        }
        
        cam = Camera.main;
        cam.transform.position = new Vector3(transform.position.x, 20f, transform.position.z);
        camOffset = transform.position - cam.transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!isLocalPlayer)
        {
            return;
        }

        if (canShoot==false)
        {
            curTime += Time.fixedDeltaTime;
        }
        if (curTime >= cooldown)
        {
            canShoot = true;
            curTime = 0;
        }
        BodyMove();
        BodyRotation();
        CameraFollow();
        if (canShoot) {
            
            if (Input.GetMouseButtonDown(0))
            {
                canShoot = false;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    CmdSpawnBomb(hit.point);
                }
            }
        }

    }

    [Command]
    private void CmdSpawnBomb(Vector3 point)
    {

            var ob = Instantiate(pew, sp.position, sp.rotation);
            ob.GetComponent<Rigidbody>().velocity = ob.transform.forward * bulletSpeed;
            NetworkServer.Spawn(ob);
            Destroy(ob, 3);

    }




    void BodyMove()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        var move = new Vector3(x, 0, y) * Time.fixedDeltaTime * speed;
        transform.Translate(move, Space.World);
    }

    void BodyRotation()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            transform.LookAt(hit.point);
        }
    }

    void CameraFollow()
    {
        cam.transform.position = transform.position - camOffset;
        var rot = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(0f, rot.y, 0f);
    }
}
