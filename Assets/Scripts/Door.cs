// Written by Amin Umer
// 11/5/2024
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Door : MonoBehaviour
//{
//    public float interactionDistance;
//    public GameObject intText;
//    public string DoorOpenAnimName, DoorCloseAnimName;
//
//    private void Update()
//    {
//        Ray ray = new Ray(transform.position, transform.forward);
//        RaycastHit hit;
//
//        if (Physics.Raycast(ray, out hit, interactionDistance))
//        {
//            if (hit.collider.gameObject.tag == "Door")
//            {
//                GameObject doorParent = hit.collider.transform.root.gameObject;
//                Animator doorAnim = doorParent.GetComponent<Animator>();
//                intText.SetActive(true);
//                if (Input.GetKeyDown(KeyCode.E))
//                {
//                    if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(DoorOpenAnimName))
//                    {
//                        doorAnim.ResetTrigger("Open");
//                        doorAnim.SetTrigger("Close");
//                    }
//                    if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(DoorCloseAnimName))
//                    {
//                        doorAnim.ResetTrigger("Close");
//                        doorAnim.SetTrigger("Open");
//                    }
//                }
//            }
//            else
//            {
//                intText.SetActive(false);
//            }
//        }
//        else
//        {
//            intText.SetActive(false);
//        }
//    }
//}
