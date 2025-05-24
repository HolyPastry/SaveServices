
using System;
using System.Collections.Generic;

namespace Bakery.Saves.Test
{


    [Serializable]
    public class TestSaveData : SerialData
    {

        public string TestString = "TestString";

        [NonSerialized]
        public TestSO TestSO;
        public string TestSOName;


        public int TestInt = 1;

        public List<string> TestList = new() { "Test1", "Test2", "Test3" };

        public override void Deserialize()
        { }

        public override void Serialize()
        {
            TestSOName = TestSO.name;
        }
    }
}
