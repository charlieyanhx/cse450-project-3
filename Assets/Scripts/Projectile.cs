using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.right * 10f;
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            SoundManager.instance.PlaySoundHit();
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            SoundManager.instance.PlaySoundMiss();
        }

        Destroy(gameObject);
    }
}
