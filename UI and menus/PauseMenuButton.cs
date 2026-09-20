using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.U2D;
using System.Reflection;
using TMPro;

public class PauseMenuButton : MonoBehaviour
{
    public Animator an;
    public GameObject pauseMenu;
    private InputManager inputManager;


    [Space]
    public GameObject resume;
    public GameObject controlMenuButton1;
    public GameObject videoSettingsButton1;
    public GameObject audioSettingsButton1;
    public string optionsAnName;
    public GameObject timer;

    [Space]
    [Header("Resolution Settings")]
    public Vector2Int[] pixelResolutions;
   
    public TMP_Dropdown resolutionDropdown;

    // Optional: assign your PixelPerfectCamera in the inspector; fallback to FindObjectOfType if null.
    public PixelPerfectCamera pixelPerfectCamera;
    [Space]
    public Toggle fullScreenToggle;
    [SerializeField] private Toggle vsyncToggle;
    [SerializeField] private Toggle timerToggle;
    private struct ResEntry { public int w, h; public bool isPixel; public int baseW, baseH; public ResEntry(int W, int H, bool p, int bW, int bH) { w = W; h = H; isPixel = p; baseW = bW; baseH = bH; } }
    private List<ResEntry> dropdownEntries = new List<ResEntry>();
    [Space]
    public bool OnlyUsingItForQualitySettings = false;
    
    private void Start()
    {
        inputManager = InputManager.instance;
 
        PopulateResolutionsDropdown();
        SetResolution(resolutionDropdown.value);
        if (OnlyUsingItForQualitySettings == true)
        {
            return;                
        }
        fullScreenToggle.SetIsOnWithoutNotify( PlayerPrefs.GetInt("fullScreen",1) == 1); 
        bool timerOn = PlayerPrefs.GetInt("timer", 0) == 1;

        GameManager.gameManager.timerOn = timerOn;
        timer.SetActive(timerOn);
        timerToggle.SetIsOnWithoutNotify(timerOn);

        vsyncToggle.SetIsOnWithoutNotify((QualitySettings.vSyncCount == 0) ? false : true);

    }

