using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class weaponController : MonoBehaviour
{
    public GameObject projectile;
    public float projectileVelocity = 10.0f;
    private Camera mainCam;
    private GameManager gameManager;
    private float nextFire = 2f;
    public float fireRate;
    public float burstLimit;
    private float burstCounter;
    public float burstRate;
    public CameraShake cameraChake;
    public LineRenderer aimHelper;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        nextFire = Time.fixedTime;
        GameObject aimHelperGameObject = new GameObject("AimHelper", typeof(LineRenderer));
        aimHelperGameObject.transform.SetParent(gameObject.transform);
        aimHelperGameObject.transform.position = gameObject.transform.position;
        aimHelper = aimHelperGameObject.GetComponent<LineRenderer>();
        aimHelper.startColor = Color.blue;
        aimHelper.endColor = Color.blue;
        aimHelper.startWidth = 0.01f;
        aimHelper.endWidth = 0.01f;
        aimHelper.SetPosition(0, transform.position);
        aimHelper.sortingLayerName = "aimHelper";

    }

    // Update is called once per frame

    void FixedUpdate()
    {
        Vector3 mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePositionFlat = new Vector2(mousePosition.x,mousePosition.y);
        

        RaycastHit2D hit = Physics2D.Raycast(mainCam.transform.position, mousePositionFlat);
        Debug.DrawLine(mainCam.transform.position, mousePosition, Color.black);
        if (hit.collider != null)
        {
            AimHelperUpdate(hit.point);
        }
        else {
            AimHelperUpdate(mousePositionFlat);
        }

        Vector3 rotation = mousePosition - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        if (Input.GetButton("Fire1"))
        {
            if (nextFire < Time.fixedTime)
            {
                LaunchProjectil(mousePosition,rotation);
                nextFire = Time.fixedTime + (burstCounter >= burstLimit ? burstRate : fireRate );
                burstCounter = burstCounter >= burstLimit ? 0 : burstCounter + 1;
                cameraChake.StartSake();
            }
            
        }
        
    }

    private void AimHelperUpdate(Vector3 endPosition)
    {
        aimHelper.SetPosition(0, transform.position);
        aimHelper.SetPosition(1, endPosition);
    }

private void LaunchProjectil(Vector3 mousePosition, Vector3 rotation)
    {
        Vector3 initProjectilePosition = transform.position + (rotation.normalized * transform.localScale.x / 2);
        Vector3 direction = mousePosition - transform.position;
        Vector2 projectionVelocity = new Vector2(direction.x, direction.y).normalized * projectileVelocity;
        GameObject ball = GetProjectil(initProjectilePosition);
        ball.SetActive(true);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.velocity = projectionVelocity;
    }

    private GameObject GetProjectil(Vector3 initialPosition)
    {
        GameObject projectil;
        if (gameManager.pool.Count < 1)
        {
            projectil = Instantiate(projectile, initialPosition, transform.rotation);
        }
        else
        {
            projectil = gameManager.pool.Dequeue();
            projectil.transform.position = initialPosition;

        }
        return projectil;
    }
}
