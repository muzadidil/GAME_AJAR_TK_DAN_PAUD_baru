using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        Debug.Log("[ReturnToMenu] Click detected, loading MainMenu...");
        SceneManager.LoadScene("MainMenu");
    }
}
