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
            if (Input.GetMouseButtonDown(0)) //mouse sol t�ka t�klad�g�mda cizgi olusacak
            {
                CizgiOlustur(); 
            }
            if (Input.GetMouseButton(0))
            {
                Vector2 ParmakPozisyonum = Camera.main.ScreenToWorldPoint(Input.mousePosition); //farenin koordinatlar�n� ald�k

                if (Vector2.Distance(ParmakPozisyonum, ParmakPozisyonuListesi[^1]) > 0.1f)
                {
                    CizgiyiGuncelleme(ParmakPozisyonum);
                }
                /* parmaklistesinin son eleman�ndan 0.1 kadar b�y�kse yani 0.1 uzunlukta mesafe varsa
                 * �izgiyi olu�turur kullan�c� �ok h�zl� kayd�rmas� durumunda hatalar� engellemek i�in
                 * "^1" ifadesi listennin uzunlugunun -1 indeksini verir yani listesnin son eleman� */
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
        Cizgi = Instantiate(LinePrefab, Vector2.zero, Quaternion.identity);   // instantiate olu�turma komutu sahneye ekler - lineprefab �izgimiz - vector2 zero yani 0,0 da olu�acak
        Cizgiler.Add(Cizgi);                                                  // cizgileri yok etmek icin depolad�k
        lineRenderer = Cizgi.GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.085f;
        lineRenderer.endWidth = 0.085f;
        EdgeCollider2D = Cizgi.GetComponent<EdgeCollider2D>();
        ParmakPozisyonuListesi.Clear();                                     // bir sonraki �izgi �ekerken tekrar kullan�cag�mdan s�f�rlad�m
        ParmakPozisyonuListesi.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        ParmakPozisyonuListesi.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition)); //�ift koordinat oldugu i�in 2 defa yaz�yoruz
        /* camera.main ana kameram�z� temsil eder
         * mouseposition komutuyla mouse un ekran koordinatlar�n� al�r�z
         * screetoworld ekran koordinatlar�n� unityinin uzay koordinatlar�na d�n��t�rmeye yarar */
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
