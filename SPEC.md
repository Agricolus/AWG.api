# FIWARE Agri Weather Gateway

[![FIWARE Banner](https://fiware.github.io/tutorials.Context-Providers/img/fiware.png)](https://www.fiware.org/developers)

### A first FIWARE Domain Application: Agri Weather Gateway

[![FIWARE Core Context Management](https://nexus.lab.fiware.org/repository/raw/public/badges/chapters/core.svg)](https://github.com/FIWARE/catalogue/blob/master/core/README.md)
[![License badge](https://img.shields.io/github/license/FIWARE/context.Orion-LD.svg)](https://opensource.org/licenses/AGPL-3.0)
[![Support badge](https://nexus.lab.fiware.org/repository/raw/public/badges/stackoverflow/fiware.svg)](https://stackoverflow.com/questions/tagged/fiware)
[![NGSI-LD badge](https://img.shields.io/badge/NGSI-LD-red.svg)](https://www.etsi.org/deliver/etsi_gs/CIM/001_099/009/01.02.01_60/gs_CIM009v010201p.pdf)
[![Documentation](https://img.shields.io/readthedocs/fiware-tutorials.svg)](https://fiware-tutorials.rtfd.io)

It is a backend service written in **.NET Core Framework** that register itself as a subscriber into the instance of **Orion-LD** for simplifying historic weather data analisys. 
It uses a **PostgreSQL** server for data persistance and it's secured by an instance of the **Wilma PEP Proxy (Powered by FIWARE)**.

To use the APIs, download and install [Microsoft .NET Core Framework SDK v3.1](https://dotnet.microsoft.com/download).

It was written using:
* [Visual Studio Code](https://code.visualstudio.com)
* [Azure Data Studio](https://docs.microsoft.com/en-us/sql/azure-data-studio)
* [Microsoft .NET Core Framework SDK](https://docs.microsoft.com/en-us/dotnet/core)
* [PostgreSQL](https://www.postgresql.org)
* [FIWARE Data Models](https://github.com/FIWARE/data-models)
* [OpenStreetMap Nominatim](https://nominatim.openstreetmap.org)

To use **Visual Studio Code**, it is highly recommended install these plugins:
* .NET Core Add Reference
* Auto-Using for C#
* C# Extensions
* C# Essentials
* C# for Visual Studio Code (powered by OmniSharp)
* MSBuild project file tools
* VScode-icons
* VScode-nuget-package-manager
* XML Documentation Comments Support for Visual Studio Code

To use **Azure Data Studio**, it is highly recommended install this plugin:
* PostgreSQL extension for Azure Data Studio

Open the Windows Command Line and type this command:

```sh
dotnet tool install --global dotnet-ef
```

The available endpoints are:

* Stations:
   * All stations - **GET: ​/api​/stations**
   * Station by ref id - **GET: ​/api​/stations​/{id}**
   * Nearest station by location - **GET: ​/api​/stations​/nearest**
   * Add new station - **POST: ​/api​/stations**
   * Add new station (LD standard) - **POST: ​/api​/stations-ld**
   * Update a station - **PUT: ​/api​/stations**
   * Update a station (LD standard) - **PUT: ​/api​/stations-ld**
   * Remove a station by ref id - **DELETE: ​/api​/stations​/{id}**

* Measures:
   * Last measure - **GET: ​/api​/measures​/last**
   * Measures grouped daily - **GET: ​/api​/measures​/daily**
   * Measures grouped weekly - **GET: ​/api​/measures​/weekly**
   * Measures grouped monthly - **GET: ​/api​/measures​/monthly**
   * Measures in interval - **GET: ​/api​/measures​/interval**
   * Add new measure - **POST: ​/api​/measures**
   * Add new measure (LD standard) - **POST: ​/api​/measures-ld**
   * Update a measure - **PUT: ​/api​/measures**
   * Update a measure (LD standard) - **PUT: ​/api​/measures-ld**

More technical details are availables through the Swagger page in **/swagger**.

These configuration parameters are in **appsettings.json**:
* **AllowedHosts**: CORS configuration
* **DataBaseType**: database protocol
* **AWGPostgreContext**: PostgreSQL connection string
* **EnableGeocodingForMeasures**: Address geocoding enabler
* **OpenStreetMapURI**: OpenStreetMap Nominatim URI

## Versioning

We use [SemVer](http://semver.org) for versioning. 

For the versions available, see the **tags** on this repository.

## Authors

See the list of **contributors** who participated in this project.

## Contributing

1. Fork this repository
2. Create your feature branch (`git checkout -b feature/fooBar`)
3. Commit your changes (`git commit -am 'Add some fooBar'`)
4. Push to the branch (`git push origin feature/fooBar`)
5. Create a new Pull Request

## License

This project is licensed under the [AGPL License](https://www.gnu.org/licenses/agpl-3.0.en.html).