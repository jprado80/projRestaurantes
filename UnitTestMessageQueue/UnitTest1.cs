using System;
using System.EnterpriseServices;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestMessageQueue
{
    
    public class UnitTest1 
        //: ServicedComponent
    {
       // [ComVisible(true)]
        [TestMethod]
        public void TestMethod1()
        {



            Mensajeria.MessageQueueProcessor objMessage = new Mensajeria.MessageQueueProcessor();
            objMessage.ProcessMessage(null,"");



        }
    }
}
