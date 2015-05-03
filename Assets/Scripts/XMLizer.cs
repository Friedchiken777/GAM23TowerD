using UnityEngine; 
using System.Collections; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 

public class XMLizer<T>
{ 
	static string _data;
	static T data;

	public XMLizer(){}

	public static string CreateXMLGeneric(T o, string path)
	{
		_data = SerializeObject(o); 
		// This is the final resulting XML from the serialization process 
		CreateXML(path); 
		//Debug.Log(_data); 
		return string.Empty;

	}

	public static T ReadXMLGeneric(string path)
	{
		LoadXML(path); 
		if(_data.ToString() != "") 
		{ 
			// notice how I use a reference to type (UserData) here, you need this 
			// so that the returned object is converted into the correct type 
			data = (T)DeserializeObject(_data); 
			// set the players position to the data we loaded 
			//VPosition=new Vector3(myData._iUser.x,myData._iUser.y,myData._iUser.z);              
			// just a way to show that we loaded in ok 
			//Debug.Log(myData._iUser.name); 
		} 
		return data;
	}
	
	/* The following metods came from the referenced URL */ 
	static string UTF8ByteArrayToString(byte[] characters) 
	{      
		UTF8Encoding encoding = new UTF8Encoding(); 
		string constructedString = encoding.GetString(characters); 
		return (constructedString); 
	} 
	
	static byte[] StringToUTF8ByteArray(string pXmlString) 
	{ 
		UTF8Encoding encoding = new UTF8Encoding(); 
		byte[] byteArray = encoding.GetBytes(pXmlString); 
		return byteArray; 
	} 
	
	// Here we serialize our UserData object of myData 
	static string SerializeObject(T pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(T)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString; 
	} 
	
	// Here we deserialize it back into its original form 
	static T DeserializeObject(string pXmlizedString) 
	{ 
		XmlSerializer xs = new XmlSerializer(typeof(T)); 
		MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		return (T)xs.Deserialize(memoryStream); 
	} 
	
	// Finally our save and load methods for the file itself 
	static void CreateXML(string path) 
	{ 
		StreamWriter writer; 
		FileInfo t = new FileInfo(path); 
		if(!t.Exists) 
		{ 
			writer = t.CreateText(); 
		} 
		else 
		{ 
			t.Delete(); 
			writer = t.CreateText(); 
		} 
		writer.Write(_data); 
		writer.Close(); 
		Debug.Log("File written."); 
	} 
	
	static void LoadXML(string path) 
	{ 
		StreamReader r = File.OpenText(path); 
		string _info = r.ReadToEnd(); 
		r.Close(); 
		_data=_info; 
		//Debug.Log("File Read"); 
	} 
} 