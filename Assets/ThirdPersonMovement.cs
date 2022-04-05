using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    // Start is called before the first frame update
 	public CharacterController controller;
	public Transform cam;
	public float speed = 20f;
	public float turnSmoothTime = 0.1f;
	float turnSmoothVelocity;
	private float verticalVelocity;
	private float gravity = 40.0f;
	private float jumpForce = 25.0f;
	
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
		
		if(direction.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
			Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			controller.Move(moveDir.normalized * speed * Time.deltaTime);
		}
		
		if(controller.isGrounded)
		{
			verticalVelocity = -gravity * Time.deltaTime;
			if (Input.GetButtonDown("Jump") || Input.GetButton("Jump"))
			{
				verticalVelocity = jumpForce;
			}
		}
		else
		{
			verticalVelocity -= gravity *Time.deltaTime;
		}
		Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
		controller.Move(moveVector * Time.deltaTime);
		
    }
}
