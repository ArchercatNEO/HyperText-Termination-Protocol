using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Mouse : MonoBehaviour
{
    [SerializeField] float sensitivity = 4;
    [SerializeField] float clickDistance = 10;
    [SerializeField] GameObject gunBarrel;
    [SerializeField] GameObject bullet; 
    [SerializeField] GameObject mainCamera;

    void HandleHovering(RaycastHit ray) 
    {
        Server server = ray.collider.gameObject.GetComponent<Server>();
        // We hit something it just wasn't a server.
        if (server == null) return;

        server.DisplayHover();

        if (Input.GetMouseButtonDown(1))
        {
            server.OnClick();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        float turnX = Input.GetAxis("Mouse X") * sensitivity;
        float turnY = Input.GetAxis("Mouse Y") * sensitivity;

        Vector3 vector = transform.rotation.eulerAngles;
        
        vector.x -= turnY;
        vector.y += turnX;
        vector.z = 0;

        transform.rotation = Quaternion.Euler(vector);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bul = Instantiate(bullet)
                .GetComponent<Bullet>()
                .SetDirection(transform.forward);
            
            bul.transform.position = gunBarrel.transform.position;
            Vector3 vector3 = gunBarrel.transform.eulerAngles;
            vector3.x += 90;
            bul.transform.rotation = Quaternion.Euler(vector3);
            bul.GetComponent<Bullet>().Port(new byte[]{123, 32, 45, 45}, 60);
        }


        // Cast a ray out from the camera? and if something got hit try to get a hover message.
        bool hit = Physics.Raycast(mainCamera.transform.position, transform.forward, out RaycastHit ray, clickDistance);
        if (hit) HandleHovering(ray);
        else UI.ChangeHover("");
        
    }
}
