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

namespace FLEX.RestService.Core
{
	public partial class Menu
	{
		public static Menu DeserializeFrom(Stream stream)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Menu));
			return (Menu)serializer.Deserialize(stream);
		}

		public static Menu DeserializeFrom(TextReader reader)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Menu));
			return (Menu)serializer.Deserialize(reader);
		}

		public static Menu DeserializeFrom(XmlReader reader)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Menu));
			return (Menu)serializer.Deserialize(reader);
		}

		public void SerializeTo(Stream stream)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Menu));
			serializer.Serialize(stream, this);
		}

		public void SerializeTo(TextWriter writer)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Menu));
			serializer.Serialize(writer, this);
		}

		public void SerializeTo(XmlWriter writer)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Menu));
			serializer.Serialize(writer, this);
		}
	}
}