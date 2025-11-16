
using System;
using System.Collections.Generic;

namespace Bakery.Persistences.Test
{


    [Serializable]
    public class TestSaveData : ISerialData
    {

        public string TestString = "TestString";

        [NonSerialized]
        public TestSO TestSO;
        public string TestSOName;


        public int TestInt = 1;

        public List<string> TestList = new() { "Test1", "Test2", "Test3" };

        public void Deserialize()
        { }

        public void Serialize()
        {
            TestSOName = TestSO.name;
        }
    }
}