    public void PlayAnimation(string anName)
    {
        an.Play(anName);

    }
    private void Update()
    { 
        if(OnlyUsingItForQualitySettings == true)
        {
            return;
        }
        if (inputManager.optionsPressed && GameManager.gameManager.demoFinishedDELETEAFTER==false )
        {
            Resume();
        }
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        bool changeSelected = false;
        if (currentSelected == null ||currentSelected.activeInHierarchy == false)
        {
             changeSelected = true;
        }
        else
        {
            Button but = currentSelected.GetComponent<Button>();
            if(but != null && (but.interactable == false || but.enabled == false))
            {
                changeSelected = true;
            }
        }
        if (changeSelected == true)
        {
            if (pauseMenu.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(resume);
            }
            if (controlMenuButton1.activeInHierarchy == true)
            {
                EventSystem.current.SetSelectedGameObject(controlMenuButton1);
            }
            else if (videoSettingsButton1.activeInHierarchy == true)
            {               
                EventSystem.current.SetSelectedGameObject(videoSettingsButton1);
            }
            else if (audioSettingsButton1.activeInHierarchy == true)
            {
                EventSystem.current.SetSelectedGameObject(audioSettingsButton1);
            }
        }
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("fullScreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void SetVsync()
    {
        if (QualitySettings.vSyncCount == 0)
        {
            QualitySettings.vSyncCount = 1;
            vsyncToggle.SetIsOnWithoutNotify(true);
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            vsyncToggle.SetIsOnWithoutNotify(false);
        }
        PlayerPrefs.SetInt("VSync", QualitySettings.vSyncCount == 0 ? 0 : 1);
        PlayerPrefs.Save();
    }
    public void SetTimerOnOff() {
        GameManager.gameManager.timerOn = !GameManager.gameManager.timerOn;
        timer.SetActive(!timer.activeInHierarchy);
        PlayerPrefs.SetInt("timer", GameManager.gameManager.timerOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void MainMenu()
    {
        SaveIt();
        Time.timeScale = 1;

        SceneManager.LoadScene(0);

    }
    public void MainMenuNoSave()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }

 
    public void SaveIt()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        switch (level)
        {
            case (int)Level.chapter2:
                Chapter2 chp2 = new Chapter2(GameManager.gameManager.bossFight, GameManager.gameManager.chapter2Boss.position, GameManager.gameManager.cutscene);
                SaveSystem.Save(GameManager.gameManager.player.position, SceneManager.GetActiveScene().buildIndex,
            GameManager.gameManager.saveData.name, 1, GameManager.gameManager.saveData.saveFile,GameManager.gameManager.timeHasPassed+ Time.timeSinceLevelLoad, chp2);
                break;
            case (int)Level.chapter3Boss:
                Chapter3Boss chp3Boss= new Chapter3Boss(GameManager.gameManager.boss3Fase);
                SaveSystem.Save(GameManager.gameManager.player.position, SceneManager.GetActiveScene().buildIndex,
           GameManager.gameManager.saveData.name, 1, GameManager.gameManager.saveData.saveFile, GameManager.gameManager.timeHasPassed+ Time.timeSinceLevelLoad, chp3Boss);
                break;
            case (int)Level.chapter4:
                Chapter4 chp4 = new Chapter4(Physics2D.gravity, GameManager.gameManager.cutscene);
                SaveSystem.Save(GameManager.gameManager.player.position, SceneManager.GetActiveScene().buildIndex,
           GameManager.gameManager.saveData.name, 1, GameManager.gameManager.saveData.saveFile, GameManager.gameManager.timeHasPassed+ Time.timeSinceLevelLoad, chp4);
                break;
            default:
                SaveSystem.Save(GameManager.gameManager.player.position, SceneManager.GetActiveScene().buildIndex,
            GameManager.gameManager.saveData.name, 1, GameManager.gameManager.saveData.saveFile, GameManager.gameManager.timeHasPassed+ Time.timeSinceLevelLoad);
                break;
        }


    }
    public void DeleteSave()
    {
        SaveSystem.Delete(1);
        GameManager.gameManager.timeHasPassed = 0;
    }



    // New resolution wiring: dropdown index -> entry in dropdownEntries
    public void SetResolution(int resolutionIndex)
    {
        var entry = dropdownEntries[resolutionIndex];
        GameManager.gameManager.dropDownValue = resolutionIndex;

        int targetScreenW = entry.w;
        int targetScreenH = entry.h;

        // If this is a pixel-base entry (a desired reference resolution), scale it up
        // to the largest integer multiple that fits the current display so the screen
        // becomes the scaled size while the pixel-perfect base remains the original.
        if (entry.isPixel && entry.baseW > 0 && entry.baseH > 0)
        {
            int displayW = Screen.currentResolution.width;
            int displayH = Screen.currentResolution.height;

            int scaleW = displayW / entry.baseW;
            int scaleH = displayH / entry.baseH;
            int scale = Mathf.Max(1, Mathf.Min(scaleW, scaleH));

            targetScreenW = entry.baseW * scale;
            targetScreenH = entry.baseH * scale;
        }

        if (Screen.width != targetScreenW || Screen.height != targetScreenH)
        {
            Screen.SetResolution(targetScreenW, targetScreenH, Screen.fullScreen);
        }

        // If we have an explicit base, pass it so PixelPerfect uses that base.
        // Otherwise (baseW/baseH == 0) ApplyPixelPerfectForScreen will compute a dynamic base
        // while leaving the screen resolution as the chosen display resolution.
        int passBaseW = (entry.baseW > 0) ? entry.baseW : 0;
        int passBaseH = (entry.baseH > 0) ? entry.baseH : 0;

        ApplyPixelPerfectForScreen(targetScreenW, targetScreenH, passBaseW, passBaseH);

        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex);
        PlayerPrefs.Save();
    }

    public void Resume()
    {
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        if(pauseMenu.activeInHierarchy == true)
        {
            Time.timeScale = 0;
            PlayAnimation(optionsAnName);
            //DOSOMETHING TO DEACTIVATE CONTROLS
            //bloom.intensity = bIntensityStrength;
        }
        else
        {
            Time.timeScale = 1;
        }
    }



    public void Quit()
    {
        Application.Quit();
    }

    
    private void PopulateResolutionsDropdown()
    {
        if (resolutionDropdown == null)
            return;

        dropdownEntries.Clear();
        resolutionDropdown.ClearOptions();

        if (pixelResolutions == null || pixelResolutions.Length == 0)
        {
            pixelResolutions = new Vector2Int[]
            {
                new Vector2Int(960,540),
                new Vector2Int(640,360),
                new Vector2Int(768,432),
                new Vector2Int(640,400)
               // new Vector2Int(1920,1080),
               // new Vector2Int(1280,720),
                //new Vector2Int(1440,810)
            };
        }

        List<string> options = new List<string>();

        foreach (var v in pixelResolutions)
        {
            dropdownEntries.Add(new ResEntry(v.x, v.y, true, v.x, v.y));
            options.Add($"{v.x} x {v.y} (pixel)");
        }

        Resolution[] systemRes = Screen.resolutions;

    
        HashSet<string> seen = new HashSet<string>();
        bool anySystemMatch = false;
        foreach (var r in systemRes)
        {
            foreach (var pr in pixelResolutions)
            {
                if (r.width % pr.x == 0 && r.height % pr.y == 0 && r.width * (long)pr.y == r.height * (long)pr.x)
                {
                    string key = $"{r.width}x{r.height}:{pr.x}x{pr.y}";
                    if (seen.Contains(key)) continue;
                    seen.Add(key);
                    dropdownEntries.Add(new ResEntry(r.width, r.height, false, pr.x, pr.y));
                    options.Add($"{r.width} x {r.height} (base {pr.x} x {pr.y})");
                    anySystemMatch = true;
                }
            }
        }

     
        if (!anySystemMatch)
        {
            seen.Clear();
            foreach (var r in systemRes)
            {
                string key = $"{r.width}x{r.height}";
                if (seen.Contains(key)) continue;
                seen.Add(key);
                dropdownEntries.Add(new ResEntry(r.width, r.height, false, 0, 0));
                options.Add($"{r.width} x {r.height}");
            }
        }

        resolutionDropdown.AddOptions(options);

        resolutionDropdown.onValueChanged.RemoveAllListeners();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        
        int currentIdx = -1;
        int bestBaseW = -1;

        int curW = Screen.width;
        int curH = Screen.height;

        if(GameManager.gameManager.resolutionSet == true)
        {
            resolutionDropdown.value = GameManager.gameManager.dropDownValue;
            resolutionDropdown.RefreshShownValue();
            return;

        }
        for (int i = 0; i < dropdownEntries.Count; i++)
        {
            if (dropdownEntries[i].w == curW && dropdownEntries[i].h == curH)
            {
                int bW = dropdownEntries[i].baseW;
                if (bW > bestBaseW)
                {
                    bestBaseW = bW;
                    currentIdx = i;
                }
            }
        }

   
        if (currentIdx < 0)
        {
            Resolution curRes = Screen.currentResolution;
    
            curW = curRes.width;
            curH = curRes.height;

            for (int i = 0; i < dropdownEntries.Count; i++)
            {
                if (dropdownEntries[i].w == curW && dropdownEntries[i].h == curH)
                {
                    int bW = dropdownEntries[i].baseW;
                    if (bW > bestBaseW)
                    {
                        bestBaseW = bW;
                        currentIdx = i;
                    }
                }
            }
        }

        
        if (currentIdx < 0)
        {

            int displayW = Screen.currentResolution.width;
            int displayH = Screen.currentResolution.height;
            float displayAspect = (float)displayW / displayH;
            float bestAspectDiff = float.MaxValue;

            for (int i = 0; i < dropdownEntries.Count; i++)
            {
                int bw = dropdownEntries[i].baseW;
                int bh = dropdownEntries[i].baseH;
                if (bw <= 0 || bh <= 0) continue; 

                float aspect = (float)bw / bh;
                float aspectDiff = Mathf.Abs(aspect - displayAspect);

                if (aspectDiff < bestAspectDiff || (Mathf.Approximately(aspectDiff, bestAspectDiff) && bw > bestBaseW))
                {
                    bestAspectDiff = aspectDiff;
                    bestBaseW = bw;
                    currentIdx = i;
                }
            }
        }

        if (currentIdx >= 0)
        {
            GameManager.gameManager.dropDownValue = currentIdx;
            resolutionDropdown.value = currentIdx;
        }
        else
        {
            GameManager.gameManager.dropDownValue = 0;
            resolutionDropdown.value = 0;
        }
         GameManager.gameManager.resolutionSet = true;  
        resolutionDropdown.RefreshShownValue();
    }

    private Vector2Int ChooseBestPixelRes(int screenW, int screenH)
    {
        // 1. First, check if any of our predefined pixel resolutions fit PERFECTLY (integer scale)
        for (int i = 0; i < pixelResolutions.Length; i++)
        {
            int pw = pixelResolutions[i].x;
            int ph = pixelResolutions[i].y;

            if (screenW % pw == 0 && screenH % ph == 0)
            {
                int scaleW = screenW / pw;
                int scaleH = screenH / ph;
                
                if (scaleW == scaleH) // Perfect integer match!
                {
                    return pixelResolutions[i];
                }
            }
        }

        // 2. If no hardcoded resolution fits perfectly (like 2240x1400), dynamically calculate one!
        // We use your first target resolution's height as a baseline (e.g., 360 or 400)
        int targetHeight = (pixelResolutions != null && pixelResolutions.Length > 0) ? pixelResolutions[0].y : 540;

        // Find the closest whole-number scale (e.g., 2240x1400 with a 360 target gives a scale of 4)
        int scale = Mathf.Max(1, Mathf.RoundToInt((float)screenH / targetHeight));

        // Create a new base resolution that perfectly divides the weird screen size
        int optimalBaseW = screenW / scale;
        int optimalBaseH = screenH / scale;

        return new Vector2Int(optimalBaseW, optimalBaseH);
    }

    private void ApplyPixelPerfectForScreen(int screenW, int screenH, int baseW = 0, int baseH = 0)
    { 
        
        if (pixelPerfectCamera == null)
            pixelPerfectCamera = FindObjectOfType<PixelPerfectCamera>();

        if (pixelPerfectCamera == null)
            return;

        Vector2Int chosen;
        if (baseW > 0 && baseH > 0)
            chosen = new Vector2Int(baseW, baseH);
        else
            chosen = ChooseBestPixelRes(screenW, screenH);

        var type = pixelPerfectCamera.GetType();

        PropertyInfo pRefX = type.GetProperty("refResolutionX", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        PropertyInfo pRefY = type.GetProperty("refResolutionY", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

        if (pRefX != null && pRefY != null && pRefX.CanWrite && pRefY.CanWrite)
        {
            pRefX.SetValue(pixelPerfectCamera, chosen.x, null);
            pRefY.SetValue(pixelPerfectCamera, chosen.y, null);
          
            return;
        }

        PropertyInfo pRef = type.GetProperty("referenceResolution", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
                         ?? type.GetProperty("referenceResolution", BindingFlags.Public | BindingFlags.Instance);
        if (pRef != null && pRef.CanWrite)
        {
            var propType = pRef.PropertyType;
            if (propType == typeof(Vector2))
            {
                pRef.SetValue(pixelPerfectCamera, new Vector2(chosen.x, chosen.y), null);
                //Debug.Log($"PixelPerfect: set referenceResolution (Vector2) to {chosen.x}x{chosen.y}");
                return;
            }
            if (propType == typeof(Vector2Int))
            {
                pRef.SetValue(pixelPerfectCamera, new Vector2Int(chosen.x, chosen.y), null);
                //Debug.Log($"PixelPerfect: set referenceResolution (Vector2Int) to {chosen.x}x{chosen.y}");
                return;
            }
        }

        FieldInfo fRefX = type.GetField("refResolutionX", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
                        ?? type.GetField("m_RefResolutionX", BindingFlags.NonPublic | BindingFlags.Instance);
        FieldInfo fRefY = type.GetField("refResolutionY", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
                        ?? type.GetField("m_RefResolutionY", BindingFlags.NonPublic | BindingFlags.Instance);

        if (fRefX != null && fRefY != null)
        {
            fRefX.SetValue(pixelPerfectCamera, chosen.x);
            fRefY.SetValue(pixelPerfectCamera, chosen.y);
            //Debug.Log($"PixelPerfect: set refResolution fields to {chosen.x}x{chosen.y}");
            return;
        }

        Debug.LogWarning("PixelPerfectCamera: could not find a writable reference resolution property/field. Pixel-perfect settings were not updated.");
    }
}
