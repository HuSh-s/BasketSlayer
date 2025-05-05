using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Timeline;
//using GoogleMobileAds.Api;
//using GoogleMobileAds;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TopAtar _TopAtar;
    [SerializeField] private CizgiCizme _CizgiCizme;

    [SerializeField] private GameObject[] Paneller;
    [SerializeField] private TextMeshProUGUI[] ScoreTextleri;
    [SerializeField] public AudioSource[] Sesler;
    [SerializeField] public ParticleSystem Kovayagirme;

    private int deathCount;
/*
    BannerView bannerView;
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-7242135691206004/7047269120";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
  private string _adUnitId = "unused";
#endif

    private InterstitialAd _interstitialAd;
#if UNITY_ANDROID
    private string _adUnitId2 = "ca-app-pub-7242135691206004/9729272787";
#elif UNITY_IPHONE
  private string _adUnitId2 = "ca-app-pub-3940256099942544/4411468910";
#else
  private string _adUnitId2 = "unused";
#endif

    */
    int GirenTopSayisi;
    void Start()
    {/*
        MobileAds.Initialize((InitializationStatus initStatus) => { });

        BannerOlustur();
        Banneristek();
        */
        deathCount = PlayerPrefs.GetInt("DeathCount", 0);

        Sesler[0].Play();

        GirenTopSayisi = 0;
        if (PlayerPrefs.HasKey("BestScore"))
        {
            ScoreTextleri[0].text = PlayerPrefs.GetInt("BestScore").ToString();
            ScoreTextleri[1].text = PlayerPrefs.GetInt("BestScore").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("BestScore", 0);
            ScoreTextleri[0].text = "0";
            ScoreTextleri[1].text = "0";
        }

    }
    public void DevamEt(Vector2 pos)
    {
        Sesler[2].Play();

        Kovayagirme.transform.position = pos;
        Kovayagirme.gameObject.SetActive(true);
        Kovayagirme.Play();

        GirenTopSayisi++;
        _TopAtar.DevamEt();
        _CizgiCizme.DevamEt();
    }

    public void OyunBitti()
    {
        //PlayerDied();

        Paneller[1].SetActive(true);
        Paneller[2].SetActive(false);

        ScoreTextleri[1].text = PlayerPrefs.GetInt("BestScore").ToString();
        ScoreTextleri[2].text = GirenTopSayisi.ToString();

        if (GirenTopSayisi > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", GirenTopSayisi);
        }

        _TopAtar.TopAtmaDurdur();
        _CizgiCizme.CizmeyiDurdur();

    }

    public void OyunBaslasin()
    {
        Paneller[0].SetActive(false);

        _TopAtar.OyunBaslasin();
        _CizgiCizme.CizmeyiBaslat();

        Paneller[2].SetActive(true);
    }

    public void TekrarOyna()
    {
        // Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /*
    public void BannerOlustur()
    {
        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        if (bannerView != null)
        {
            BannerDestroyAd();
        }
        bannerView = new BannerView(_adUnitId, adaptiveSize, AdPosition.Bottom);
    }
    public void Banneristek()
    {
        if (bannerView == null)
        {
            BannerOlustur();
        }
        var request = new AdRequest();

        bannerView.LoadAd(request);
    }
    public void BannerDestroyAd()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }
    }

    public void InterstitialAdYukleme()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
        }
        var request = new AdRequest();
        InterstitialAd.Load(_adUnitId2, request,(InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                return;
            }
            _interstitialAd = ad;
        });
    }

    public void InterstitialAdGosterme()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    void PlayerDied()
    {
        deathCount++;
        PlayerPrefs.SetInt("DeathCount", deathCount);

        if (deathCount % 3 == 0)
        {
            InterstitialAdYukleme();
            InterstitialAdGosterme();
        }
    }
    */

}
