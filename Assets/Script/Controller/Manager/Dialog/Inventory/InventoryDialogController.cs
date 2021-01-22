using Assets.Script.Enum;
using Assets.Script.Manager.Dialog.Inventory;
using Assets.Script.Utility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Controller.Manager.Dialog.Inventory
{

    /// <summary>
    /// インベントリダイアログ
    /// </summary>
    [AddComponentMenu("Utage/ADV/InventoryDialogController")]
	public class InventoryDialogController : BaseUtageDialogController
	{
		/// <summary>
		/// 装備アイテムグリッド
		/// </summary>
		[SerializeField]
		private GameObject _equipmentGrid;

		/// <summary>
		/// UseRoomsアイテムグリッド
		/// </summary>
		[SerializeField]
		private GameObject _useRoomsGrid;

		/// <summary>
		/// BossRoomアイテムグリッド
		/// </summary>
		[SerializeField]
		private GameObject _useBossRoomGrid;

		/// <summary>
		/// スロットのコレクション
		/// </summary>
		private List<GameObject> _slotList;

		/// <summary>
		/// スロットオブジェクトのファイルパス
		/// </summary>
		private const string _slotObjectFilePath = "Utage/Prefab/Inventory/Slot";

		/// <summary>
		/// Initialize
		/// </summary>
		protected override void Initialize()
        {
            base.Initialize();
			InitDisplaySlots();
		}

		/// <summary>
		/// Destory
		/// </summary>
		protected override void Destory()
		{
			base.Destory();
		}

		/// <summary>
		/// アイテムスロットの初期化
		/// </summary>
		private void InitDisplaySlots()
        {
			// TODO:一旦ここで
			if(_slotList == null)
            {
				_slotList = new List<GameObject>();
			}

			// 前回表示したアイテムは破棄する
			if (_slotList.Count > 0)
            {
				foreach(var slot in _slotList)
                {
					Destroy(slot);
				}
			}

			var itemIdList = GameUtil.GetGameSceneController().CurrentGetItemList;
			int count = 0;
			foreach (var itemId in itemIdList)
            {
				count++;

				var itemInfo = GameUtil.GetGameSceneController().ItemInfoList.Where(x =>x.Id == itemId).FirstOrDefault();
				
				GameObject slot = (GameObject)Resources.Load(_slotObjectFilePath);
				GameObject prefab = (GameObject)Instantiate(slot);

				// 装備可能アイテム
				if(itemInfo.Type == ItemType.Equipment)
                {
					prefab.transform.SetParent(_equipmentGrid.transform, false);
				}
				// ボス部屋以外で使用可能
				else if (itemInfo.Type == ItemType.UseRooms)
                {
					prefab.transform.SetParent(_useRoomsGrid.transform, false);
				}
				// ボス部屋で使用可能
				else if (itemInfo.Type == ItemType.UseBossRoom)
                {
					prefab.transform.SetParent(_useBossRoomGrid.transform, false);
				}
				
				prefab.GetComponent<Slot>().ItemId = itemId;
				var itemImage = prefab.transform.Find("ItemImage").gameObject.GetComponent<Image>();
				itemImage.sprite = itemInfo.SpThm;
				
				_slotList.Add(prefab);
			}
		}
	}
}
