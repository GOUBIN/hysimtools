using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HRT_ThirdPersonCameraFlow : MonoBehaviour
{
    public Transform player;
    private Vector3 offsetPosition ;//位置偏移
    private bool isRotating = false;

    private Vector3 offest = Vector3.zero;
    public float distance = 0;
    public float scrollSpeed = 10;
    public float rotateSpeed = 2;

    // Use this for initialization
    void Start()
    {
        offest = new Vector3(0, 1.6f, 0);  
        
        if (player != null)
        {
            

        transform.LookAt(player.position);//初始化相机,让相机朝着玩家
        offsetPosition = transform.position - player.position;//相机位置-玩家位置
        }
    }



    bool isSet = false;
    // Update is called once per frame
    void Update()
    {


        if (player != null)
        {
            if (isSet==false)
            {
                transform.LookAt(player.position+ offest);//初始化相机,让相机朝着玩家
                offsetPosition = transform.position - player.position;//相机位置-玩家位置
                isSet = true;
            }

            transform.position = offsetPosition + player.position;//偏移量+玩家位置
            RotateView();
            ScrollView();
        }
         
    }

    /// <summary>
    /// 处理视野的拉近和拉远效果
    /// </summary>
    void ScrollView()
    {
        //print(Input.GetAxis("Mouse ScrollWheel"));//向前 返回负值 (拉近视野) 向后滑动 返回正值(拉远视野)
        distance = offsetPosition.magnitude;
        distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        distance = Mathf.Clamp(distance,1, 10);
        offsetPosition = offsetPosition.normalized * distance;
    }


    /// <summary>
    /// 处理视野的旋转
    /// </summary>
    void RotateView()
    {
        //Input.GetAxis("Mouse X");//得到鼠标在水平方向的滑动
        //Input.GetAxis("Mouse Y");//得到鼠标在垂直方向的滑动
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            transform.RotateAround(player.position+ offest, player.up, rotateSpeed * Input.GetAxis("Mouse X"));

            Vector3 originalPos = transform.position;
            Quaternion originalRotation = transform.rotation;

            transform.RotateAround(player.position + offest, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));//影响的属性有两个 一个是position 一个是rotation
            float x = transform.eulerAngles.x;
            if (x < 5 || x > 80)
            {//当超出范围之后，我们将属性归位原来的，就是让旋转无效
                transform.position = originalPos;
                transform.rotation = originalRotation;
            }

        }

        offsetPosition = transform.position - player.position;
    }

}
