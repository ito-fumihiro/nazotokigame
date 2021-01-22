using Assets.Script.Attach.Game.Map;
using Assets.Script.Enum;
using Assets.Script.Utility;
using System.Linq;
using UnityEngine;

namespace Assets.Script.Controller.Game.Map
{
    /// <summary>
    /// MapController
    /// </summary>
    public class MapController : MonoBehaviour
    {
        #region SerializeField

        /// <summary>
        /// マップグループ
        /// </summary>
        [SerializeField]
        private GameObject _mapGroup;

        /// <summary>
        /// マップステージキャンバス
        /// </summary>
        [SerializeField]
        private GameObject _stageCanvas;

        #endregion


        #region 参照用

        /// <summary>
        /// マップステージオブジェクトへの参照
        /// </summary>
        private GameObject _stageObject;

        #endregion


        /// <summary>
        /// マップステージオブジェクトを配置し、セットアップする
        /// </summary>
        /// <param name="stageId">ステージID</param>
        public void SetupMapObject(string stageId)
        {
            DestroyMapStageObject();

            // StageCanvasにStageオブジェクトを読み込む
            var gameController = GameUtil.GetGameSceneController();
            var stageInfo = gameController.StageInfoList.Where(x => x.Id == stageId).FirstOrDefault();
            gameController.CurrentStageInfo = stageInfo;

            var stageObjectPath = stageInfo.StageObjectFilePath;

            GameObject stage = (GameObject)Resources.Load(stageInfo.StageObjectFilePath);
            GameObject prefab = (GameObject)Instantiate(stage);
            prefab.transform.SetParent(_stageCanvas.transform, false);
            _stageObject = prefab;
            _mapGroup.SetActive(true);

            // マップボタンを初期化
            ReleaseHouseButton(1);
        }

        /// <summary>
        /// 配置したマップステージオブジェクトを破棄する
        /// </summary>
        private void DestroyMapStageObject()
        {
            if (_stageObject)
            {
                Destroy(_stageObject);
            }
        }

        /// <summary>
        /// ハウスボタンを開放する
        /// </summary>
        /// <param name="releaseOrder">解放されるハウスの順番</param>
        public void ReleaseHouseButton(int releaseOrder)
        {
            var houseButtons = _stageObject.transform.GetComponentsInChildren<HouseButton>().ToList();

            foreach (var houseButton in houseButtons)
            {
                // クリアされたもの
                if (houseButton.ReleaseOrder < releaseOrder)
                {
                    houseButton.SetStatus(HouseButtonStatus.Complete);
                }
                // 解放されるもの
                else if (houseButton.ReleaseOrder == releaseOrder)
                {
                    houseButton.SetStatus(HouseButtonStatus.Enabled);
                }// 解放されるもの
                else
                {
                    houseButton.SetStatus(HouseButtonStatus.Disabled);
                }
            }
        }
    }
}
