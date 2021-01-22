using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Script.Controller.Manager.Dialog.Config
{
    /// <summary>
    /// ManagerSceneControleer
    /// </summary>
    public class ManagerSceneControleer : MonoBehaviour
    {
        /// <summary>
        /// Start
        /// </summary>
        void Start()
        {
            // Managerシーンは残存させて次のシーンへ
            SceneManager.LoadScene("Title", LoadSceneMode.Additive);
        }
    }
}
