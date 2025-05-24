using System.Collections;
using Holypastry.Bakery;
using UnityEngine;
namespace Bakery.Saves.Test
{
    public class SaveSystemUnitTest : MonoBehaviour
    {
        [SerializeField] private TestSO _testSO;

        private DataCollection<TestSO> _collection;

        void Awake()
        {
            _collection = new DataCollection<TestSO>("Tests");
        }

        IEnumerator Start()
        {
            var data = new TestSaveData
            {
                TestSO = _testSO
            };

            SaveServices.DeleteSave();
            SaveServices.Save("Key", data);

            yield return new WaitForSeconds(1f);

            var loadedData = SaveServices.Load<TestSaveData>("Key");
            loadedData.TestSO = _collection.GetFromName(loadedData.TestSOName);

            Debug.Log(loadedData.TestString);
            Debug.Log(loadedData.TestInt);
            Debug.Log(loadedData.TestList.Count);
            Debug.Log(loadedData.TestSO.TestSOString);
        }
    }
}
