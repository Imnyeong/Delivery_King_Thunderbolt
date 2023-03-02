using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    string inGame = "InGame";
    int buttonSound = 0;
    int playSound = 3;
    void Start() => GameManager.Instance.BGMPlay();
    public void OnClickStart() => SceneManager.LoadScene(inGame);
    // 시작 버튼 =>  InGame Scene으로 이동
    public void OnClickExit() { Application.Quit(); }
    // 종료 버튼 => Application 종료
    public void PlayButtonSound() => GameManager.Instance.EffectPlay(buttonSound);
    public void PlayStartSound() => GameManager.Instance.EffectPlay(playSound);
}
