SRTM .NET Library
===================

Alpinechough.Srtm is a .NET library (written in C#) for reading the [Shuttle Radar Topography Mission (SRTM)](http://www2.jpl.nasa.gov/srtm/) data files, which may be obtained through this URL: [http://dds.cr.usgs.gov/srtm/](http://dds.cr.usgs.gov/srtm/)

Hello World
-----------

To write your first application, create an empty .NET project in your favorite language in MonoDevelop or Visual Studio and reference the Alpinechough.Srtm library. Download [N47E011.hgt](http://dds.cr.usgs.gov/srtm/version2_1/SRTM3/Eurasia/N47E011.hgt.zip) and [S17W069.hgt](http://dds.cr.usgs.gov/srtm/version2_1/SRTM3/South_America/S17W069.hgt.zip). Store the files in the "SrtmDataFiles" folder.

	using System;
	using Alpinechough.Srtm;
	
	class SrtmDemo {
		static void Main () {
			SrtmData srtmData = new SrtmData("SrtmDataFiles");
			
			int elevationInnsbruck = srtmData.GetHeight(new GeographicalCoordinates (47.267222, 11.392778));
			Console.WriteLine("Elevation of Innsbruck: {0}m", elevationInnsbruck);
			
			int elevationLaPaz = srtmData.GetHeight(new GeographicalCoordinates (-16.5, -68.15));
			Console.WriteLine("Elevation of La Paz: {0}m", elevationLaPaz);
		}
	}

License
-------

[MIT License](http://www.opensource.org/licenses/mit-license.html)

Copyright (c) 2012 Alpine Chough Software.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

Contact
-------

Andreas Windischer (andreas.windischer@alpinechough.com)
