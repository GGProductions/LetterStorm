using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using GGProductions.LetterStorm.Data;
using UnityEngine;

namespace GGProductions.LetterStorm.Utilities
{
	public static class GameStateUtilities
	{
		#region Load State ----------------------------------------------------
		/// <summary>
		/// Load all player data from persistent storage
		/// </summary>
		public static PlayerData Load()
		{
			PlayerData loadedData = null;

			// If the save file exists...
			//if (File.Exists("C:/Users/David/AppData/LocalLow/Unity" + "/playerInfo.dat"))
			if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
			{
				// Open up the save file for reading
				//FileStream file = File.Open("C:/Users/David/AppData/LocalLow/Unity" + "/playerInfo.dat", FileMode.Open);
				FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
				
				// Create a binary formatter and use it to deserialize the PlayerData from the save file
				BinaryFormatter bf = new BinaryFormatter();
				loadedData = (PlayerData)bf.Deserialize(file);

				// Close stream to the release lock on file
				file.Close();
			}

			return loadedData;
		}
		#endregion Load State -------------------------------------------------

		#region Save State ----------------------------------------------------
		/// <summary>
		/// Save all player data to persistent storage for future access
		/// </summary>
		public static void Save(PlayerData dataToSave)
		{
			// Open up the save file for writing, creating it if it does not already exist
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			//FileStream file = File.Create("C:/Users/David/AppData/LocalLow/Unity" + "/playerInfo.dat");

			// Create a binary formatter and use it to serialize the PlayerData to the save file
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(file, dataToSave);

			// Close stream to the save file
			file.Close();
		}
		#endregion Save State -------------------------------------------------
	}
}
