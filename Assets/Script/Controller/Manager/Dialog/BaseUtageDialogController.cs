using UnityEngine;
using Utage;

namespace Assets.Script.Controller.Manager.Dialog
{
    /// <summary>
    /// 宴ダイアログコントローラーの基底クラス
    /// </summary>
    public class BaseUtageDialogController : MonoBehaviour
	{

		/// <summary>
		/// ADVエンジン
		/// </summary>
		[SerializeField]
		protected AdvEngine _engine;

		/// <summary>
		/// Close
		/// </summary>
		public void Close()
		{
			this.gameObject.SetActive(false);
			Destory();
		}

		/// <summary>
		/// Open
		/// </summary>
		public void Open()
		{
			this.gameObject.SetActive(true);
			Initialize();
		}


		/// <summary>
		/// Update
		/// </summary>
		protected void Update()
		{
			Render();

			//閉じる入力された
			if (InputUtil.IsMouseRightButtonDown() )
			{
				Back();
			}
		}

		/// <summary>
		/// Back
		/// </summary>
		protected virtual void Back()
		{
			this.Close();
			_engine.UiManager.Status = AdvUiManager.UiStatus.Default;
		}

		/// <summary>
		/// Initialize
		/// </summary>
		protected virtual void Initialize()
		{
		}

		/// <summary>
		/// Render
		/// </summary>
		protected virtual void Render()
		{
		}

		/// <summary>
		/// Destory
		/// </summary>
		protected virtual void Destory()
		{
		}


		#region ボタンアクション
		
		/// <summary>
		///  戻るボタンが押された
		/// </summary>
		public void OnClickedBackButton()
		{
			Back();
		}

        #endregion
	}
}
