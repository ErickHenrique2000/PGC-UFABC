using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class playerProx : MonoBehaviour
{

    public  Vector2     range;
    public  bool        playerProximo;
    public  LayerMask   playerMask;
    public  Light2D   luz;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerProximo = Physics2D.OverlapBox(transform.position, range, 0, playerMask);
        if(playerProximo){
            //luz.SetActive(true);
            //luz.gameObject.GetComponent<Light>
            
            if (luz.intensity < 1) {
                luz.intensity += 0.02f;
            }
        } else{
            //luz.SetActive(false);
            if (luz.intensity > 0) {
                luz.intensity -= 0.02f;
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, range);
    }
}
