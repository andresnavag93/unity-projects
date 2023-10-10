using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPlay : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer videoMarketing, videoDesign, videoSoftware;

    public GameObject marketingText, marketingIcon, softwareText, softwareIcon, designText, designIcon;

    // Start is called before the first frame update
    void Start()
    {
        videoMarketing.frame = 0;
        videoSoftware.frame = 0;
        videoDesign.frame = 0;
        marketingText.SetActive(true);
        marketingIcon.SetActive(true);
        softwareText.SetActive(true);
        softwareIcon.SetActive(true);
        designText.SetActive(true);
        designIcon.SetActive(true);
    }

    public void MarketingVideo()
    {

        videoMarketing.Play();
        marketingText.SetActive(false);
        marketingIcon.SetActive(false);
        videoSoftware.Pause();
        softwareText.SetActive(true);
        softwareIcon.SetActive(true);
        videoDesign.Pause();
        designText.SetActive(true);
        designIcon.SetActive(true);
    }

    public void SoftwareVideo()
    {
        videoMarketing.Pause();
        marketingText.SetActive(true);
        marketingIcon.SetActive(true);
        videoSoftware.Play();
        softwareText.SetActive(false);
        softwareIcon.SetActive(false);
        videoDesign.Pause();
        designText.SetActive(true);
        designIcon.SetActive(true);
    }

    public void DesignVideo()
    {
        videoMarketing.Pause();
        marketingText.SetActive(true);
        marketingIcon.SetActive(true);
        videoSoftware.Pause();
        softwareText.SetActive(true);
        softwareIcon.SetActive(true);
        videoDesign.Play();
        designText.SetActive(false);
        designIcon.SetActive(false);
    }

    public void StopPlay()
    {
        videoMarketing.frame = 0;
        videoMarketing.Pause();
        marketingText.SetActive(true);
        marketingIcon.SetActive(true);
        videoSoftware.frame = 0;
        videoSoftware.Pause();
        softwareText.SetActive(true);
        softwareIcon.SetActive(true);
        videoDesign.frame = 0;
        videoDesign.Pause();
        designText.SetActive(true);
        designIcon.SetActive(true);
    }

    public void OpenWhatsapp()
    {
        Application.OpenURL("https://api.whatsapp.com/send?phone=17377017875&text=Hola%20wayuinc,%20quisiera%20conocer%20sobre...");
    }


    public void OpenWeb()
    {
        Application.OpenURL("http://wayuinc.com/#/home");
    }


    public void OpenGoogle()
    {
        Application.OpenURL("https://www.google.com/maps/place/Wayu+Inc./@10.4784917,-66.8653333,15.63z/data=!4m5!3m4!1s0x0:0x91b0e84c2c10a3a3!8m2!3d10.4793159!4d-66.8614874");
    }

}
