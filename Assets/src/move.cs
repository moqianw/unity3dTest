using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class move : MonoBehaviour
{
    public Rigidbody body;
    GameObject obj;
    private float dtime = 0.0f;
    private short fps = 0;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        obj = this.gameObject;

        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        getFPS();
        //keymouthevent();
        var transform = obj.transform;

        float horizontal = Input.GetAxis("Horizontal"); // A/D »ò ×ó/ÓÒ·½Ïò¼ü
        float vertical = Input.GetAxis("Vertical");
        bool jump = Input.GetButtonDown("Jump");
        float space = jump ? 1.0f : 0.0f;
        body.Move(new Vector3(transform.position.x + 2 * horizontal * Time.deltaTime
            , transform.position.y + space * Time.deltaTime
            , transform.position.z + 2 * vertical * Time.deltaTime
            ), transform.rotation);
        if (Input.GetMouseButtonDown(0))
        {
            animator.Play("right");
        }
        if (Input.GetMouseButtonDown(1))
        {
            animator.Play("left");
        }

    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.gameObject.name + " enter");
    //}
    //private void OnCollisionStay(Collision collision)
    //{
    //    Debug.Log(collision.gameObject.name);
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    Debug.Log(collision.gameObject.name + " exit");
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    var car = GameObject.Find("FNAL_MODEL");
    //    if (car != null)
    //    {
    //        car.SetActive(false);
    //        car.SetActive(true);
    //    }
    //}
    //private void OnTriggerStay(Collider other)
    //{
        
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    var car = GameObject.Find("FNAL_MODEL");
    //    if (car != null)
    //    {
            
    //    }
    //}
    private void keymouthevent()
    {
        var transform = obj.transform;
        if (Input.GetMouseButton(0))
        {
            var x = Input.GetAxis("Mouse X");
            var y = Input.GetAxis("Mouse Y");
            transform.Rotate(Vector3.up, x);
            transform.Rotate(Vector3.right, - y);
        }
        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.position += transform.forward * 0.1f;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    transform.position -=  transform.forward * 0.1f;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.position -=  transform.right * 0.1f;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.position += transform.right * 0.1f;
        //}
    }
    private void getFPS()
    {
        fps++;
        dtime += Time.deltaTime;
        if (dtime >= 1.0f)
        {
            Debug.Log(fps.ToString());
            fps = 0;
            dtime = 0.0f;
        }
    }
    // Update is called once per frame

    private IEnumerator loadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        yield return operation;
    }
}
