using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    [SerializeField] private float speed;

    private Animator animator;

    protected Vector2 direction;

    //private Rigidbody2D myRigidBody;

    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        //myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        //myRigidBody.velocity = direction * speed;

        if (direction.x != 0 || direction.y != 0)
        {
            AnimateMovement(direction);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }


    }


    public void AnimateMovement(Vector2 direction)
    {

        animator.SetLayerWeight(1, 1);

        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }
}
