using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//здесь хранятся все функции которые можно отнести к разным частям проекта 

namespace Wanderer.Utils
{


    public static class Utils
    {
        //возвращает рандомное случайное направление 
        public static Vector3 GetRandomDir()
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
    }
}