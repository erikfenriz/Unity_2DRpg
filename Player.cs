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


    // Start is called before the first frame update
    protected override void Start()
    {

        health.Initialize(initialHealth, initialHealth);
        mana.Initialize(initialMana, initialMana);

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();

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
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector2.right;
        }
    }

}
