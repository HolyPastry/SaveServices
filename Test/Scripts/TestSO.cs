
using UnityEngine;

namespace Bakery.Saves.Test
{
    [CreateAssetMenu(fileName = "SaveData", menuName = "Bakery/Test/SaveData", order = 0)]
    public class TestSO : ScriptableObject
    {
        public string TestSOString = "TestSOString";
    }

}
