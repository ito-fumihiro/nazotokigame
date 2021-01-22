using UnityEngine;

namespace Assets.Script.Attach.Game.Quiz.Question
{
    /// <summary>
    /// クイズオブジェクト内のお邪魔がアタッチする
    /// </summary>
    public class Obstacle : MonoBehaviour
	{
		#region SerializeField

		/// <summary>
		/// お邪魔解放アイテムと紐づくID
		/// </summary>
		[SerializeField]
		private string _targetItemId;
		public string TargetItemId { get { return _targetItemId; } }

        #endregion

        /// <summary>
        /// 対象のアイテム番号のオブジェクトを非アクティブにする
        /// </summary>
        public void Release()
        {
			this.gameObject.SetActive(false);
        }        
	}
}