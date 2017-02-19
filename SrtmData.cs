#region MIT License
// MIT License
// Copyright (c) 2012 Alpine Chough Software.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.	
#endregion

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Alpinechough.Srtm
{
	/// <summary>
	/// SRTM Data.
	/// </summary>
	/// <exception cref='DirectoryNotFoundException'>
	/// Is thrown when part of a file or directory argument cannot be found.
	/// </exception>
	public class SrtmData
	{
		#region Lifecycle
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Alpinechough.Srtm.SrtmData"/> class.
		/// </summary>
		/// <param name='dataDirectory'>
		/// Data directory.
		/// </param>
		/// <exception cref='DirectoryNotFoundException'>
		/// Is thrown when part of a file or directory argument cannot be found.
		/// </exception>
		public SrtmData (string dataDirectory)
		{
			if (!Directory.Exists (dataDirectory))
				throw new DirectoryNotFoundException (dataDirectory);
			
			DataDirectory = dataDirectory;
			DataCells = new List<SrtmDataCell> ();
		}
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Gets or sets the data directory.
		/// </summary>
		/// <value>
		/// The data directory.
		/// </value>
		private string DataDirectory { get; set; }
		
		/// <summary>
		/// Gets or sets the SRTM data cells.
		/// </summary>
		/// <value>
		/// The SRTM data cells.
		/// </value>
		private List<SrtmDataCell> DataCells { get; set; }
		
		#endregion
		
		#region Public methods
		
		/// <summary>
		/// Unloads all SRTM data cells.
		/// </summary>
		public void Unload ()
		{
			DataCells.Clear ();
		}
		
		/// <summary>
		/// Gets the height.
		/// </summary>
		/// <returns>
		/// The height. Null, if height is not available.
		/// </returns>
		/// <param name='coordinates'>
		/// Coordinates.
		/// </param>
		/// <exception cref='Exception'>
		/// Represents errors that occur during application execution.
		/// </exception>
		public int? GetHeight (IGeographicalCoordinates coordinates)
		{
			int cellLatitude = (int)Math.Floor (Math.Abs (coordinates.Latitude));
			if (coordinates.Latitude < 0)
			{
				cellLatitude *= -1;
				cellLatitude -= 1; // because negative so in bottom tile
			}
			
			int cellLongitude = (int)Math.Floor (Math.Abs (coordinates.Longitude));
			if (coordinates.Longitude < 0)
			{
				cellLongitude *= -1;
				cellLongitude -= 1; // because negative so in left tile
			}

			SrtmDataCell dataCell = DataCells.Where (dc => dc.Latitude == cellLatitude && dc.Longitude == cellLongitude).FirstOrDefault ();
			if (dataCell != null)
				return dataCell.GetHeight (coordinates);
			
			string filename = string.Format ("{0}{1:D2}{2}{3:D3}.hgt",
				cellLatitude < 0 ? "S" : "N",
				Math.Abs (cellLatitude),
				cellLongitude < 0 ? "W" : "E",
				Math.Abs (cellLongitude));
			
			string filePath = Path.Combine (DataDirectory, filename);
			
			if (!File.Exists (filePath))
				throw new Exception ("SRTM data cell not found: "+filePath);
			
			dataCell = new SrtmDataCell (filePath);
			DataCells.Add (dataCell);
			return dataCell.GetHeight (coordinates);
		}
		
		#endregion
	}
}

