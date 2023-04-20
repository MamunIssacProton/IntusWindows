# IntusWindows.Sales

# Prerequisites

- ## .Net7 or later
- ## docker

### 99.5% pure .net framework used, no 3rd party library used for this project except healthcheck implementation

## Database Used

1. Postgresql (for faster write operation)

## How to run / setup this project on your local environment

1. clone this repository by following this command

   `git clone https://github.com/MamunIssacProton/IntusWindows.git`
   using your terminal/command prompt

2. navigate to the project directory. you can use command like `cd IntusWindows`

3. run docker-compose file to pulling required postgresql image from dockerhub. you can use following command

`docker compose -f "docker-compose.yml" up -d --build `

or use visual studio code docker plugin to do docker-compose-up operation

4. Open the Solution (sln file) in Visual Studio or your IDE

5. Select the `IntusWindows.Sales.Order.Infrastructure` project to do the ### Databse migrations

   you can also navigate to the project by usng

   `cd IntusWindows.Sales.Order.Infrastructure`

   from previous working directory

6. do migration by following this command

   `dotnet ef migrations add init`

   you can use your own migration name insted of init

7. apply pending migrations to database by following this command

   `dotent ef database update`

8. make sure to create multiple project startup from Visual Studio or your IDE, select `IntusWindows.Sales.Order.Api`
   and
   `IntusWindows.Sales.Order.Web.Blazor.Hosted` app as the startup projects
9. now run the application with the custom multiple project startup configuration.

### There is no Database seeding technique implemented on project, so in order to test all feature, please do consider by creating

`State`

` Dimension`

`Element`

`Window`

`Order`

you will get a context menu in `Order` page by selecting order of your choice. the <mark>context menu</mark> has a bug where its don't get disappeared left mouse click [it may be the result of prevent deault browser options on mouse right button click]

## As it is using SignalR, it'll get's auto updates from any changes happens in any operations.

you can try to open multiple window in single/multiple browser to test the integrations.

# Constrains on creating

` MaxWindowHeight=2200`

`MinWindowHeight=1850`

`MaxWindowWidth=1500`

`MinWindowWidth=600`

`MaxDoorHeight=2200`

`MinDoorHeight=1850`

`MaxDoorWidth=1400`

`MinDoorWidth=1200`

you can find this contrains with validation logics on
`IntusWindows.Sales.Order.Domain.Utils` folder

## Validation Used on

1. Application Level (Blazor Wasm- client app) project on performing any `write`/`update` operation.

2. Api level `(IntusWindows.Sales.Order.Api)` project to validate the incoming `request`/`command`to perform operations.

3. Domain level `IntusWindows.Sales.Order.Domain` project to do the final `data validation check`

## Used

1. ### .NET Core Hosted Blazor WebAssembly for the front-end Application in .net 7

2. ### `AssemblyInternalVisibleTo` to do another level of abstraction for database project

   you can find it on `IntusWindows.Sales.Order.Infrastructure` project repositories folder

3. ### made Custom `Realtime Progress tracker` for ding Http get Operation

4. ### used reusable pure blazor componet with `IAsyncDisposable`

5. ### used `Extension Methods` for both `razor` component css as well as backend projects

6. ### implemented `Optimized Queries` using LINQ

### `Area of improvements`

1. #### implement realtime progressbar on `http delete, post` methods

2. #### implement the `external api` healthchecks by using background service/ hosted service in decoupled way.

3. ### `showing validation errors in a list`

### The SwaggerUI has disabled on `IntusWindows.Sales.Order.Api` project. you can enable it by just removing the comments on line `27,28` in `program.cs` file
