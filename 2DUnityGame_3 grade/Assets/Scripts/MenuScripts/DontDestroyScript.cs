﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

class DontDestroyScript: MonoBehaviour
{
    public void ChangingScene(GameObject field)
    {
        DontDestroyOnLoad(field);
    }
}
