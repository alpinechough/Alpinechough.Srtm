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
using System.Text;

namespace Alpinechough.Srtm
{
/// <summary>
/// Geographical coordinates.
/// </summary>
	public class GeographicalCoordinates : IGeographicalCoordinates
	{
		#region Lifecycle
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Alpinechough.Srtm.GeographicalCoordinates"/> class.
		/// </summary>
		public GeographicalCoordinates ()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Alpinechough.Srtm.GeographicalCoordinates"/> class.
		/// </summary>
		/// <param name='latitude'>
		/// Latitude.
		/// </param>
		/// <param name='longitude'>
		/// Longitude.
		/// </param>
		public GeographicalCoordinates (double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Alpinechough.Srtm.GeographicalCoordinates"/> class.
		/// </summary>
		/// <param name='latitudeDegrees'>
		/// Latitude Degrees.
		/// </param>
		/// <param name='latitudeMinutes'>
		/// Latitude Minutes.
		/// </param>
		/// <param name='latitudeSeconds'>
		/// Latitude Seconds.
		/// </param>
		/// <param name='latitudeDirection'>
		/// Latitude Direction "N" or "S".
		/// </param>
		/// <param name='longitudeDegrees'>
		/// Longitude Degrees.
		/// </param>
		/// <param name='longitudeMinutes'>
		/// Longitude Minutes.
		/// </param>
		/// <param name='longitudeSeconds'>
		/// Longitude Seconds.
		/// </param>
		/// <param name='longitudeDirection'>
		/// Longitude Direction "W" or "E".
		/// </param>
		public GeographicalCoordinates (
			int latitudeDegrees, int latitudeMinutes, int latitudeSeconds, char latitudeDirection,
			int longitudeDegrees, int longitudeMinutes, int longitudeSeconds, char longitudeDirection)
			: this(
				(latitudeDegrees + latitudeMinutes / 60.0 + latitudeSeconds / 3600.0) * (latitudeDirection.ToString().ToUpper() == "N" ? 1.0 : -1.0),
				(longitudeDegrees + longitudeMinutes / 60.0 + longitudeSeconds / 3600.0) * (longitudeDirection.ToString().ToUpper() == "W" ? 1.0 : -1.0))
		{
			switch (latitudeDirection)
			{
				case 'n':
				case 'N':
				case 's':
				case 'S':
					break;
				default:
					throw new ArgumentException ();
			}

			switch (longitudeDirection)
			{
				case 'w':
				case 'W':
				case 'e':
				case 'E':
					break;
				default:
					throw new ArgumentException ();
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Alpinechough.Srtm.GeographicalCoordinates"/> class.
		/// </summary>
		/// <param name='geographicalCoordinates'>
		/// Geographical coordinates.
		/// </param>
		public GeographicalCoordinates (GeographicalCoordinates geographicalCoordinates)
			: this(geographicalCoordinates.Latitude, geographicalCoordinates.Longitude)
		{
		}
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Gets or sets the latitude.
		/// </summary>
		/// <value>
		/// The latitude.
		/// </value>
		public double Latitude { 
			get { 
				return latitude;
			}
			set {
				if (value < -90 || value > 90)
					throw new ArgumentOutOfRangeException ();
				
				latitude = value;
			}
		}

		private double latitude = 0;
		
		/// <summary>
		/// Gets or sets the longitude.
		/// </summary>
		/// <value>
		/// The longitude.
		/// </value>
		public double Longitude { 
			get {
				return longitude;
			}
			set {
				if (value > 180 || value <= -180)
					throw new ArgumentOutOfRangeException ();
				
				longitude = value;
			}
		}

		private double longitude = 0;
		
		#endregion
		
		#region ToString / Parse
		
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="Alpinechough.Srtm.GeographicalCoordinates"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents the current <see cref="Alpinechough.Srtm.GeographicalCoordinates"/>.
		/// </returns>
		public override string ToString ()
		{
			return string.Format ("{0:0.000000}°, {1:0.000000}°", Latitude, Longitude);
		}
		
		#endregion
	}
}

