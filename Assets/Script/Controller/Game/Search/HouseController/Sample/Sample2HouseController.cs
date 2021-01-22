using Assets.Script.Enum;
using Assets.Script.Utility;
using UnityEngine;

namespace Assets.Script.Controller.Game.Search.HouseController.Sample
{
    /// <summary>
    /// 開発用のサンプルHouse2のコントローラー
    /// </summary>
    public class Sample2HouseController : BaseHouseController
	{
		#region SerializeField

		/// <summary>
		/// 金庫
		/// </summary>
		[SerializeField]
		private GameObject _safe;

		/// <summary>
		/// 旗
		/// </summary>
		[SerializeField]
		private GameObject _flag;

        #endregion

        #region アクション定義

		/// <summary>
		/// 国旗をクリックしたとき
		/// </summary>
        public void ClickedFlag()
		{
			DisplayMessage("世界の国旗だ！");
		}

		/// <summary>
		/// 金庫をクリックしたとき
		/// </summary>
		public void ClickedSafe()
		{
			StartSenario("Test2", CompletePigClickedScenario);

			void CompletePigClickedScenario()
			{
				EndSenario();

				// 個別の終了処理
				GameUtil.GetGameSceneController().ChangeGameMode(GameMode.Quiz);
				var quizId = "quiz_sample_2";
				GameUtil.GetQuizController().SetupQuizGroup(quizId);
			}
		}

        #endregion
    }
}