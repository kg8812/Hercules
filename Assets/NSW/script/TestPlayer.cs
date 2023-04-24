using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    CharacterController cc;
    public Transform npc = null;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 move = Vector3.zero;

        move.x = Input.GetAxis("Horizontal");
        move.z = Input.GetAxis("Vertical");
        move.y = 0;

        cc.Move(move * Time.deltaTime*10f);
        if(Input.GetMouseButtonUp(0) && IsNpc())
        {
            // npc.GetComponent<NPC>().SayDialog(this.transform);
            npc.SendMessage("SayDialog", this.transform,SendMessageOptions.DontRequireReceiver);
            Debug.Log("NPC추출");
        }
    }

    //마우스 포지션 검출

    bool IsNpc()
    {
        bool isNpc = false;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray.origin,ray.direction,out hit))
        {
            if (hit.transform.tag == "Npc")
            {
                isNpc = true;
                npc = hit.transform;
            }
            else
            {
                isNpc = false;
            }
        }
        

        return isNpc;
    }
}
