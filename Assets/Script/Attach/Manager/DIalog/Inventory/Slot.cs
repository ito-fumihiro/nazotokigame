using Assets.Script.Controller.Manager.Dialog.ItemDetail;
using Assets.Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utage;

namespace Assets.Script.Manager.Dialog.Inventory
{
    /// <summary>
    /// 継承したクラスをインベントリのスロットオブジェクトがアタッチする
    /// </summary>
    public class Slot : MonoBehaviour
	{
		/// <summary>
		/// アイテムID
		/// </summary>
		private string _itemId;
		public string ItemId { get { return _itemId; } set { _itemId = value; } }


		#region アクション定義

		/// <summary>
		/// インベントリアイテムがクリックされた時
		/// </summary>
		public void Clicked() {

			// クリックされたアイテムのIDをItemDetailダイアログに渡す
			// TODO: 最適化したい
			Scene scene = SceneManager.GetSceneByName("Manager");
			ItemDetailDialogController itemDetailDialogController = null;
			foreach (var rootGameObject in scene.GetRootGameObjects())
			{
				var ui = rootGameObject.transform.Find("UI");
                if (ui == null){
					continue;
				}

				var itemDetail = ui.transform.Find("ItemDetail");
				itemDetailDialogController = itemDetail.GetComponent<ItemDetailDialogController>();
				break;
			}
			itemDetailDialogController.DisplayItemId = _itemId;

			// ステータス変更
			UtageUtil.ChangeNazotokiAdvUiStatus(AdvUiManager.UiStatus.ItemDetail);

		}

        #endregion
    }
}