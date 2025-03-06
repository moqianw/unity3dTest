using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class move : MonoBehaviour
{
    private CharacterController player;
    GameObject obj;
    private float dtime = 0.0f;
    private short fps = 0;
    // Start is called before the first frame update
    void Start()
    {
        obj = this.gameObject;
        player = GetComponent<CharacterController>();


    }

    void Update()
    {
        getFPS();
        keymouthevent();
        float horizontal = Input.GetAxis("Horizontal"); // A/D »ò ×ó/ÓÒ·½Ïò¼ü
        float vertical = Input.GetAxis("Vertical");
        bool jump = Input.GetButtonDown("Jump");
        float space = jump ? 1.0f : 0.0f;
        player.SimpleMove(new Vector3(horizontal, space, vertical));
    }
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
