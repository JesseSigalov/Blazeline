using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadIndex(int index)
    {
        if (index > 0)
            Cursor.visible = false;
        else
            Cursor.visible = true;
 
        SceneManager.LoadScene(index);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().buildIndex != 0)
                LoadIndex(0);
        }
    }
}
