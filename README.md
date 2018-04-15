# TransactionsManagerIdentityServer
OpenID Connect provider.

[![CircleCI](https://circleci.com/gh/guillermocorrea/TransactionsManagerIdentityServer.svg?style=svg)](https://circleci.com/gh/guillermocorrea/TransactionsManagerIdentityServer)

Requirements
============
* [Dotnet core 2](https://www.microsoft.com/net/download/windows)

Run the Application
===================
* In a cmd hit `dotnet restore` and `dotnet run src/TransactionsManagerIdentityServer/TransactionsManagerIdentityServer.csproj` or open the solution in Visual Studio 2017 and run the application from there.
* Open a browser at http://localhost:5000

Dataset
=======
The dataset posted at https://www.kaggle.com/ntnu-testimon/paysim1 was imported into a SQL Server table to be queried and managed.

![Transactions Table](https://raw.githubusercontent.com/guillermocorrea/TransactionsManager/master/Transactions%20Table.png)

Architecture
============
![Architecture](https://raw.githubusercontent.com/guillermocorrea/TransactionsManager/master/components-diagram.jpg)

Contact
=======
Developed by [Guillermo Correa](https://www.linkedin.com/in/luis-guillermo-correa-guti%C3%A9rrez-01620a94/). Feel free to contact me at: guillermo.correa.99@gmail.com