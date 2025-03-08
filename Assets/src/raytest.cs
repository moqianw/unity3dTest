using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raytest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool res = Physics.Raycast(ray, out hit);
            if (res)
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }
    }
}
