using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class LoadingSceneBase : MonoBehaviour
{

    private enum _loadingProgressType { Real, Virtual }

    private enum _loadingInfoType { percetage, text, none }

    private AsyncOperation _asynOperation;

    [Header("*** DRAG & DROP THE GAME OBJECTS ***")]
    [SerializeField, Tooltip("Drag and drop the screen image UI game object.")]
    private GameObject _loadingScreenBG;

    [SerializeField, Tooltip("Drag and drop the slider UI game object.")]
    private Slider _progressBar;

    [SerializeField, Tooltip("Drag and drop the loading text UI game object.")]
    private Text _loadingText;

    [Header("*** SCENE NAME TO LOAD ***")]
    [SerializeField, Tooltip("Drag and drop the loading text UI game object.")]
    private string _sceneNameToLoad;

    [Header("*** LOADING BAR CONFIGURATION ***")]
    [SerializeField, Tooltip("Choose the progress type of the loading bar.")]
    private _loadingProgressType _progressType = new _loadingProgressType();

    [SerializeField, Tooltip("Choose the information type of the loading bar.")]
    private _loadingInfoType _infoType = new _loadingInfoType();

    [SerializeField, Tooltip("Active the pause of the percentage loading progress.")]
    private bool _orderToContinue;

    [SerializeField, Tooltip("Insert the info message to continue to the new scene.")]
    private string _pauseMessage = "Press Any Key To Continue";

    [SerializeField, Range(1,5), Tooltip("Choose duration delay in the seconds to the next scene.")]
    private float _secondsToContinue = 1;

    [Header("*** VIRTUAL LOADING PARAMETERS ***")]
    [SerializeField, Range(0,1), Tooltip("Insert the virtual increment of the loading bar progress.")]
    private float _virtualIncrement = 0.2f;

    [SerializeField, Range(0,5), Tooltip("Insert the vritual increment duration timing of the loading bar progress.")]
    private float _virtualTiming = 1f;

    /// ---------------------------------------------------------------
    /// Methods
    /// ---------------------------------------------------------------

    /// <summary>
    /// The pause message activation
    /// </summary>

    private void ActivePauseMessage(){
        if (_orderToContinue == true){
            _loadingText.text = _pauseMessage;
            if (Input.anyKey){
                switch(_progressType){
                    case _loadingProgressType.Real:
                        _asynOperation.allowSceneActivation = true;
                        break;
                    case _loadingProgressType.Virtual:
                        SceneManager.LoadScene(_sceneNameToLoad);
                        break;
                    default:
                        break;
                }
            }
        } else {
            switch (_progressType ){
                case _loadingProgressType.Real:
                    _asynOperation.allowSceneActivation = true;
                    break;
                case _loadingProgressType.Virtual:
                    SceneManager.LoadScene(_sceneNameToLoad);
                    break;
                default:
                    break;
            }
        }
    }


    private void ShowLoadingProgressInfo(){
        if (_progressType == _loadingProgressType.Real){
            float progressfill = Mathf.Clamp01(_asynOperation.progress / 0.9f);
            _progressBar.value = progressfill;

            switch(_infoType){
                case _loadingInfoType.percetage:
                    _loadingText.text = (progressfill * 100).ToString("f0") + "%";
                    break;
                case _loadingInfoType.text:
                    _loadingText.text = "Loading...";
                    break;
                case _loadingInfoType.none:
                    _loadingText.text = "";
                    break;
                default:
                    break;
            }
        }

        if (_progressType == _loadingProgressType.Virtual){

            switch(_infoType){
                case _loadingInfoType.percetage:
                    _loadingText.text = (_progressBar.value * 100).ToString("f0") + "%";
                    break;
                case _loadingInfoType.text:
                    _loadingText.text = "Loading...";
                    break;
                case _loadingInfoType.none:
                    _loadingText.text = "";
                    break;
                default:
                    break;
            }
        }

    }

    private IEnumerator VirtualLoadingProgress(){
        yield return new WaitForSeconds(_secondsToContinue);

        while (_progressBar.value != 1f){
            _progressBar.value += _virtualIncrement;

            if  (_progressBar != null){
                ShowLoadingProgressInfo();
            } else {
                Debug.Log("Progress Bar missing");
            }
            yield return new WaitForSeconds(_virtualTiming);
        }

        while (_progressBar.value == 1f){
            if (!_orderToContinue){
                yield return new WaitForSeconds(_secondsToContinue);
            }

            ActivePauseMessage();
            yield return null;
        }
    }

    private IEnumerator RealLoadingProgress(){
        yield return new WaitForSeconds(_secondsToContinue);

        _asynOperation = SceneManager.LoadSceneAsync(_sceneNameToLoad);
        _asynOperation.allowSceneActivation = false;

        while (!_asynOperation.isDone){
            if (_asynOperation.progress == 0.9f){
                if ( _progressBar != null){
                    ShowLoadingProgressInfo();
                } else {
                    Debug.Log("Progress bar missing");
                }
            }

            if (!_orderToContinue){
                yield return new WaitForSeconds(_secondsToContinue);

            }

            ActivePauseMessage();
        }

        yield return null;
    }

    protected virtual void LoadLevel(){
        if (_sceneNameToLoad == "" || _sceneNameToLoad == null){
            Debug.Log("Missing Scene Name! Insert it!");
            return;
        }

        if (_sceneNameToLoad != "" || _sceneNameToLoad != null){
            if (_loadingScreenBG != null){
                _loadingScreenBG.SetActive(true);
            }

            if (_progressBar != null){
                _progressBar.gameObject.SetActive(true);
            }

            if (_loadingText != null){
                _loadingText.gameObject.SetActive(true);
            }

            if (_progressType == _loadingProgressType.Real){
                StartCoroutine(RealLoadingProgress());
            }
            if (_progressType == _loadingProgressType.Virtual){
                StartCoroutine(VirtualLoadingProgress());
            }
        }
    }

}  


