using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ammoPanel : MonoBehaviour
{
    private Text ammoText;
    private AudioSource audioSource;

    public int START_AMMO = 40, MAX_IN_RELOAD = 10;

    public bool isShootable;

    private int reloaded_ammo, tot_ammo_ex_reloaded;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        isShootable = true;
        reloaded_ammo = MAX_IN_RELOAD;
        tot_ammo_ex_reloaded = START_AMMO - MAX_IN_RELOAD;
        ammoText = gameObject.GetComponent<Text>();
        updateAmmoTextPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void oneShot()
    {
        if (reloaded_ammo == 0 && tot_ammo_ex_reloaded == 0)
        {
            // TODO play no ammo sound
            return;
        }
        reloaded_ammo--;
        updateAmmoTextPanel();
        if(reloaded_ammo == 0)
            reload();
    }

    private void updateAmmoTextPanel()
    {
        String betweenSymbol = " / ";
        ammoText.text = reloaded_ammo + betweenSymbol + tot_ammo_ex_reloaded;
        if (reloaded_ammo != 0 || tot_ammo_ex_reloaded != 0)
            isShootable = true;
    }

    public void reload()
    {
        isShootable = false;
        //TODO play sound
        if (tot_ammo_ex_reloaded == 0)
            return;
        audioSource.Play();
        if (tot_ammo_ex_reloaded > MAX_IN_RELOAD)
        {
            reloaded_ammo = MAX_IN_RELOAD;
            tot_ammo_ex_reloaded -= MAX_IN_RELOAD;
        }
        else
        {
            reloaded_ammo = tot_ammo_ex_reloaded;
            tot_ammo_ex_reloaded = 0;
        }
        Invoke("updateAmmoTextPanel", 3.4f);
    }
    
    
}
