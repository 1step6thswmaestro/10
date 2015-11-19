using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {    
    
    public GameObject boomPrefab;
    

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        
        GameObject boomParticle = Instantiate(boomPrefab, this.transform.position, this.transform.rotation ) as GameObject;
        
        Destroy(this.gameObject);
        Destroy(boomParticle, 0.5f);
    }
}
