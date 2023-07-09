# http-from-docker-to-local

This is a demo solution of a Worker process running on Docker, making HTTP requests to an API running on IIS Express on the local machine.

There are 2 APIs:
- Api: older style of Web API with Global.asax and older csproj format
- ApiV2: newer style of Web API with launchSettings.json, Startup and more recent csproj format

Both APIs:
- target .NET Framework 4.8
- create HTTP bindings on port 55173
- binding to both localhost and 127.0.0.1

Typically IIS Express only binds to localhost, and if the same request is made to 127.0.0.1, the response will be a Bad Request - Invalid Hostname (HTTP 400).

To test this, simply run either Api or ApiV2, open a browser tab and go to http://localhost:55173/api/values or http://127.0.0.1:55173/api/values, both should work.

The place where the bindings are being defined is in the following file, under the element `<sites>`: 
- src\\.vs\HttpFromDockerToLocal\config\applicationhost.config
```
            <site name="Api" id="2">
                <application path="/" applicationPool="Clr4IntegratedAppPool">
                    <virtualDirectory path="/" physicalPath="C:\Git\http-from-docker-to-local\src\Api" />
                </application>
                <bindings>
                    <binding protocol="http" bindingInformation="*:55173:localhost" />
                    <binding protocol="http" bindingInformation="*:55173:127.0.0.1" />
                    <binding protocol="https" bindingInformation="*:44364:localhost" />
                </bindings>
            </site>
            <site name="ApiV2" id="3">
                <application path="/" applicationPool="Clr4IntegratedAppPool">
                    <virtualDirectory path="/" physicalPath="C:\Git\http-from-docker-to-local\src\ApiV2" />
                </application>
                <bindings>
                    <binding protocol="http" bindingInformation="*:55173:localhost" />
                    <binding protocol="http" bindingInformation="*:55173:127.0.0.1" />
                </bindings>
            </site>
```

While the Api project will make use of this applicationhost.config file by default, the newer style ApiV2 is referencing this file in launchSettings.json, by executing the iisexpress.exe [/config:path-to-applicationhost.config] [/site:corresponding-site-name-in-applicationhost.config] [/apppool:corresponding-app-pool-in-applicationhost.config]

When running the Worker application, depending on whether if it's on the local machine or in docker, it will set the API_BASE_URL environment variable to http://localhost:55173 or http://host.docker.internal:55173 respectively, as per defined in launchSettings.json file. The host.docker.internal in a docker container will resolve to the local machine, where the API has the binding to 127.0.0.1, solving the Bad Request error.
