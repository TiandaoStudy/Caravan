//-----------------------------------------------------------------------------------------------------
// <auto-generated>
//     This source code was auto-generated by XsdClassGen.tt.
//     Runtime Version: 4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace FLEX.Web.XmlSettings.AjaxLookup
{
    /// <summary>
    ///   Automatically generated mapping for the AjaxLookupData XML element
    ///   declared in the FLEX.Web.XmlSettings.AjaxLookup namespace.
    /// </summary>
	public partial class AjaxLookupData
	{
        /// <summary>
        ///   Deserializes given string into an instance of <see cref="AjaxLookupData" />.
        /// </summary>
        /// <param name="str">The string from which an instance of <see cref="AjaxLookupData" /> should be deserialized.</param>
        /// <returns>An instance of <see cref="AjaxLookupData" /> deserialized from given string.</returns>
        /// <exception cref="ArgumentException">Given string is null or empty.</exception>
		public static AjaxLookupData DeserializeFrom(string str)
		{
            if (String.IsNullOrEmpty(str)) {
                throw new ArgumentException("Cannot deserialize from a null or empty string.", "str");
            }
			var serializer = new XmlSerializer(typeof(AjaxLookupData));
		    using (var stream = new StringReader(str)) {
			    return serializer.Deserialize(stream) as AjaxLookupData;
		    }
		}        
        
        /// <summary>
        ///   Deserializes given stream into an instance of <see cref="AjaxLookupData" />.
        /// </summary>
        /// <param name="stream">The stream from which an instance of <see cref="AjaxLookupData" /> should be deserialized.</param>
        /// <returns>An instance of <see cref="AjaxLookupData" /> deserialized from given stream.</returns>
        /// <exception cref="ArgumentNullException">Given stream is null.</exception>
		public static AjaxLookupData DeserializeFrom(Stream stream)
		{
            if (stream == null) {
                throw new ArgumentNullException("stream");
            }
			var serializer = new XmlSerializer(typeof(AjaxLookupData));
			return serializer.Deserialize(stream) as AjaxLookupData;
		}

        /// <summary>
        ///   Deserializes given reader into an instance of <see cref="AjaxLookupData" />.
        /// </summary>
        /// <param name="reader">The reader from which an instance of <see cref="AjaxLookupData" /> should be deserialized.</param>
        /// <returns>An instance of <see cref="AjaxLookupData" /> deserialized from given reader.</returns>
        /// <exception cref="ArgumentNullException">Given reader is null.</exception>
		public static AjaxLookupData DeserializeFrom(TextReader reader)
		{            
            if (reader == null) {
                throw new ArgumentNullException("reader");
            }
			var serializer = new XmlSerializer(typeof(AjaxLookupData));
			return serializer.Deserialize(reader) as AjaxLookupData;
		}
        
        /// <summary>
        ///   Deserializes given reader into an instance of <see cref="AjaxLookupData" />.
        /// </summary>
        /// <param name="reader">The reader from which an instance of <see cref="AjaxLookupData" /> should be deserialized.</param>
        /// <returns>An instance of <see cref="AjaxLookupData" /> deserialized from given reader.</returns>
        /// <exception cref="ArgumentNullException">Given reader is null.</exception>
		public static AjaxLookupData DeserializeFrom(XmlReader reader)
		{            
            if (reader == null) {
                throw new ArgumentNullException("reader");
            }
			var serializer = new XmlSerializer(typeof(AjaxLookupData));
			return serializer.Deserialize(reader) as AjaxLookupData;
		}
        
        /// <summary>
        ///   Serializes this instance of <see cref="AjaxLookupData" /> into given string.
        /// </summary>
        /// <param name="str">The string into which the instance of <see cref="AjaxLookupData" /> should be serialized.</param>
		public void SerializeTo(ref string str)
		{
			var serializer = new XmlSerializer(typeof(AjaxLookupData));
            using (var stream = new StringWriter()) {
			    serializer.Serialize(stream, this);
                str = stream.ToString();
            }
		}
        
        /// <summary>
        ///   Serializes this instance of <see cref="AjaxLookupData" /> into given stream.
        /// </summary>
        /// <param name="stream">The stream into which the instance of <see cref="AjaxLookupData" /> should be serialized.</param>
        /// <exception cref="ArgumentNullException">Given stream is null.</exception>
		public void SerializeTo(Stream stream)
		{            
            if (stream == null) {
                throw new ArgumentNullException("stream");
            }
			var serializer = new XmlSerializer(typeof(AjaxLookupData));
			serializer.Serialize(stream, this);
		}
        
        /// <summary>
        ///   Serializes this instance of <see cref="AjaxLookupData" /> into given writer.
        /// </summary>
        /// <param name="writer">The writer into which the instance of <see cref="AjaxLookupData" /> should be serialized.</param>
        /// <exception cref="ArgumentNullException">Given writer is null.</exception>
		public void SerializeTo(TextWriter writer)
		{            
            if (writer == null) {
                throw new ArgumentNullException("writer");
            }
			var serializer = new XmlSerializer(typeof(AjaxLookupData));
			serializer.Serialize(writer, this);
		}
        
        /// <summary>
        ///   Serializes this instance of <see cref="AjaxLookupData" /> into given writer.
        /// </summary>
        /// <param name="writer">The writer into which the instance of <see cref="AjaxLookupData" /> should be serialized.</param>
        /// <exception cref="ArgumentNullException">Given writer is null.</exception>
		public void SerializeTo(XmlWriter writer)
		{            
            if (writer == null) {
                throw new ArgumentNullException("writer");
            }
			var serializer = new XmlSerializer(typeof(AjaxLookupData));
			serializer.Serialize(writer, this);
		}
	}
}
