using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private Stats health;

    [SerializeField]
    private Stats mana;

    [SerializeField]
    private float initialHealth;

    [SerializeField]
    private float initialMana;

    [SerializeField]
    private GameObject[] spellPrefab;

    [SerializeField]
    private Block[] blocks;

    [SerializeField]
    private Transform[] exitPoints;

    private int exitIndex = 2;

    private Transform target;


    // Start is called before the first frame update
    protected override void Start()
    {

        health.Initialize(initialHealth, initialHealth);
        mana.Initialize(initialMana, initialMana);

        target = GameObject.Find("Target").transform;

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();

        //Debug.Log(LayerMask.GetMask("Block"));

		base.Update();
    }

    private void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.P))
        {
            mana.MyCurrentValue -= 10;
            health.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            mana.MyCurrentValue += 10;
            health.MyCurrentValue += 10;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            exitIndex = 0;
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            exitIndex = 3;
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            exitIndex = 2;
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            exitIndex = 1;
            direction += Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.Space)) // attack button
        {
            Block();

            if (!isAtacking && !isMoving && inlineOfSight())
            {
                attackRoutine = StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        
            isAtacking = true;

            myAnimator.SetBool("attack", isAtacking);

            yield return new WaitForSeconds(1); // hardcoded value

            CastSpell();

            StopAttack();
     
    }

    public void CastSpell()
    {
        Instantiate(spellPrefab[0], exitPoints[exitIndex].position, Quaternion.identity);
    }

    private bool inlineOfSight()
    {
        Vector3 targetDirection = (target.transform.position - transform.position).normalized;

        //Debug.DrawRay(transform.position, targetDirection, Color.red); // debugging line of sight

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection,
            Vector2.Distance(transform.position, target.transform.position),256);

        if (hit.collider == null)
        {
            return true;
        }
        return false;
    }

    private void Block()
    {
        foreach (Block b in blocks)
        {
            b.Deactivate();
        }

        blocks[exitIndex].Activate();
    }

}
