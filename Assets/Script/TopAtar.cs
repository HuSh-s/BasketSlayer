using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopAtar : MonoBehaviour
{
    [SerializeField] private GameObject[] Toplar;
    [SerializeField] private GameObject TopAtarMerkez;
    [SerializeField] private GameObject Kova;
    [SerializeField] private GameObject[] KovaNoktalari;
    [SerializeField] private GameManager _GameManager;
    int AktifTopIndex;
    int RandomSayiKova;
    bool Kilit;

    public static int AtilanTopSayisi;
    public static int TopAtisSayisi;

    private void Start()
    {
        TopAtisSayisi = 0;
        AtilanTopSayisi = 0;
    }

    public void OyunBaslasin()
    {
        StartCoroutine(TopAtisSistemi());
    }

    IEnumerator TopAtisSistemi()
    {
        while (true)
        {
            if (!Kilit)
            {
                yield return new WaitForSeconds(0.5f);


                if(TopAtisSayisi !=0 && TopAtisSayisi % 4 == 0) 
                {
                    for (int i = 0; i < 2; i++)
                    {
                        TopAtisveAyarlama();
                    }

                    AtilanTopSayisi = 2;
                    TopAtisSayisi++;
                }
                else
                {
                    TopAtisveAyarlama();

                    AtilanTopSayisi = 1;
                    TopAtisSayisi++;
                }



                yield return new WaitForSeconds(0.7f); // kovamýzý belli bir süre sonra cýkardýk

                RandomSayiKova = Random.Range(0, KovaNoktalari.Length - 1);
                Kova.transform.position = KovaNoktalari[RandomSayiKova].transform.position;
                Kova.SetActive(true);
                Kilit = true;
                Invoke("TopuKontrolEt", 5f);

            }
            else
            {
                yield return null;
            }
        }
    }

    void TopAtisveAyarlama()
    {
        Toplar[AktifTopIndex].transform.position = TopAtarMerkez.transform.position; //merkeze topumuzu aldýk
        Toplar[AktifTopIndex].SetActive(true); // aklifliðini açtýk
        Toplar[AktifTopIndex].GetComponent<Rigidbody2D>().AddForce(730 * PozisyonVer(AciVer(70f, 110f)));

        if (AktifTopIndex != Toplar.Length - 1)
            AktifTopIndex++;
        else
            AktifTopIndex = 0;
    }

    float AciVer(float deger1, float deger2)
    {
        return Random.Range(deger1, deger2); // rastgele aci aldýk
    }

    Vector3 PozisyonVer (float Gelenaci)
    {
        return Quaternion.AngleAxis(Gelenaci, Vector3.forward) * Vector3.right;
        // AngleAxis komutuyla forward diyerek z eksenin etrafýnda matris oluþturdu right x eksenini aldýk
    }

    public void DevamEt()
    {
        if(AtilanTopSayisi == 1)
        {
            Kilit = false;
            Kova.SetActive(false);
            CancelInvoke();
            AtilanTopSayisi--;
        }
        else
        {
            AtilanTopSayisi--;
        }
   
    }

    public void TopAtmaDurdur()
    {
        StopAllCoroutines();
    }

    void TopuKontrolEt()
    {
        if (Kilit)
        {
            GetComponent<GameManager>().OyunBitti();
            _GameManager.Sesler[1].Play();
        }
    }
}
