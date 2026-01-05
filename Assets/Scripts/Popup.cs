using System.Collections;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public float power = 20;
    public int touchCnt = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "ball")
        {
            if(touchCnt < 1)
                StartCoroutine(CoPulse(collision.rigidbody));

            touchCnt++;
        }
    }

    IEnumerator CoPulse(Rigidbody rb)
    {
        yield return new WaitForSeconds(2);

        rb.AddForce(Vector3.up * 20, ForceMode.Impulse);

        touchCnt = 0;
    }
}
