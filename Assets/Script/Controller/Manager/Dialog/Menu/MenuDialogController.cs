using Assets.Script.Utility;
using UnityEngine;
using Utage;

namespace Assets.Script.Controller.Manager.Dialog.Menu
{
    /// <summary>
    /// メニューダイアログ
    /// </summary>
    [AddComponentMenu("Utage/ADV/MenuDialogController")]
	public class MenuDialogController : BaseUtageDialogController
	{

		#region ボタンアクション

		/// <summary>
		/// コンフィグボタンが押された
		/// </summary>
		public virtual void OnClickedConfigButton()
		{
			_engine.UiManager.Status = AdvUiManager.UiStatus.Config;
		}

		/// <summary>
		/// バックログボタンが押された
		/// </summary>
		public virtual void OnClickedBackLogButton()
		{
			_engine.UiManager.Status = AdvUiManager.UiStatus.Backlog;
		}

		/// <summary>
		/// タイトルボタンが押された
		/// </summary>
		public virtual void OnClickedTitileButton()
		{
			StartCoroutine(GameUtil.CoUnloadCurrentSceneAndLoadNextScene("Game", "Title"));
		}

		#endregion
	}
}
