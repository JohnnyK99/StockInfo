# StockInfo
! This project has been moved from another repository, unusual timing of the first commits is the side-effect of this action  !  
  
.NET Core SPA application including REST API, SQL database connection and client-side Blazor, as well as login and register functionalities provided by ASP.NET Identity  

## Table of contents
* [Features](#features)
* [Screenshots](#screenshots)
* [Technologies](#technologies)
* [Setup](#setup)

## Features
* Create an account or log in - all functionalities are only available for registered users!
* Select a stock from a list of autocomplete options
* Display general information about the company
* Read recent articles regarding the selected stock
* Analyse data from up to a year ago in one of five chart types
* Save favourite stocks for easier access in the future  

Company information and chart data is provided by [polygon.io](https://polygon.io)

## Screenshots
![Login screen](https://i.ibb.co/gFPT21B/scr2.png)
![Stock view](https://i.ibb.co/qp209gn/scr2.png)
![List of saved stocks](https://i.ibb.co/xX5MTLQ/scr3.png)

## Technologies
Project is created with:
* .NET 5.0
* Entity Framework
* ASP.NET Core
* ASP.NET Identity
* MS SQL Server
* Blazor (client-side)
* [Syncfusion](https://www.syncfusion.com)

## Setup
Execute *update-database* command in the Package Manager Console to create the database scheme.
