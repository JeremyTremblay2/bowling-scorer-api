[![Build Status](https://codefirst.iut.uca.fr/api/badges/jeremy.tremblay/bowling-scorer-api/status.svg)](https://codefirst.iut.uca.fr/jeremy.tremblay/bowling-scorer-api)
[![Bugs](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=bowling-scorer-api&metric=bugs&token=249c19b1a829285d93c10d5a8ea13706901d6f71)](https://codefirst.iut.uca.fr/sonar/dashboard?id=bowling-scorer-api-token)
[![Code Smells](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=bowling-scorer-api&metric=code_smells&token=249c19b1a829285d93c10d5a8ea13706901d6f71)](https://codefirst.iut.uca.fr/sonar/dashboard?id=bowling-scorer-api-token)
[![Coverage](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=bowling-scorer-api&metric=coverage&token=249c19b1a829285d93c10d5a8ea13706901d6f71)](https://codefirst.iut.uca.fr/sonar/dashboard?id=bowling-scorer-api)
[![Duplicated Lines (%)](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=bowling-scorer-api&metric=duplicated_lines_density&token=249c19b1a829285d93c10d5a8ea13706901d6f71)](https://codefirst.iut.uca.fr/sonar/dashboard?id=bowling-scorer-api)
[![Lines of Code](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=bowling-scorer-api&metric=ncloc&token=249c19b1a829285d93c10d5a8ea13706901d6f71)](https://codefirst.iut.uca.fr/sonar/dashboard?id=bowling-scorer-api)
[![Maintainability Rating](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=bowling-scorer-api&metric=sqale_rating&token=249c19b1a829285d93c10d5a8ea13706901d6f71)](https://codefirst.iut.uca.fr/sonar/dashboard?id=bowling-scorer-api)
[![Quality Gate Status](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=bowling-scorer-api&metric=alert_status&token=249c19b1a829285d93c10d5a8ea13706901d6f71)](https://codefirst.iut.uca.fr/sonar/dashboard?id=bowling-scorer-api)
[![Reliability Rating](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=bowling-scorer-api&metric=reliability_rating&token=249c19b1a829285d93c10d5a8ea13706901d6f71)](https://codefirst.iut.uca.fr/sonar/dashboard?id=bowling-scorer-api)
[![Security Hotspots](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=bowling-scorer-api&metric=security_hotspots&token=249c19b1a829285d93c10d5a8ea13706901d6f71)](https://codefirst.iut.uca.fr/sonar/dashboard?id=bowling-scorer-api)
[![Security Rating](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=bowling-scorer-api&metric=security_rating&token=249c19b1a829285d93c10d5a8ea13706901d6f71)](https://codefirst.iut.uca.fr/sonar/dashboard?id=bowling-scorer-api)
[![Technical Debt](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=bowling-scorer-api&metric=sqale_index&token=249c19b1a829285d93c10d5a8ea13706901d6f71)](https://codefirst.iut.uca.fr/sonar/dashboard?id=bowling-scorer-api)
[![Vulnerabilities](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=bowling-scorer-api&metric=vulnerabilities&token=249c19b1a829285d93c10d5a8ea13706901d6f71)](https://codefirst.iut.uca.fr/sonar/dashboard?id=bowling-scorer-api)

<h1 align="center">🎳 Bowling Scorer API 🎳</h1>

The goal of this project is to develop a functionnal API to manage bowling scores, players, statistics and games.

<p align="center">
    <img src="./doc/images/bowling.png" height="400"/>
</p>

This project will use and cover a Restful API for the player part, and use some websockets to ensure real time exchanges with the clients for the bowling part (games, scores, etc.).

## ✔️ Features

These features will be available in the future, when the API will be completely finished, so stay tuned to be notified when there will be available !

### 🕹️ Restful API
- [✅] Request player's data (name, url of his profile picture, ID...)
- [✅] Get the statistics of the players (best score, number of games (won, loose...), scores more generally).
- [✅] Create players and change their names and profile pictures, and also their statistics.

### 🎙️ GRPC API
- [✅] Get information about bowling players
- [✅] Get informations about the scores
- [✅] CRUD Methods on players
- [✅] CRUD Methods on statistics

## 🧱 Structure of the project

To realize such a work, we have structured this project following a certain architecture, here is it [Architecture Description](doc/description-architecture.md)

## 🖥️ Langages and technologies used

- C# ([API reference](https://learn.microsoft.com/en-US/dotnet/csharp/))
- EntityFramework ([API reference](https://learn.microsoft.com/en-US/ef/))
- ASP .NET & API development ([API reference](https://learn.microsoft.com/en-US/aspnet/core/))
- GRPC ([API reference](https://learn.microsoft.com/fr-fr/aspnet/core/grpc/basics?view=aspnetcore-7.0))

## 🧵 Prerequisities

- [Visual Studio](https://visualstudio.microsoft.com/en/)

## ⚙️ How to run the app ?

* Start Visual Studio.
* Open the solution in `src/` named `BowlingScorerAPI`.
---
### 🔖 If you want to run the RestFul API

* Open a terminal or a PowerShell instance and place you in the RestfulAPI project:
```ps
cd RestfulAPI
```
* Create the database by using these commands: 

> If **dotnet ef** is not installed on your computer :
>```ps
>dotnet tool install dotnet-ef
>```
>If you are on one of the IUT's computer :
>```ps
>dotnet new tool-manifest
>dotnet tool install dotnet-ef
>```

* Generate **migrations** and **database**:
```ps
dotnet ef migrations add bowlingMigration --project ../Entities --context BowlingDbContext
dotnet ef database update bowlingMigration --project ../Entities --context BowlingDbContext
```
* You can't use directly the API, you need to start the Ocelot gateway to access it
    * Right click on solution
    * Select "Properties.."
    * Select "Starting Projects"
    * Check "Multiple Starting Projects"
        * Select Start on "RestfulAPI" and "ApiGateway"
* Start the project.
* You maybe would to have a graphical interface to explore the requests in the API. To do this, you can use Postman and import the solution that is in the "postman" folder at the root of the project
---
### 🔖 If you want to run the GRPC API
* Open a terminal or a PowerShell instance and place you in the RestfulAPI project:
```ps
cd BowlingGrpcServer
```
* Create the database by using these commands: 

> If **dotnet ef** is not installed on your computer :
>```ps
>dotnet tool install dotnet-ef
>```
>If you are on one of the IUT's computer :
>```ps
>dotnet new tool-manifest
>dotnet tool install dotnet-ef
>```

* Generate **migrations** and **database**:
```ps
dotnet ef migrations add bowlingMigration --project ../Entities --context BowlingDbContext
dotnet ef database update bowlingMigration --project ../Entities --context BowlingDbContext
```
* You can't use directly the GRPC API
    * Right click on solution
    * Select "Properties.."
    * Select "Starting Projects"
    * Check "Multiple Starting Projects"
        * Select Start on "BowlingGrpcServer", "BowlingGrpcClient" and "StatisticsGrpcClient"
* Start the project.
---

## 📌 Documentation & developper guidance

API Choice description : [here](doc/api-choice-description.md)</br>
API Gateway description : [here](doc/api-gateway-description.md)</br>
Architecture description : [here](doc/description-architecture.md)  

> Also, look at the `doc` folder at the root of this repository!

---
## 👤 Authors

**Jérémy TREMBLAY**

* Github: [@JeremyTremblay2](https://github.com/JeremyTremblay2)
* LinkedIn: [@Jérémy Tremblay](https://fr.linkedin.com/in/j%C3%A9r%C3%A9my-tremblay2)

**Mickaël DUBOIS (omega2028)**

* GitLab: [@omega2028](https://gitlab.com/omega2028)
* LinkedIn: [@Mickaël Dubois](https://www.linkedin.com/in/micka%C3%ABl-dubois-174827212/)

## 📝 License

We want to leave the possibility to other people to work on this project, to improve it and to make it grow, that's why we decided to place it under MIT license.

> Read more about the [MIT licence](https://opensource.org/licenses/MIT).
