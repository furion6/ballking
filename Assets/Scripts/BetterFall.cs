using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterFall : MonoBehaviour
{
    // Start is called before the first frame update
    public float fallMultiplier = 1.5f;
    public float lowMultiplier = 1f;
    Rigidbody2D rb;
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y < 0){
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }else if(rb.velocity.y > 0){
            rb.velocity += Vector2.up * Physics2D.gravity.y * lowMultiplier * Time.deltaTime;
        }
    }
}
