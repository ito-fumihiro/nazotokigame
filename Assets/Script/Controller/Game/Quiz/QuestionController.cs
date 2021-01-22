using Assets.Script.Attach.Game.Quiz.Question;
using Assets.Script.Model;
using Assets.Script.Utility;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Controller.Game.Quiz
{
	/// <summary>
	/// QuestionController
	/// </summary>
	public class QuestionController : MonoBehaviour
	{
		#region 参照用

		/// <summary>
		/// クイズの回答フィールド群の参照
		/// </summary>
		private GameObject _inputFields;

		/// <summary>
		/// クイズの障害物群の参照
		/// </summary>
		private GameObject _obstacles;

		#endregion

		/// <summary>
		/// クイズの正答
		/// </summary>
		private string _correctAnswer;
		
		/// <summary>
		/// Start
		/// </summary>
        private void Start()
        {
			// 先に参照を取得しておく
			var QuizQuestionObject = GameUtil.GetQuizController().QuestionObject;
			_inputFields = QuizQuestionObject.transform.Find("InputFields").gameObject;
			_obstacles = QuizQuestionObject.transform.Find("Obstacles").gameObject;
		}

		/// <summary>
		/// クイズのパラメータをロードデータより読み込む
		/// </summary>
		/// <param name="quizInfo">クイズ情報</param>
		public void SetQuestionParam(QuizInfo quizInfo)
        {
			_correctAnswer = quizInfo.CorrectAnswer;
		}

		/// <summary>
		/// アイテム情報に紐づく障害物を非アクティブにする
		/// </summary>
		/// <param name="itemId">アイテムID</param>
		public void ReleaseQuestionObstacles(string itemId)
		{
			var obstacles = _obstacles.transform.GetComponentsInChildren<Obstacle>().ToList();
			foreach(var obs in obstacles)
            {
				if(obs.TargetItemId == itemId)
                {
					obs.Release();
				}
			}
		}

		/// <summary>
		/// 正解かどうかをチェックする
		/// </summary>
		/// <returns>正解ならtrue</returns>
		public bool CheckAnswer()
        {
			// InputFieldの入力値を連結して回答の文字列を生成
			var fields = _inputFields.GetComponentsInChildren<InputField>().OrderBy(x => x.name).ToList();
			string answers = ""; 
			foreach(var field in fields)
            {
				answers += field.text;
            }

			if(answers == _correctAnswer)
            {
				return true;
            }
            else
            {
				return false;
            }
        }
        
	}
}