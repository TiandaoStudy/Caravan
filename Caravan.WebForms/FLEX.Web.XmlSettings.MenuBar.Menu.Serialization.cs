//-----------------------------------------------------------------------------------------------------
// <auto-generated>
//     This source code was auto-generated by XsdClassGen.tt.
//     Runtime Version: 4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace FLEX.Web.XmlSettings.MenuBar
{
    /// <summary>
    ///   Automatically generated mapping for the Menu XML element
    ///   declared in the FLEX.Web.XmlSettings.MenuBar namespace.
    /// </summary>
	public partial class Menu
	{
        /// <summary>
        ///   Deserializes given string into an instance of <see cref="Menu" />.
        /// </summary>
        /// <param name="str">The string from which an instance of <see cref="Menu" /> should be deserialized.</param>
        /// <returns>An instance of <see cref="Menu" /> deserialized from given string.</returns>
        /// <exception cref="ArgumentException">Given string is null or empty.</exception>
		public static Menu DeserializeFrom(string str)
		{
            if (string.IsNullOrEmpty(str)) {
                throw new ArgumentException("Cannot deserialize from a null or empty string.", "str");
            }
			var serializer = new XmlSerializer(typeof(Menu));
		    using (var stream = new StringReader(str)) {
			    return serializer.Deserialize(stream) as Menu;
		    }
		}        
        
        /// <summary>
        ///   Deserializes given stream into an instance of <see cref="Menu" />.
        /// </summary>
        /// <param name="stream">The stream from which an instance of <see cref="Menu" /> should be deserialized.</param>
        /// <returns>An instance of <see cref="Menu" /> deserialized from given stream.</returns>
        /// <exception cref="ArgumentNullException">Given stream is null.</exception>
		public static Menu DeserializeFrom(Stream stream)
		{
            if (stream == null) {
                throw new ArgumentNullException("stream");
            }
			var serializer = new XmlSerializer(typeof(Menu));
			return serializer.Deserialize(stream) as Menu;
		}

        /// <summary>
        ///   Deserializes given reader into an instance of <see cref="Menu" />.
        /// </summary>
        /// <param name="reader">The reader from which an instance of <see cref="Menu" /> should be deserialized.</param>
        /// <returns>An instance of <see cref="Menu" /> deserialized from given reader.</returns>
        /// <exception cref="ArgumentNullException">Given reader is null.</exception>
		public static Menu DeserializeFrom(TextReader reader)
		{            
            if (reader == null) {
                throw new ArgumentNullException("reader");
            }
			var serializer = new XmlSerializer(typeof(Menu));
			return serializer.Deserialize(reader) as Menu;
		}
        
        /// <summary>
        ///   Deserializes given reader into an instance of <see cref="Menu" />.
        /// </summary>
        /// <param name="reader">The reader from which an instance of <see cref="Menu" /> should be deserialized.</param>
        /// <returns>An instance of <see cref="Menu" /> deserialized from given reader.</returns>
        /// <exception cref="ArgumentNullException">Given reader is null.</exception>
		public static Menu DeserializeFrom(XmlReader reader)
		{            
            if (reader == null) {
                throw new ArgumentNullException("reader");
            }
			var serializer = new XmlSerializer(typeof(Menu));
			return serializer.Deserialize(reader) as Menu;
		}
        
        /// <summary>
        ///   Serializes this instance of <see cref="Menu" /> into given string.
        /// </summary>
        /// <param name="str">The string into which the instance of <see cref="Menu" /> should be serialized.</param>
		public void SerializeTo(ref string str)
		{
			var serializer = new XmlSerializer(typeof(Menu));
            using (var stream = new StringWriter()) {
			    serializer.Serialize(stream, this);
                str = stream.ToString();
            }
		}
        
        /// <summary>
        ///   Serializes this instance of <see cref="Menu" /> into given stream.
        /// </summary>
        /// <param name="stream">The stream into which the instance of <see cref="Menu" /> should be serialized.</param>
        /// <exception cref="ArgumentNullException">Given stream is null.</exception>
		public void SerializeTo(Stream stream)
		{            
            if (stream == null) {
                throw new ArgumentNullException("stream");
            }
			var serializer = new XmlSerializer(typeof(Menu));
			serializer.Serialize(stream, this);
		}
        
        /// <summary>
        ///   Serializes this instance of <see cref="Menu" /> into given writer.
        /// </summary>
        /// <param name="writer">The writer into which the instance of <see cref="Menu" /> should be serialized.</param>
        /// <exception cref="ArgumentNullException">Given writer is null.</exception>
		public void SerializeTo(TextWriter writer)
		{            
            if (writer == null) {
                throw new ArgumentNullException("writer");
            }
			var serializer = new XmlSerializer(typeof(Menu));
			serializer.Serialize(writer, this);
		}
        
        /// <summary>
        ///   Serializes this instance of <see cref="Menu" /> into given writer.
        /// </summary>
        /// <param name="writer">The writer into which the instance of <see cref="Menu" /> should be serialized.</param>
        /// <exception cref="ArgumentNullException">Given writer is null.</exception>
		public void SerializeTo(XmlWriter writer)
		{            
            if (writer == null) {
                throw new ArgumentNullException("writer");
            }
			var serializer = new XmlSerializer(typeof(Menu));
			serializer.Serialize(writer, this);
		}
	}
}
