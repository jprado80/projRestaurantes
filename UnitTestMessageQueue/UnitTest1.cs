using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestMessageQueue
{

    [ProgId("UnitTest.MessageQueueProcessor")]
    [ComVisible(true)]
    [Guid("82C0C4AA-7183-4B12-A4A2-06FB277AAF12")]
    [TestClass]
    public class UnitTest1
    {
        [ComVisible(true)]
        [TestMethod]
        public void TestMethod1()
        {



            Mensajeria.MessageQueueProcessor objMessage = new Mensajeria.MessageQueueProcessor();
            //objMessage.ProcessMessage("","","");



        }
    }
}
