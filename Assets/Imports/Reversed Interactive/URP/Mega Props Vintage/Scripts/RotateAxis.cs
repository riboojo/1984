using UnityEngine;
using System.Collections;

namespace StixGames.GrassShader
{
	public class RotateAxis : MonoBehaviour
	{
		public Vector3 axis = Vector3.forward;
		public float speed = 90;
        public bool isActive = false;

		void Update()
		{
            if (isActive)
            {
                transform.rotation *= Quaternion.AngleAxis(speed * Time.deltaTime, axis);
            }
		}

        public void SetState(bool state)
        {
            isActive = state;
        }
	}
}
