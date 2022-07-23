using UnityEngine;
using UnityEngine.SceneManagement;


public class Level : MonoBehaviour
{
   [Header("UI References")] 
   [SerializeField] private GameObject levelSucceedPanel;
   [SerializeField] private GameObject levelFailedPanel;
   
   
   private int _ballCount;
   private Tube _tube;
   private bool _isFailed;


   #region Life Cycle

   private void Start()
   {
      _tube = GetComponentInChildren<Tube>();
      levelSucceedPanel.SetActive(false);
      levelFailedPanel.SetActive(false);
   }

   #endregion
   
   
   #region Ball

   public void BallDidFallOff()
   {
      _ballCount++;
      _isFailed = true;
      CheckAllBallsProcessed();
   }
   
   public void BallDidGetInCup()
   {
      _ballCount++;
      CheckAllBallsProcessed();
   }


   private void CheckAllBallsProcessed()
   {
      if (_ballCount != _tube.BallCount) 
         return;
      
      levelFailedPanel.SetActive(_isFailed);
      levelSucceedPanel.SetActive(!_isFailed);
   }
   #endregion
   
   
   #region User Interaction

   public void LevelFailedButtonPressed()
   {
      var sameSceneIdx = SceneManager.GetActiveScene().buildIndex;
      SceneManager.LoadScene(sameSceneIdx);
   }
    
   public void LevelSucceedButtonPressed()
   {
      var nextSceneIdx = SceneManager.GetActiveScene().buildIndex + 1;
      if (nextSceneIdx >= SceneManager.sceneCountInBuildSettings)
      {
         nextSceneIdx = Random.Range(0, SceneManager.sceneCountInBuildSettings);
      }
      SceneManager.LoadScene(nextSceneIdx);
   }

   #endregion
 
}
