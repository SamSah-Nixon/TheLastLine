using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthMeter : MonoBehaviour
{
    public float health = 10000f;
    public float maxHealth = 10000f;
    public TextMeshProUGUI healthText;
    public GameObject gameOverText;
    public String prefix;
    public GameObject healthBar;
    private float healthBarWidth;
    private bool beingDamaged = false;
    private Color healthBarColor;
    private float lastHealthTick;
    void Start()
    {
        healthBarWidth = healthBar.GetComponent<RectTransform>().rect.width;
        healthBarColor = healthBar.GetComponent<Image>().color;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        healthText.text = prefix + health.ToString();
        healthBar.GetComponent<RectTransform>().localScale = new Vector3(health / maxHealth, 1, 1); 
        healthBar.GetComponent<RectTransform>().localPosition = new Vector3(health / maxHealth * (healthBarWidth / 2 - 5) - (healthBarWidth / 2 - 5), 0,0);

        if (health <= 0)
        {
            gameOverText.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else if (beingDamaged)
        {
            if(healthBar.GetComponent<Image>().color.r >= 1)
                healthBar.GetComponent<Image>().color = new Color(1, 0, 0);
            else
                healthBar.GetComponent<Image>().color = new Color(healthBar.GetComponent<Image>().color.r + 0.04f,
                                                                  healthBar.GetComponent<Image>().color.g - 0.04f, 
                                                                  healthBar.GetComponent<Image>().color.b - 0.04f
            );
        }

        if (lastHealthTick + 0.1f < Time.time)
        {
            beingDamaged = false;
            healthBar.GetComponent<Image>().color = healthBarColor;
        }
    }


    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            lastHealthTick = Time.time;
            beingDamaged = true;
            health--;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            beingDamaged = false;
            healthBar.GetComponent<Image>().color = healthBarColor;
        }
    }


}
