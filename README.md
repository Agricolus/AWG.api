# FIWARE AgroWeatherGateway

[![FIWARE Banner](https://fiware.github.io/tutorials.Context-Providers/img/fiware.png)](https://www.fiware.org/developers)

### A first FIWARE Domain Application: AgroWeatherGateway

[![FIWARE Core Context Management](https://nexus.lab.fiware.org/repository/raw/public/badges/chapters/core.svg)](https://github.com/FIWARE/catalogue/blob/master/core/README.md)
[![License badge](https://img.shields.io/github/license/FIWARE/context.Orion-LD.svg)](https://opensource.org/licenses/AGPL-3.0)
[![Support badge](https://nexus.lab.fiware.org/repository/raw/public/badges/stackoverflow/fiware.svg)](https://stackoverflow.com/questions/tagged/fiware)
[![NGSI-LD badge](https://img.shields.io/badge/NGSI-LD-red.svg)](https://www.etsi.org/deliver/etsi_gs/CIM/001_099/009/01.02.01_60/gs_CIM009v010201p.pdf)
[![Documentation](https://img.shields.io/readthedocs/fiware-tutorials.svg)](https://fiware-tutorials.rtfd.io)

## Table of Contents

* [Introduction](#introduction)
* [Architecture](#architecture)
* [Functionalities](#functionalities)
* [Built With](#built-with)
* [Installation](#installation)
  * [Infrastructure](#infrastructure)
  * [Services](#backend-services)
  * [Getting Started](#docker)
  * [APIs](#apis)
  * [Web Frontend](#web-frontend)
  * [Developers Tools](#developers-tools)
* [Versioning](#versioning)
* [Authors](#authors)
* [Contributing](#contributing)
* [License](#license)

## Introduction

The main purpose of this project is to provide an idea of a [FIWARE](https://www.fiware.org) Domain Specific Enabler related to the [FIWARE AgriFood Reference Architecture](https://www.fiware.org/community/smart-agrifood). This DSE enables a standardized connection to many the agro-weather sensors in the market relying on [Orion Context Broker](https://fiware-orion.readthedocs.io/en/master) technology and [NGSI-LD](https://fiware-datamodels.readthedocs.io/en/latest/ngsi-ld_faq) standard. 

This product has some major interests: 

* Providing a standardized interface to several producers that can be used by AG platforms and other AG related products
* Providing a standalone platform that can be used to visualize data coming from different sensors
* Providing a product that can be used event by the government (local and central) to connect many different sensors and to provide access to the farmers

In a first market analysis this kind of product is missing and, moreover, is more than welcomed by the public administration and by platform producers to simplify the access to this kind of data. 

This idea starts from a common need expressed by two of these platforms, [Agricolus](https://www.agricolus.com/en) and [365FarmNet](https://www.365farmnet.com/en), that meet in the [IOF2020](https://www.iof2020.eu) ecosystem and recognized as a common benefit to run a joint effort to build this GE. Both the companies recognized FIWARE as the natural architecture to choose for this kind of development. 

This Gateway is both a standalone application and a data gateway for agroweather data: 

* The **standalone application**: is provided to deploy a monitoring and data collection system for government, association of farmers and other organizations that needs to collect, visualize and deliver these data to third parties. With this DSE they will be able to setup an environment capable of implement several communication standards for several hardware producers with a single point of access
* The **data gateway**: each organization will be able to deliver, upon authorization of each entity, these dynamic data to third parties, such as AG platforms and mobile apps, to enable quickly and with a reduced cost new services and functionalities

## Architecture

* **Context Providers**: 
   * Will interact with different producers ecosystems and cloud applications. Each producer may allow the access to a cloud repository or directly to the sensors. The context provider will manage the communication layers, the authentication of the weatherstations, sensors specific configurations and other hardware-related data (such as alarm notifications in case of hardware failure, if provided)
* **APIs Access for External Applications**: 
   * Upon specific authorizations third parties will be enabled to acces real time data form the DSE with given credentials
* **Web Interface**:
  * Sensors management and configuration:
     * CRUD operations on weather stations
     * Weather stations POI management
     * Specific hardware producer configuration of each weather station
  * Weather stations web map:
     * Weather stations are visualized in a web map
  * Weather stations charts and data:
     * Actual and historic data for each weather station can be accessed and visualized in a graphic format
     * Actual and historic data can be downloaded in a CSV format

## Functionalities

* Data harmonization done with NGSI-LD Data Models (updating Observed Data Model)​
* APIs for data access​
* Admin frontend with overall sensors dashboard​
* End user portal that will have access to data​

## Built With

* APIs
   * [Visual Studio Code](https://code.visualstudio.com)
   * [Azure Data Studio](https://docs.microsoft.com/en-us/sql/azure-data-studio)
   * [Microsoft .NET Core Framework](https://docs.microsoft.com/en-us/dotnet/core)
   * [PostgreSQL](https://www.postgresql.org)
   * [FIWARE Data Models](https://github.com/FIWARE/data-models)
* Web Frontend
   * [NPM](https://www.npmjs.com)
   * [Vue.js](https://vuejs.org)
   * [Font Awesome](https://fontawesome.com/)
   * [Weather Icons](https://erikflowers.github.io/weather-icons/)

## Installation

#### Infrastructure

AWG is composed of some backend services, one frontend web application plus a varying number of IoT agents.

#### Services

1. [Docker](https://github.com/ging/fiware-idm)
2. [Keyrock Identity Manager](https://github.com/ging/fiware-idm)
3. [Orion-LD Context Broker](https://github.com/Fiware/context.Orion-LD)
4. [Wilma PEP Proxy](https://github.com/ging/fiware-pep-proxy)
5. [APIs](https://github.com/Agricolus/awg.api)
6. [Web Frontend](https://github.com/Agricolus/awg.frontend)

#### Getting Started

If you want to use [Docker](https://docs.docker.com/install), install it following the official guide.

If you want to use [PostgreSQL](https://www.postgresql.org/download) on premises, install it following the official guide.

To install it through Docker, type:

```sh
sudo docker run --name <my-postgres-instance-> -e POSTGRES_PASSWORD=<my-password> --restart unless-stopped -p 5432:5432 -d postgres
```

The default installation of PostgreSQL open the 5432 port.

If you want to use [MySQL](https://www.mysql.com/downloads/) on premises, install it following the official guide.

To install it through Docker, type:

```sh

```

If you want to use [Orion Context Broker](https://github.com/telefonicaid/fiware-orion) on premises, install it following the official guide.

To install it through Docker, type:
```sh
sudo docker-compose -f orion-compose.yml up -d
```

The default installation of Orion Context Broker open the 1026 port.

##### orion-compose.yml

```yml
version: "3.5"
services:
  mongo:
    restart: always
    image: mongo:3.6
    command: --nojournal
    networks:
      default:
      idmnet:

  orion:
    restart: always
    image: fiware/orion-ld
    command: -dbhost mongo
    ports:
      - "1026:1026"
    networks:
      default:
      idmnet:

networks:
  default:
  idmnet:
    name: idmnet
    internal: true
```

#### APIs

Is a backend service written in **.NET Core Framework** that register itself as a subscriber into the instance of **Orion-LD** for simplifying historic weather data analisys. 
It uses a **PostgreSQL** server for data persistance and it's secured by an instance of the **Wilma PEP Proxy (Powered by FIWARE)**.

To use the APIs, download and install [Microsoft .NET Core Framework SDK v3.1](https://dotnet.microsoft.com/download).

#### Web Frontend

Project setup:
```sh
npm install
```

Compiles and hot-reloads for development:
```sh
npm run serve
```

Compiles and minifies for production:
```sh
npm run build
```

Lints and fixes files:
```sh
npm run lint
```

To customize the configurations, see the [Configuration Reference](https://cli.vuejs.org/config/).

#### Developers Tools

To use Visual Studio Code, it is highly recommended install this plugins:
* .NET Core Add Reference
* Auto-Using for C#
* C# Extensions
* C# Essentials
* C# for Visual Studio Code (powered by OmniSharp)
* MSBuild project file tools
* VScode-icons
* VScode-nuget-package-manager
* XML Documentation Comments Support for Visual Studio Code

To use Azure Data Studio, it is highly recommended install this plugin:
* PostgreSQL extension for Azure Data Studio

Open the Windows Command Line and type this command:

```sh
dotnet tool install --global dotnet-ef
```

## Versioning

We use [SemVer](http://semver.org/) for versioning. 

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