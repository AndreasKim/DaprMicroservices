# DaprMicroservices

Sample project to show the usage of Dapr microservices with Grpc and Clean Architecture.
In order to run the project first install dapr (https://docs.dapr.io/getting-started/install-dapr-cli/)


```console
powershell -Command "iwr -useb https://raw.githubusercontent.com/dapr/cli/master/install/install.ps1 | iex"
```

and run "dapr init" (https://docs.dapr.io/getting-started/install-dapr-selfhost/).
In the following you can normally start the project in Visual Studio. The Dapr sidecar is automatically started via Dapr Sidekick.

## Project Structure

![grafik](https://user-images.githubusercontent.com/106377614/209716594-81106af8-4d0f-49e0-89bc-044ad229cdd2.png)
