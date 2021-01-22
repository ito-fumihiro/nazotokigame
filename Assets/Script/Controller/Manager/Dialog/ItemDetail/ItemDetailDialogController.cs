using Assets.Script.Enum;
using Assets.Script.Utility;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utage;

namespace Assets.Script.Controller.Manager.Dialog.ItemDetail
{
    /// <summary>
    /// アイテム詳細ダイアログ
    /// </summary>
    [AddComponentMenu("Utage/ADV/ItemDetailDialogController")]
	public class ItemDetailDialogController : BaseUtageDialogController
	{
		/// <summary>
		/// 表示アイテムID
		/// </summary>
		private string _displayItemId;
		public string DisplayItemId { get { return _displayItemId; } set { _displayItemId = value; } }

		/// <summary>
		/// 表示アイテムのイメージ
		/// </summary>
		[SerializeField]
		private GameObject _displayItemImage;

		/// <summary>
		/// 表示アイテムの詳細テキスト
		/// </summary>
		[SerializeField]
		private GameObject _displayItemDescriptionText;

		/// <summary>
		/// アイテム装備ボタン
		/// </summary>
		[SerializeField]
		private GameObject _itemEquipButton;

		/// <summary>
		/// アイテム使用ボタン
		/// </summary>
		[SerializeField]
		private GameObject _itemUseButton;

		/// <summary>
		/// Initialize
		/// </summary>
		protected override void Initialize()
        {
            base.Initialize();

			// アイテム画像設定
			var itemInfo = GameUtil.GetGameSceneController().ItemInfoList.Where(x=>x.Id == DisplayItemId).FirstOrDefault();

			var itemImage = _displayItemImage.GetComponent<Image>();
			itemImage.sprite = itemInfo.SpOrg;

			var itemText = _displayItemDescriptionText.GetComponent<Text>();
			itemText.text = itemInfo.Description;

			// ボタンのアクティブ制御
			// TODO: ここの分岐なんとかならないかね？
			if(itemInfo.Type == ItemType.Equipment &&
				GameUtil.GetGameSceneController().CurrentGameMode == GameMode.Seach )
			{
				_itemEquipButton.SetActive(true);
				_itemUseButton.SetActive(false);
			}
            else if(itemInfo.Type == ItemType.UseRooms &&
				GameUtil.GetGameSceneController().CurrentGameMode == GameMode.Quiz &&
				GameUtil.GetQuizController().CurrentQuizMode == QuizMode.Question)
            {
				_itemEquipButton.SetActive(false);
				_itemUseButton.SetActive(true);
            }
            else
            {
				_itemEquipButton.SetActive(false);
				_itemUseButton.SetActive(false);
			}
		}

		/// <summary>
		/// Back
		/// </summary>
		protected override void Back()
		{
			base.Back();
			_engine.UiManager.Status = AdvUiManager.UiStatus.Inventory;
		}

        #region アクション定義

		/// <summary>
		/// 装備ボタンをクリック
		/// </summary>
		public void ClickedEquipButton()
        {
			GameUtil.GetGameSceneController().EquipItem(DisplayItemId);
			
        }

		/// <summary>
		/// 使用ボタンをクリック
		/// </summary>
		public void ClickedUseButton()
		{
			GameUtil.GetGameSceneController().UseItem(DisplayItemId);
		}

		#endregion
	}
}
