using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
	public Transform spriteTransform; 
	public float distance = 1.0f;

	public float roationSpeed = 5f;

	void Update()
	{
		Vector3 mouseScreenPosition = Input.mousePosition;

		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.WorldToScreenPoint(spriteTransform.position).z));


		Vector3 direction = (mouseWorldPosition - spriteTransform.position).normalized;
		float currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		Vector3 newPosition = spriteTransform.position + new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad), 0) * distance;
		transform.position = newPosition;

		transform.rotation = Quaternion.Euler(0, 0, currentAngle);
	}
}
