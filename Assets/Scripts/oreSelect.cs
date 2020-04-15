using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oreSelect : MonoBehaviour
{
    bool bronze = false;
    bool silver = false;
    bool gold = false;
    public Light lightFX;
    public Material bronzeMat;
    public Material silverMat;
    public Material goldMat;
    public ParticleSystem particles;
    private ParticleSystem myParticles;
    private Light myLight;
    private string result = "";
    public float temp;

    // Start is called before the first frame update
    void Start()
    {
        lightFX = Resources.Load<Light>("Effects/oreLight");
        temp = Random.value;
        SpriteRenderer tempRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        particles = Resources.Load<ParticleSystem>("Effects/oreParticles");
        if (gameObject.transform.position.y > -35)
        {

            if (temp > 0.95)
            {

                if (temp < 0.975)
                {
                    myParticles = Instantiate(particles, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    myLight = Instantiate(lightFX, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    bronzeMat = Resources.Load<Material>("bronzeOreMat");
                    tempRenderer.material = bronzeMat;
                    
                    result += "bronze";
                }
                else if (temp > 0.975 && temp < 0.99)
                {
                    myParticles = Instantiate(particles, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    myLight = Instantiate(lightFX, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    silverMat = Resources.Load<Material>("silverOreMat");
                    tempRenderer.material = silverMat;
                    result += "silver";
                }
                else if (temp > 0.99)
                {
                    myParticles = Instantiate(particles, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    myLight = Instantiate(lightFX, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    goldMat = Resources.Load<Material>("goldOreMat");
                    tempRenderer.material = goldMat;
                    result += "gold";
                    if (temp > 0.995)
                    {
                        if (this.gameObject.transform.position.x < 0)
                        {
                            result += "greenCrawler";
                           
                        }
                        else
                        {
                            result += "purpleCrawler";
                            
                        }

                    }

                }
                else
                {
                    result += "n";
                }
            }

        }
        else if (this.gameObject.transform.position.y < -35 && this.gameObject.transform.position.y > -60)
        {
            if (temp > 0.90)
            {
                if (temp < 0.95)
                {
                    myParticles = Instantiate(particles, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    myLight = Instantiate(lightFX, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    bronzeMat = Resources.Load<Material>("bronzeOreMat");
                    tempRenderer.material = bronzeMat;
                    result += "bronze";
                }
                else if (temp > 0.95 && temp < 0.98)
                {
                    myParticles = Instantiate(particles, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    myLight = Instantiate(lightFX, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    silverMat = Resources.Load<Material>("silverOreMat");
                    tempRenderer.material = silverMat;
                    result += "silver";
                }
                else if (temp > 0.98)
                {
                    myParticles = Instantiate(particles, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    myLight = Instantiate(lightFX, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    goldMat = Resources.Load<Material>("goldOreMat");
                    tempRenderer.material = goldMat;
                    result += "gold";
                    if (temp > 0.985)
                    {
                        if (this.gameObject.transform.position.x < 0)
                        {

                            result += "greenCrawler";
                        }
                        else
                        {
                            result += "purpleCrawler";
                        }
                    }

                }
                else
                {
                    result += "n";
                }
            }
        }
        else if (this.gameObject.transform.position.y < -60 && this.gameObject.transform.position.y > -75)
        {

            if (temp > 0.85)
            {
                if (temp < 0.93)
                {
                    myParticles = Instantiate(particles, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    myLight = Instantiate(lightFX, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    bronzeMat = Resources.Load<Material>("bronzeOreMat");
                    tempRenderer.material = bronzeMat;
                    result += "bronze";
                }
                else if (temp > 0.93 && temp < 0.97)
                {
                    myParticles = Instantiate(particles, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    myLight = Instantiate(lightFX, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    silverMat = Resources.Load<Material>("silverOreMat");
                    tempRenderer.material = silverMat;
                    result += "silver";
                }
                else if (temp > 0.97)
                {
                    myParticles = Instantiate(particles, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    myLight = Instantiate(lightFX, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    goldMat = Resources.Load<Material>("goldOreMat");
                    tempRenderer.material = goldMat;
                    result += "gold";
                    if (temp > 0.975)
                    {
                        if (this.gameObject.transform.position.x < 0)
                        {


                            result += "greenCrawler";
                            result += "purpleCrawler";
                        }
                        else
                        {


                            result += "greenCrawler";
                            result += "purpleCrawler";
                        }
                    }

                }
                else
                {
                    result += "n";
                }
            }
        }
        else
        {


            if (temp > 0.75)
            {
                if (temp < 0.85)
                {
                    myParticles = Instantiate(particles, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    myLight = Instantiate(lightFX, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    bronzeMat = Resources.Load<Material>("bronzeOreMat");
                    tempRenderer.material = bronzeMat;
                    result += "bronze";
                }
                else if (temp > 0.85 && temp < 0.95)
                {
                    myParticles = Instantiate(particles, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    myLight = Instantiate(lightFX, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    silverMat = Resources.Load<Material>("silverOreMat");
                    tempRenderer.material = silverMat;
                    result += "silver";
                }
                else if (temp > 0.95)
                {
                    myParticles = Instantiate(particles, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    myLight = Instantiate(lightFX, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                    goldMat = Resources.Load<Material>("goldOreMat");
                    tempRenderer.material = goldMat;
                    result += "gold";
                    if (temp > 0.965)
                    {
                        result += "orangeCrawler";
                    }

                }
                else
                {
                    result += "n";
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getResult()
    {
            return result;
    }
    private void OnDestroy()
    {
        Destroy(myLight);
        Destroy(myParticles);
    }
}
