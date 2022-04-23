using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SimpleBillboard : MonoBehaviour {

    public Transform target;

    public Vector3 offset;

    public bool lateUpdate = true;
    public bool faceToCamera = false;

    public Vector2 xLock = new Vector2(0,360f);

    public bool freezeX = true, freezeY, freezeZ = true;

    void Start () {
		
	}
	
	
	void Update () {
        if (!lateUpdate)
            Refresh();
	}

    void LateUpdate(){
        if (lateUpdate)
            Refresh();
    }


    void Refresh()
    {
        if (target == null && Camera.main != null)
            target = Camera.main.transform;
        if (target == null) return;

        transform.forward = target.forward * ((faceToCamera) ? -1 : 1);
        if (freezeX || freezeY || freezeZ) {

            float x = transform.eulerAngles.x * ((freezeX) ? 0 : 1);
            float y = transform.eulerAngles.y * ((freezeY) ? 0 : 1);
            float z = transform.eulerAngles.z * ((freezeZ) ? 0 : 1);
            transform.eulerAngles = new Vector3(Mathf.Clamp(x,xLock.x, xLock.y), y, z);
        }
        transform.eulerAngles += offset;
    }
}
