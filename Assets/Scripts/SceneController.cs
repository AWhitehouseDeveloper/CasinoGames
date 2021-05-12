using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    class SceneController : MonoBehaviour
    {
        public bool soundToMenu = true;
        static SceneController instance = null;
        public static SceneController Instance { get { return instance; } }
     
        public void Awake()
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        public void OnLoadScene(string sceneString)
        {
            StartCoroutine(LoadScene(sceneString));
        }

        IEnumerator LoadScene(string sceneString)
        {
            SceneManager.LoadScene(sceneString);

            yield return null;
        }
    }
}
