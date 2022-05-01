using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject joystickMove, joystickLook, cam;
    public Rigidbody rb;
    public Animator anim;
    public GameObject lookingAt, walkPS;

    public float speed = 3;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 pos = Vector3.right * (joystickMove.transform.localPosition.x / joystickMove.GetComponent<RectTransform>().sizeDelta.x * speed) + Vector3.forward * (joystickMove.transform.localPosition.y / joystickMove.GetComponent<RectTransform>().sizeDelta.x * speed);
        Vector3 newPos = new Vector3(pos.x, rb.velocity.y, pos.z);
        rb.velocity = newPos;

        cam.transform.position = Vector3.Slerp(cam.transform.position, transform.position, 0.2f);

        if (joystickMove.transform.localPosition != Vector3.zero)
        {
            anim.SetBool("walk", true);
            walkPS.SetActive(true);
        }
        else
        {
            anim.SetBool("walk", false);
            walkPS.SetActive(false);
        }

        if (joystickLook.transform.localPosition != Vector3.zero) lookingAt = joystickLook;
        else lookingAt = joystickMove;

        Quaternion rot1 = transform.rotation;
        transform.LookAt(transform.position + new Vector3(lookingAt.transform.localPosition.x / lookingAt.GetComponent<RectTransform>().sizeDelta.x, 0, lookingAt.transform.localPosition.y / lookingAt.GetComponent<RectTransform>().sizeDelta.y));
        Quaternion rot2 = transform.rotation;
        transform.rotation = rot1;
        transform.rotation = Quaternion.Slerp(transform.rotation, rot2, 0.5f);

        Quaternion rott1 = walkPS.transform.rotation;
        walkPS.transform.LookAt(walkPS.transform.position + new Vector3(joystickMove.transform.localPosition.x / joystickMove.GetComponent<RectTransform>().sizeDelta.x, 0, joystickMove.transform.localPosition.y / joystickMove.GetComponent<RectTransform>().sizeDelta.y));
        Quaternion rott2 = walkPS.transform.rotation;
        walkPS.transform.rotation = rott1;
        walkPS.transform.rotation = Quaternion.Slerp(walkPS.transform.rotation, rott2, 0.5f);

        walkPS.transform.position = transform.position;
    }
}
