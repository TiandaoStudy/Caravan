//------------------------------------------------------------------------------
// <auto-generated>
//     This source code was auto-generated by XsdClassGen.tt.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace FLEX.WebForms
{
	public partial class webMarkupMin
	{
		public static webMarkupMin DeserializeFrom(Stream stream)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(webMarkupMin));
			return (webMarkupMin)serializer.Deserialize(stream);
		}

		public static webMarkupMin DeserializeFrom(TextReader reader)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(webMarkupMin));
			return (webMarkupMin)serializer.Deserialize(reader);
		}

		public static webMarkupMin DeserializeFrom(XmlReader reader)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(webMarkupMin));
			return (webMarkupMin)serializer.Deserialize(reader);
		}

		public void SerializeTo(Stream stream)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(webMarkupMin));
			serializer.Serialize(stream, this);
		}

		public void SerializeTo(TextWriter writer)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(webMarkupMin));
			serializer.Serialize(writer, this);
		}

		public void SerializeTo(XmlWriter writer)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(webMarkupMin));
			serializer.Serialize(writer, this);
		}
	}
}
