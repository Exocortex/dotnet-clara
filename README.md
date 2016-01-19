# Clara.io api bindings for .Net framework

## Installation 
You can download the source code of whole project, and run dotnet-clara.exe.

Or you can install dotnet-clara in your project, through NuGet.
In Visual Studio, open NuGet Package Manage Console, then run command:
```bash
Install-Package dotnet-clara
```

## Quick start

```bash
$ dotnet-clara set --apiToken <api-token>
$ dotnet-clara set --username <username>
```

## API Overview
After installing dotnet-clara, include dotnet-clara in your project
```c#
using dotnet_clara;
using dotnet_clara.lib;
using dotnet_clara.rescources;
```

Create a `clara` instance with your api token and username, visit https://clara.io/settings/api for your api token:

```c#
Clara clara = new Clara(username, apiToken, host)
```
or you can create a `clara` instance with default configuration
```c#
Config config = new Config();
Clara clara = new Clara(config);
```

The parameters for each resource method can be checked in rescoureces classes in the resoureces folder.
Basically, there are there rescoureces classes `Scenes`, `Jobs` and  `Users`.
You can access the method of rescoureces by 
```c#
clara.rescource.Method(var input);
```
For example:
```c#
clara.jobs.get(jobId);
clara.scenes.Update(sceneId, "newSceneName");
```
For most methods in `Jobs` `User` and `Scenes` rescources, they return a repsonse of [HttpResponseMessage](https://msdn.microsoft.com/en-us/library/system.net.http.httpresponsemessage(v=vs.118).aspx) type.

For `Render`  and `Export` of `Scene`, the return data are [Stream](https://msdn.microsoft.com/en-us/library/system.io.stream(v=vs.110).aspx) type. You can save the data to a file.

Please check how to handle the return data in the source code.
## Available resources and methods

  * scenes:library <query>                                 List public scenes
  * scenes:list <query>                                    List your scenes
  * scenes:create <query>                                  Create a new scene
  * scenes:update <sceneId> <query>                        Update a scene
  * scenes:get <sceneId>                                   Get scene data
  * scenes:delete <sceneId>                                Delete a scene
  * scenes:clone <sceneId>                                 Clone a scene
  * scenes:import <sceneId> <fileList>                     Import a file into the scene
  * scenes:export <sceneId> <extension>                    Export a scene
  * scenes:render <sceneId> <query> <options> <filePath>   Render an image
  * scenes:command [options] <sceneId> <plugin> <command>  Run a command
  * jobs:get <jobId>                                       Get job data
  * user:get <username>                                    Get User Profile
  * user:update <username> <query>                         Update user profile
  * user:listScenes <username> <query>                     List user's scenes
  * user:listJobs <username> <query>                       List user's jobs
  * set:[option] <value>                                   Set a configuration value to $HOME/.clara.json
  * get:[option]                                           Return the current configuration for [option]

## Configuration

There are several ways to set up the configuration data command line or api:

1. Pass directly (through function call, or command line).

```bash
dotnet-clara set --apiToken <apiToken> --username <username> 
```
2. Or with api:

```c#
Clara clara = new Clara(username, apiToken, host)
```

3. Configuration file in current working directory

 A json file named `.clara.json` can hold configuration data:
```json
{
  "apiToken": "api-token-here",
  "username": "your-username",
  "host":"clara.io",
  "basePath":"/api"
}
```
Configuration file in $HOME. If the configuration file `.clara.json` exists in $HOME, it will be used.

## Command line overview

Congifguration commands are available from the command line runner.
```bash
$ dotnet-clara help
$ dotnet-clara scenes --get 7a5f12ca-6773-4409-9696-7a65d22520e0
$ dotnet-clara set --apiToken <apiToken> --username <username>
```
You can use the clara command line to quickly set/get your configuration data. It will write
to `$HOME/.clara.json`:

```bash
$ dotnet-clara set --apiToken your-api-token
$ dotnet-clara get --username
```
You can call rescource and method through command line:
```bash
$ dotnet-clara scenes --get sceneId
```
## Available resources and methods
```
-----------------Job-----------------------------
"help --- for help
-----------------Configuration-----------------------------
set --[option] <value> --- set configuration data
get --[option] <value> --- get configuration data
[option] : username, apiToken, host 
-----------------Job-----------------------------
job --[option] <value>
[option] : get 
--get <jobId> --- get job data 
-----------------User----------------------------
user --[option] <value>
[option] : get, update, listScenes, listJobs
--get <username> --- get user profile
--update <username> <profile> --- update user profiel
--listScenes <username> <query> --- list user's scenes
--listJobs <username> <query> --- list user's jobs
-----------------Scene---------------------------
scene --[option] <value> : get one configuration item
[option] : get, update, library, create, delete, clone, export, import, command, render
--get <sceneId> --- get a scene data
--update <sceneId> <sceneName> --- update a scene
--library <query> --- list public scenes
--create --- create a new scene
--delete <sceneId> --- delete a scene
--clone <sceneId> --- clone a scene
--export <sceneId> <extension> <filePath> --- export a scene
--import <sceneId> <fileList> --- import a file into the scene
--command <sceneId> <commandOptions> --- run a command
--render <sceneId> <query> <options> <filePath> --- render an image
```
## Development

Run the tests in Visual Studio.

