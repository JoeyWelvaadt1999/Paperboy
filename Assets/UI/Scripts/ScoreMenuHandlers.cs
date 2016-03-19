using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using UnityEngine.Events;

using Extensions;

public class ScoreMenuHandlers : MonoBehaviour 
{
		[SerializeField] private GameObject _settings;
		[SerializeField]private Text _countDown;
	private string HighscoreURL = "http://www.basegames.nl/highscores.pl";

	public Text EndScoreText;
	public Text DistanceScoreText;
	public Text PaperScoreText;

	public float StartDelay = 0.5F;
	public float TypeDelay = 0.01F;

	private Animator Anim;

	public bool IsVisible = false;

	void Start()
	{
				_countDown.enabled = false;
		Anim = GetComponent<Animator>();
	}

	void Update()
	{
		DistanceScoreText.text = "Score: " + Mathf.RoundToInt(Global.Instance.DistanceScore);
		PaperScoreText.text = "Papers: " + Global.Instance.Dollars;
	}

	public void SubmitScore()
	{
		string Name = SystemInfo.deviceName;
		Debug.Log(Name + " Submited the score: " + Mathf.RoundToInt(Global.Instance.TotalScore) );
	}

	public void Home()
	{
		Application.LoadLevel(0);
	}
	public void Retry()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

		public void Settings (bool showSettings) {
				this.transform.FindChild ("PauseMenu-Container").gameObject.SetActive(!showSettings);
				_settings.SetActive (showSettings);
		}

	public void ShowScoreMenu()
	{
		IsVisible = true;

		Anim.SetTrigger("StartScoreFadeIn");
		
		int CurrentTotalScore = Mathf.RoundToInt(Global.Instance.TotalScore);

		StartCoroutine(this.TypeIn("Score: " + Mathf.RoundToInt(Global.Instance.TotalScore), StartDelay, TypeDelay));

		if(CurrentTotalScore > PlayerPrefs.GetInt("Highscore", 0))
			PlayerPrefs.SetInt("Highscore", CurrentTotalScore);
	}
	public void ShowPauseMenu()
	{
		IsVisible = true;
		
		Anim.SetTrigger("StartPauseFadeIn");
	}
	public void HidePauseMenu()
	{
		StartCoroutine (WaitForStart ());

	}

		IEnumerator WaitForStart () {
				_countDown.enabled = true;

				for (int i = 3; i > 0; i--) {
						Anim.SetTrigger("StartPauseFadeOut");
						_countDown.text = i.ToString ();
						yield return new WaitForSeconds (1);
				}
				_countDown.enabled = false;
				IsVisible = false;
				Global.Instance.IsPlaying = true;
		}
}
