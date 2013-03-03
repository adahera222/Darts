using UnityEngine;
using System.Collections;

public class DartThrow : MonoBehaviour 
{
    public GameObject Score, Board;
	public float DragSpeed, ThrowSpeed, RotationSpeed;
	int[] scores =  {20, 1, 18, 4, 13, 6, 10, 15, 2, 17, 3, 19, 7, 16, 8, 11, 14, 9, 12, 5};
	int points = 0;

	
	void Start () 
	{
	}
	
	void Update () 
	{
		if ((Input.GetMouseButton(0)) & !CompareTag("Finish")) 					
			if (Input.GetMouseButton(1))
			{
				Vector3 angles = new Vector3 (-Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"), 0);
				transform.Rotate(angles*RotationSpeed*Time.deltaTime);
			} 
			else
			{
				Vector3 direction = new Vector3 (Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0);
				transform.Translate(direction*DragSpeed*Time.deltaTime);
			}
	}
	
	void FixedUpdate () 
	{
		if (Input.GetMouseButtonUp(0)) 
			rigidbody.AddRelativeForce(Vector3.forward*ThrowSpeed, ForceMode.Impulse);
	}
	
	void OnCollisionEnter()
	{
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		AddPoints();
		tag = "Finish";
	}

	private
		
	void AddPoints() 
	{
		points += CountPoints(); 
		Score.guiText.text = points.ToString();
	}
		
	int CountPoints() 
	{
		Vector2 v = new Vector2 (transform.position.x, transform.position.y);
		
		//sector
		float a = Vector2.Angle(Vector2.up, v);
		if (( v.x < 0 ) && ( a > 9 )) 
			a = 360 - a;
		float b = (a+9)/18;
		int i = Mathf.FloorToInt(b);
		int result = scores[i];
		
		//distance
		float d = Vector2.Distance(Vector2.zero, v)/Board.transform.localScale.x;
		d *= 200;
		
		if (d < 80) {
			if (d < 72) {
				if (d < 50){
					result *= 2;
					if (d < 45) {
						if (d < 13) {
							result = 50;
							if (d < 7)
								result = 100;
						}
					}
				}
			}
		} else
			result = 0;
		
		return result;
	}
}
