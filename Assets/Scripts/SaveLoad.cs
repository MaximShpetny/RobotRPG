using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad 
{

	private static string path = Application.persistentDataPath + "/gamesave.skillbox";
	private static BinaryFormatter formatter = new BinaryFormatter();

	public static void SaveGame(Character character)
	{
		
		FileStream fs = new FileStream (path, FileMode.Create);

		SaveData data = new SaveData(character);

		formatter.Serialize(fs, data);

		fs.Close();

	}

	public static SaveData LoadGame()
	{
		if(File.Exists(path)) {
			FileStream fs = new FileStream(path, FileMode.Open);

			SaveData data = formatter.Deserialize(fs) as SaveData;

			fs.Close();

			return data;
		} 
		else 
		{
			return null;
		}
		
	}


}
