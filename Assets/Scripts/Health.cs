using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public Slider hpBar;
    public Slider fuelBar;
    public int hp = 5;
    public float fuel = 100;
    public GameObject explosion;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            Damage(1);
            hpBar.value -= 1;
            playSound("lazer");
        }
        else if (other.tag == "hp")
        {
            if (hp < 5)
            {
                Damage(-1);
                hpBar.value += 1;
            }
            playSound("hp");
        }
        else if(other.tag == "fuel")
        {
            fuel += 40;
            fuelBar.value += 40;
            playSound("fuel");
        }
        else if(other.tag == "coin")
        {
            Manage manage = GameObject.Find("Manager").GetComponent<Manage>();
            manage.SetReward();
            playSound("coin");
        }
        else if(other.tag == "speed")
        {
            PlayerMove pm = GameObject.Find("spaceship").GetComponent<PlayerMove>();
            StartCoroutine(wait5second(pm));
            playSound("speed");
        }
    }

    public void Damage(int value)
    {
        hp -= value;
        if(hp<=0)
        {
            Manage.instance.SetGameOver();
            GameObject temp = (GameObject)Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(temp, 2.0f);
            Destroy(gameObject);
        }
        else if(fuelBar.value <= 0)
        {
            Manage.instance.SetGameOver();
            GameObject temp = (GameObject)Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(temp, 2.0f);
            Destroy(gameObject);
        }
    }

    IEnumerator wait5second(PlayerMove pm)
    {
        pm.m_speed = 30;
        yield return new WaitForSeconds(5f);
        pm.m_speed = 15;
    }

    private void Update()
    {
        DecreaseFuel();
    }

    void DecreaseFuel()
    {
        float delta = Time.deltaTime;
        fuel -= delta * 2;
        fuelBar.value -= delta*2;
    }

    void playSound(string str)
    {
        GameObject.Find(str).GetComponent<AudioSource>().Play();
        GameObject.Find(str).GetComponent<AudioSource>().volume = 1;
    }
}
