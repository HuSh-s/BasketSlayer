using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CizgiCizme : MonoBehaviour
{
    public GameObject LinePrefab;
    public GameObject Cizgi;

    public LineRenderer lineRenderer;
    public EdgeCollider2D EdgeCollider2D;
    public List<Vector2> ParmakPozisyonuListesi;

    public List<GameObject> Cizgiler;
    

    bool CizmekMumkunMu;
    int CizmeHakki;

    [SerializeField] private TextMeshProUGUI CizmeHakkiText;

    private void Start()
    {
        CizmekMumkunMu = false;
        CizmeHakki = 3;
        CizmeHakkiText.text = CizmeHakki.ToString();
    }

    void Update()
    {
        if(CizmekMumkunMu && CizmeHakki!= 0)
        {
            if (Input.GetMouseButtonDown(0)) //mouse sol týka týkladýgýmda cizgi olusacak
            {
                CizgiOlustur(); 
            }
            if (Input.GetMouseButton(0))
            {
                Vector2 ParmakPozisyonum = Camera.main.ScreenToWorldPoint(Input.mousePosition); //farenin koordinatlarýný aldýk

                if (Vector2.Distance(ParmakPozisyonum, ParmakPozisyonuListesi[^1]) > 0.1f)
                {
                    CizgiyiGuncelleme(ParmakPozisyonum);
                }
                /* parmaklistesinin son elemanýndan 0.1 kadar büyükse yani 0.1 uzunlukta mesafe varsa
                 * çizgiyi oluþturur kullanýcý çok hýzlý kaydýrmasý durumunda hatalarý engellemek için
                 * "^1" ifadesi listennin uzunlugunun -1 indeksini verir yani listesnin son elemaný */
            }

        }

        if(Cizgiler.Count!= 0 && CizmeHakki != 0)
        {
            if(Input.GetMouseButtonUp(0))
            {
                CizmeHakki--;
                CizmeHakkiText.text = CizmeHakki.ToString();
            }
        }
    }

    void CizgiOlustur()
    {
        Cizgi = Instantiate(LinePrefab, Vector2.zero, Quaternion.identity);   // instantiate oluþturma komutu sahneye ekler - lineprefab çizgimiz - vector2 zero yani 0,0 da oluþacak
        Cizgiler.Add(Cizgi);                                                  // cizgileri yok etmek icin depoladýk
        lineRenderer = Cizgi.GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.085f;
        lineRenderer.endWidth = 0.085f;
        EdgeCollider2D = Cizgi.GetComponent<EdgeCollider2D>();
        ParmakPozisyonuListesi.Clear();                                     // bir sonraki çizgi çekerken tekrar kullanýcagýmdan sýfýrladým
        ParmakPozisyonuListesi.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        ParmakPozisyonuListesi.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition)); //çift koordinat oldugu için 2 defa yazýyoruz
        /* camera.main ana kameramýzý temsil eder
         * mouseposition komutuyla mouse un ekran koordinatlarýný alýrýz
         * screetoworld ekran koordinatlarýný unityinin uzay koordinatlarýna dönüþtürmeye yarar */
        lineRenderer.SetPosition(0, ParmakPozisyonuListesi[0]);
        lineRenderer.SetPosition(1, ParmakPozisyonuListesi[1]);
        EdgeCollider2D.points = ParmakPozisyonuListesi.ToArray();
    }

    void CizgiyiGuncelleme(Vector2 GelenParmakPozisyonum)
    {
        ParmakPozisyonuListesi.Add(GelenParmakPozisyonum);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, GelenParmakPozisyonum);
        EdgeCollider2D.points = ParmakPozisyonuListesi.ToArray();
    }

    public void DevamEt()
    {
        if(TopAtar.AtilanTopSayisi==0)
        {
            foreach (var item in Cizgiler)
            {
                Destroy(item.gameObject);
            }
            Cizgiler.Clear();
            CizmeHakki = 3;
            CizmeHakkiText.text = CizmeHakki.ToString();
        }

        
    }

    public void CizmeyiDurdur()
    {
        CizmekMumkunMu = false;
    }
    public void CizmeyiBaslat()
    {
        CizmeHakki = 3;
        CizmekMumkunMu = true;
    }
}
