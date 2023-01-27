[![Build Status](https://codefirst.iut.uca.fr/api/badges/jeremy.tremblay/bowling-scorer-api/status.svg)](https://codefirst.iut.uca.fr/jeremy.tremblay/bowling-scorer-api)
[![Bugs](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=bowling-scorer-api-token&metric=bugs&token=249c19b1a829285d93c10d5a8ea13706901d6f71)](https://codefirst.iut.uca.fr/sonar/dashboard?id=bowling-scorer-api-token)
[![Code Smells](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=bowling-scorer-api-token&metric=code_smells&token=249c19b1a829285d93c10d5a8ea13706901d6f71)](https://codefirst.iut.uca.fr/sonar/dashboard?id=bowling-scorer-api-token)
[![Coverage](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=bowling-scorer-api-token&metric=coverage&token=249c19b1a829285d93c10d5a8ea13706901d6f71)](https://codefirst.iut.uca.fr/sonar/dashboard?id=bowling-scorer-api-token)

<h1 align="center">🎳 Bowling Scorer API 🎳</h1>

The goal of this project is to develop a functionnal API to manage bowling scores, players, statistics and games.

<p align="center">
    <img src="./doc/images/bowling.png" height="400"/>
</p>

This project will use and cover a Restful API for the player part, and use some websockets to ensure real time exchanges with the clients for the bowling part (games, scores, etc.).

## ✔️ Features

These features will be available in the future, when the API will be completely finished, so stay tuned to be notified when there will be available !

### 🕹️ Restful API
- [ ] Request player's data (name, url of his profile picture, ID...)
- [ ] Get the statistics of the players (medium throw, best score, number of games (won, loose...), throws results and scores more generally).
- [ ] Create players and change their names and profile pictures, and also their statistics.

### 🎙️ Web sockets
- [ ] Get information about bowling games (date of the game, status...)
- [ ] Get informations about the scores (detail for each player present in the game), current cell in the scoretable, current score...
- [ ] Add and edit the scores in the score table, create some games and achieve them.

## 🧱 Structure of the project

To realize such a work, we have structured this project following a certain architecture, here is it:
> ⚠️ Under construction

## 🖥️ Langages and technologies used

- C# ([API reference](https://learn.microsoft.com/en-US/dotnet/csharp/))
- EntityFramework ([API reference](https://learn.microsoft.com/en-US/ef/))
- ASP .NET & API development ([API reference](https://learn.microsoft.com/en-US/aspnet/core/))

## 🧵 Prerequisities

- [Visual Studio](https://visualstudio.microsoft.com/en/)

## ⚙️ How to run the app ?

1. Start Visual Studio.
2. Open the solution in `src/`.
3. Open a terminal or a PowerShell instance and place you in the RestfulAPI project:

```ps

cd RestfulAPI

```

4. Create the database by using these commands: 

```ps

dotnet ef migrations add bowlingMigration --project ../Entities --context BowlingDbContext
dotnet ef database update bowlingMigration --project ../Entities --context BowlingDbContext

```

5. You can now run the application and try it out.

## 📌 Documentation & developper guidance

If you want to participate to this project, be sure to check the documentation before, on the [Wiki](https://codefirst.iut.uca.fr/git/jeremy.tremblay/bowling-scorer-api/wiki) of this project.

Also, look at the `doc` folder at the root of this repository!

## 👤 Authors

**Jérémy TREMBLAY**

* Github: [@JeremyTremblay2](https://github.com/JeremyTremblay2)
* LinkedIn: [@Jérémy Tremblay](https://fr.linkedin.com/in/j%C3%A9r%C3%A9my-tremblay2)

**Mickaël DUBOIS**

* GitLab: [@omega2028](https://gitlab.com/omega2028)
* LinkedIn: [@Mickaël Dubois](https://www.linkedin.com/in/micka%C3%ABl-dubois-174827212/)

## 📝 License

I want to leave the possibility to other people to work on this project, to improve it and to make it grow, that's why we decided to place it under MIT license.

> Read more about the [MIT licence](https://opensource.org/licenses/MIT).