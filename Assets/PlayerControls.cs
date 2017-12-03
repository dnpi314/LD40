using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour 
{
  float rotationY;
  float rotationX;
  Vector3 moveVector;
  Vector2 moveInput;
  CharacterController controller;
  bool inWater = false;
  float timeUnderwater = 0;
  float drownTime = 30;

  public float mouseSensitivity = 1f;
  public float moveSpeed;
  public float swimSpeed;
  public float jumpPower;
  public float gravity;
  public Transform water;
  public UnderwaterEffect underwaterEffect;
  public LayerMask selectableLayer;
  public GameObject indicator;

  public static int engineParts = 0;
  public static bool underwater = false;

	void Start () 
	{
    controller = GetComponent<CharacterController>();
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
	}
	
	void Update () 
	{
    if (water.position.y > transform.position.y)
    {
      inWater = true;
    }
    else
    {
      inWater = false;
    }

    if (underwater)
    {
      timeUnderwater += Time.deltaTime;
      if (timeUnderwater >= drownTime)
      {
        Scenes.GameOver("Game Over. You drowned");
      }
    }
    else
    {
      timeUnderwater = 0;
    }

    //rotation
    rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
    rotationY += Input.GetAxis("Mouse X") * mouseSensitivity;

    if (rotationX <= -90)
    {
      rotationX = -90;
    }
    if (rotationX >= 80)
    {
      rotationX = 80;
    }
    Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    transform.rotation = Quaternion.Euler(0, rotationY, 0);

    //movement
    moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    moveInput.Normalize();
    if (!inWater)
    {
      moveVector = transform.TransformDirection(new Vector3(moveInput.x * moveSpeed, moveVector.y, moveInput.y * moveSpeed));
      if (controller.isGrounded)
      {
        moveVector.y = 0;
        if (Input.GetButton("Jump"))
        {
          moveVector.y = jumpPower;
        }
      }
      moveVector.y -= gravity * Time.deltaTime;
      controller.Move(moveVector * Time.deltaTime);
    }
    else
    {
      moveVector = Camera.main.transform.TransformDirection(new Vector3(moveInput.x * swimSpeed, 0, moveInput.y * swimSpeed));
      controller.Move(moveVector * Time.deltaTime);
    }

    if ((water.position.y - transform.position.y) > 0.6f)
    {
      underwater = true;
      underwaterEffect.enabled = true;
    }
    else
    {
      underwater = false;
      underwaterEffect.enabled = false;
    }

    //Select Objects
    Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
    RaycastHit hitInfo;
    if(Physics.Raycast(ray, out hitInfo, 3, selectableLayer))
    {
      if(hitInfo.collider.tag == "Leak")
      {
        indicator.SetActive(true);

        if (Input.GetButton("Interact"))
        {
          int leakIndex;
          string leakName = hitInfo.collider.name;
          if(leakName == "Leak")
          {
            leakIndex = 0;
          }
          else
          {
            char[] trimChars = { 'L', 'e', 'a', 'k', ' ', '(', ')' };
            leakName = leakName.Trim(trimChars);
            leakIndex = int.Parse(leakName);
          }
          water.GetComponent<WaterRising>().RemoveLeak(leakIndex);
        }
      }

      else if (hitInfo.collider.tag == "Part")
      {
        indicator.SetActive(true);

        if (Input.GetButton("Interact"))
        {
          Destroy(hitInfo.collider.gameObject);
          engineParts++;
        }
      }

      else if (hitInfo.collider.tag == "Engine")
      {
        if (engineParts >= 5)
        {
          indicator.SetActive(true);

          if (Input.GetButton("Interact"))
          {
            Scenes.GameOver("Victory!");
          }
        }
        else
        {
          indicator.SetActive(false);
        }
      }

      else
      {
        indicator.SetActive(false);
      }
    }

    else
    {
      indicator.SetActive(false);
    }
	}
}
