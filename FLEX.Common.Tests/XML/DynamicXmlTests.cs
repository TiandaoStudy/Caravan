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
         dynamic obj = DynamicXml.Parse(xml);
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
         dynamic obj = DynamicXml.Parse(xml);
         Assert.AreEqual("1", obj.Elems.Elem[0].Value);
         Assert.AreEqual("2", obj.Elems.Elem[1].Value);
      }

      [Test]
      public void Parse_SimpleXml_OneAttribute()
      {
         const string xml = @"
            <UnitTest>
               <Elems Value=""1"">
                  <Elem>1</Elem>
                  <Elem>2</Elem>
               </Elems> 
            </UnitTest>
         ";
         dynamic obj = DynamicXml.Parse(xml);
         Assert.AreEqual("1", obj.Elems.Value);
      }
   }
}
