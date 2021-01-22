using Assets.Script.Enum;
using Assets.Script.Utility;
using UnityEngine;

namespace Assets.Script.Controller.Game.Search.HouseController.Sample
{
    /// <summary>
    /// 開発用のサンプルHouseのコントローラー
    /// </summary>
    public class SampleHouseController : BaseHouseController
	{
		#region SerializeField

		/// <summary>
		/// ダイヤモンド
		/// </summary>
		[SerializeField]
		private GameObject _diamond;

		/// <summary>
		/// メモ
		/// </summary>
		[SerializeField]
		private GameObject _memo;

		/// <summary>
		/// 豚
		/// </summary>
		[SerializeField]
		private GameObject _pig;

        #endregion

        #region const

        /// <summary>
        ///  ダイヤモンドのアイテムID
        /// </summary>
        private const string _diamondItemId = "item_sample_3";

		/// <summary>
		/// ダイヤモンド押下時に再生されるシナリオラベル
		/// </summary>
		private const string _diamondScenario = "Test3";

        #endregion

        #region アクションイベント

        /// <summary>
        /// ダイヤモンド押下時
        /// </summary>
        public void ClickedDiamond()
		{
			StartSenario(_diamondScenario, CompleteDiamondClickedScenario);

			void CompleteDiamondClickedScenario()
			{
				EndSenario();

				// 個別の終了処理
				GameUtil.GetGameSceneController().GetItem(_diamondItemId);
				_diamond.SetActive(false);
			}
		}

        /// <summary>
        /// メモ押下時
        /// </summary>
        public void ClickedMemo()
		{
			DisplayMessage("タッチしたときのコメントを出します");
		}

		/// <summary>
		/// 豚押下時
		/// </summary>
		public void ClickedPig()
		{
			StartSenario("Test2", CompletePigClickedScenario);

			void CompletePigClickedScenario()
			{
				EndSenario();

				// 個別の終了処理
				GameUtil.GetGameSceneController().ChangeGameMode(GameMode.Quiz);
				var quizId = "quiz_sample_1";
				GameUtil.GetQuizController().SetupQuizGroup(quizId);
			}
		}

		#endregion

	}
}