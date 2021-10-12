using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class PlayerControler : MonoBehaviour {
	
	private float speed = 1.5f;

	[SerializeField]private GameObject DeathOverlay;
	[SerializeField]private Transform head;
	private Transform PlayerTransform; 

	private float sensitivity;
	private float headMinY = -40f; 
	private float headMaxY = 40f;

	private float jumpForce = 10; 
	private float jumpDistance = 1.2f;

	private Vector3 direction;
	private float h, v;
	private int layerMask;
	private Rigidbody body;
	private float rotationY;
	private bool Ground;

	private float SpeedCoofficient = 10f;

	private bool IsDead;
	public bool CanMove;
	
	void Start () 
	{
		CanMove = true;
		sensitivity = 2f;
		PlayerTransform = GetComponent<Transform>();
		body = GetComponent<Rigidbody>();
		body.freezeRotation = true;
		layerMask = 1 << gameObject.layer | 1 << 2;
		layerMask = ~layerMask;

		if(Advertisement.isSupported)
		{
			Advertisement.Initialize("3670077", false);// тут надо вместо 3670077 указать наш AppID 
		}
	}
	
	void FixedUpdate()
	{
		if(!IsDead && CanMove)
		{
			body.AddForce(direction * speed, ForceMode.VelocityChange);

			if(Mathf.Abs(body.velocity.x) > speed)
			{
				body.velocity = new Vector3(Mathf.Sign(body.velocity.x) * speed, body.velocity.y, body.velocity.z);
			}
			if(Mathf.Abs(body.velocity.z) > speed)
			{
				body.velocity = new Vector3(body.velocity.x, body.velocity.y, Mathf.Sign(body.velocity.z) * speed);
			}
		}
	}

	bool GetJump() 
	{
		RaycastHit hit;
		Ray ray = new Ray(transform.position, Vector3.down);
		if (Physics.Raycast(ray, out hit, jumpDistance))
		{
			return true;
			Ground = true;
			
		}
		else
		{
			return false;
			Ground = false;
		}
	}

	void Update () 
	{
		if(!IsDead && CanMove)
		{
			h = SimpleInput.GetAxis("Horizontal") * SpeedCoofficient;
			v = SimpleInput.GetAxis("Vertical") * SpeedCoofficient;

			float rotationX = PlayerTransform.localEulerAngles.y + SimpleInput.GetAxis("Horizontal2") * sensitivity;
			rotationY += SimpleInput.GetAxis("Vertical2") * sensitivity;
			rotationY = Mathf.Clamp (rotationY, headMinY, headMaxY);
			head.localEulerAngles = new Vector3(-rotationY, 0, 0);
			PlayerTransform.localEulerAngles = new Vector3(0, rotationX, 0);

			direction = new Vector3(h, 0, v);
			direction = head.TransformDirection(direction);
			direction = new Vector3(direction.x, 0, direction.z);
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, Vector3.down * jumpDistance);
	}

	public void Jump()
	{
		if(GetJump() && !IsDead && CanMove)
		{
			body.velocity = new Vector2(0, jumpForce * 300);
		}
	}

	public void Die()
	{
		IsDead = true;
		DeathOverlay.SetActive(true);
		ShowAD();
	}

	public void Spawn()
	{
		IsDead = false;
		CanMove = true;
		DeathOverlay.SetActive(false);
	}

	private void ShowAD()
	{
		Advertisement.Show("video");
	}
}

