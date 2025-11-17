using System.Collections;
using Holypastry.Bakery;
using UnityEngine;
namespace Bakery.Persistences.Test
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

            Persistence.Manager().DeleteSaveFile();
            Persistence.Manager().Cache("Key", data);

            yield return new WaitForSeconds(1f);

            var loadedData = Persistence.Manager().LoadOrCreate<TestSaveData>("Key");
            loadedData.TestSO = _collection.GetFromName(loadedData.TestSOName);

            Debug.Log(loadedData.TestString);
            Debug.Log(loadedData.TestInt);
            Debug.Log(loadedData.TestList.Count);
            Debug.Log(loadedData.TestSO.TestSOString);
        }
    }
}
