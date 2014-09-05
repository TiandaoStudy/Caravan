using FLEX.Common.XML;
using NUnit.Framework;

namespace FLEX.Common.UnitTests.XML
{
   sealed class DynamicXmlTests : TestBase
   {
      [Test]
      public void Parse_SimpleXml_OneElement()
      {
         const string xml = @"
            <UnitTest>
               <Elem>1</Elem>
            </UnitTest>
         ";
         dynamic obj = DynamicXml._getExpandoFromXmlScript(xml);
         Assert.AreEqual("1", obj.Elem);
      }

      [Test]
      public void Parse_SimpleXml_TwoElements()
      {
         const string xml = @"
            <UnitTest>
               <Elems>
                  <Elem>1</Elem>
                  <Elem>2</Elem>
               </Elems> 
            </UnitTest>
         ";
         dynamic obj = DynamicXml._getExpandoFromXmlScript(xml);
         Assert.AreEqual("1", obj.Elems[0]);
         Assert.AreEqual("2", obj.Elems[1]);
      }
   }
}
