using Assets.Script.Utility;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Controller.Game.Search.HouseController
{
    /// <summary>
    /// Roomオブジェクトの基底クラス。
    /// 継承したクラスをSearchRoomオブジェクトがアタッチする
    /// </summary>
    public abstract class BaseHouseController : MonoBehaviour
	{
		/// <summary>
		/// 探索画面のメッセージ領域にメッセージを表示
		/// </summary>
		/// <param name="msg">表示するメッセージ</param>
		protected void DisplayMessage(string msg)
		{
			var messageTextObj = GameUtil.GetSearchController().MessageButtonText;
			var buttonMessageObj = GameUtil.GetSearchController().MessageButton;

			messageTextObj.GetComponent<Text>().text = msg;
			buttonMessageObj.SetActive(true);
		}

		/// <summary>
		/// シナリオを開始する
		/// </summary>
		/// <param name="msg">表示するメッセージ</param>
		protected void StartSenario(string scenarioLabel, Action complete)
		{
			GameUtil.GetSearchController().SetSearchCanvasesEnabled(false);
			UtageUtil.GetNazotokiAdvEngineController().JumpScenario(scenarioLabel, complete);

		}

		/// <summary>
		/// シナリオを終了する
		/// </summary>
		protected void EndSenario()
		{
			GameUtil.GetSearchController().SetSearchCanvasesEnabled(true);
		}
	}
}