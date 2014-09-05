using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace FLEX.Common.XML
{
   public sealed class DynamicXml : DynamicObject
   {
      private readonly XElement _root;

      private DynamicXml(XElement root)
      {
         _root = root;
      }

      public static DynamicXml Parse(string xmlString)
      {
         return new DynamicXml(XDocument.Parse(xmlString).Root);
      }

      public static DynamicXml Load(string filename)
      {
         return new DynamicXml(XDocument.Load(filename).Root);
      }

      public override bool TryGetMember(GetMemberBinder binder, out object result)
      {
         result = null;

         var att = _root.Attribute(binder.Name);
         if (att != null)
         {
            result = att.Value;
            return true;
         }

         var nodes = _root.Elements(binder.Name);
         if (nodes.Count() > 1)
         {
            result = nodes.Select(n => new DynamicXml(n)).ToList();
            return true;
         }

         var node = _root.Element(binder.Name);
         if (node != null)
         {
            if (node.HasElements)
            {
               result = new DynamicXml(node);
            }
            else
            {
               result = node.Value;
            }
            return true;
         }

         return true;
      }


      public static dynamic _getExpandoFromXmlScript(String xml, XElement node = null)
{
    if (String.IsNullOrWhiteSpace(xml) && node == null) return null;
    // If a file is not empty then load the xml and overwrite node with the
    // root element of the loaded document
    if (!String.IsNullOrWhiteSpace(xml))
    {
        var doc = XDocument.Parse(xml);
        node = doc.Root;
    }
    dynamic result = new ExpandoObject();
    var pluralizationService = PluralizationService.CreateService(CultureInfo.CreateSpecificCulture("en-us"));
    foreach (var gn in node.Elements())
    {
        // The code determines if it is a container node based on the child
        // elements with the same name. If there is only one child element,
        // then it uses the PluralizationService to determine if the pluralization of the child elements name 
        // matches the parent node. If so then it's most likely a collection 
        var isCollection = gn.HasElements &&
            ( 
                gn.Elements().Count() > 1 && gn.Elements().All
                (e => e.Name.LocalName.ToLower() == gn.Elements().First().Name.LocalName) ||
                gn.Name.LocalName.ToLower() == pluralizationService.Pluralize
                (gn.Elements().First().Name.LocalName).ToLower()
            );
                    
        var p = result as IDictionary<String, dynamic>;
        var values = new List<dynamic>();
        // If the current node is a container node then we want to skip adding
        // the container node itself, but instead we load the children elements
        // of the current node. If the current node has child elements then load
        // those child elements recursively
        if (isCollection)
            foreach (var item in gn.Elements())
            {
                values.Add((item.HasElements) ? _getExpandoFromXml(null, item) :
                    item.Value.Trim());
            }
        else
            values.Add((gn.HasElements) ? _getExpandoFromXml(null, gn) :
                gn.Value.Trim());

        // Add the object name + value or value collection to the dictionary
        p[gn.Name.LocalName] = isCollection ? values : values.FirstOrDefault();
    }
      if (node.HasAttributes)
{
dynamic attributes = new ExpandoObject();
var a = attributes as IDictionary<string,dynamic>;
foreach (XAttribute attr in node.Attributes().ToList())
{
a[attr.Name.LocalName] = attr.Value;
}
result.attr = a;
}
    return result;
}

      public static dynamic _getExpandoFromXml(String file, XElement node = null)
{
    if (String.IsNullOrWhiteSpace(file) && node == null) return null;
    // If a file is not empty then load the xml and overwrite node with the
    // root element of the loaded document
    if (!String.IsNullOrWhiteSpace(file))
    {
        var doc = XDocument.Load(file);
        node = doc.Root;
    }
    dynamic result = new ExpandoObject();
    var pluralizationService = System.Data.Entity.Design.PluralizationServices.
    PluralizationService.CreateService(CultureInfo.CurrentCulture);
    foreach (var gn in node.Elements())
    {
        // The code determines if it is a container node based on the child
        // elements with the same name. If there is only one child element,
        // then it uses the PluralizationService to determine if the pluralization of the child elements name 
        // matches the parent node. If so then it's most likely a collection 
        var isCollection = gn.HasElements &&
            ( 
                gn.Elements().Count() > 1 && gn.Elements().All
                (e => e.Name.LocalName.ToLower() == gn.Elements().First().Name.LocalName) ||
                gn.Name.LocalName.ToLower() == pluralizationService.Pluralize
                (gn.Elements().First().Name.LocalName).ToLower()
            );
                    
        var p = result as IDictionary<String, dynamic>;
        var values = new List<dynamic>();
        // If the current node is a container node then we want to skip adding
        // the container node itself, but instead we load the children elements
        // of the current node. If the current node has child elements then load
        // those child elements recursively
        if (isCollection)
            foreach (var item in gn.Elements())
            {
                values.Add((item.HasElements) ? _getExpandoFromXml(null, item) :
                    item.Value.Trim());
            }
        else
            values.Add((gn.HasElements) ? _getExpandoFromXml(null, gn) :
                gn.Value.Trim());

        // Add the object name + value or value collection to the dictionary
        p[gn.Name.LocalName] = isCollection ? values : values.FirstOrDefault();
    }
    return result;
}
   }
}