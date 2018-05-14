# Practice Technology Stack


### Task list

Technology stack todo list #1 
These tasks are largely Googleable

Phase 1
1. Create a .NET Framework Console App
1. Import NuGet package Newtonsoft's JSON.NET
1. Serialize a POCO to JSON, restart app, deserialize (it's a realistic usage flow to simply restart the app)

Phase 2
1. Import package RestSharp
1. Make a REST call to an endpoint and deserialize the return with: [package name: Newtonsoft / library name: JSON.NET ] 
1. Do same step as above but not as class but with an anonymous type (deserialize) (Probably use `populateObject()` method in Newtonsoft) 
	* Thus far, that covers how to use NuGet, Newtonsoft, and RestSharp, all important fundamentals.

Phase 3
1. Install NuGet pack Google's protobuf: Google.protobuf (not John's or various other flavours existing out there)...
1. Write a proto file that defines a message .. install NuGet pacvkage Google.protobuf.tools ..
1. Use the protoc tool (proto compiler) to .... Compile the message .proto file I just wrote into C#  
1. Construct a message in C# code and serialize it to file
1. Restart app and deserialize that message file.
1. Serialize, deserialize... like before...

Phase 4
1. Install gRPC NuGet and gRPC.tools NuGet
1. Create a new proto file that defines a service (a collection of methods... A server... Can have multiple services) (eg car rental service... Exposes)
1. Using protoc (and the new gRPC tools) compile the proto file >>> should have C# files that expose ... Eg car rental service base (class)
1. Extend car rental service base and override all the methods.
1. Implement them!

Phase 5
1. Write a second .NET app that is a client. Make them talk to each other.
1. Those generated C# files, you don't want to copy paste into your project, but rather make a class library* that contains them instead. (* shared... to discuss).
1. Get proto files to build automatically with MSBuild.

Phase 6
1. Do it again in Angular.

NOTE: Currently the existing Task list above doesn't seem to conform to the server-client1-client2 topology I've imagined below, and the above seems like it would need some changes to conform to that. But it seems roughly where this project is going.


### Topology of 'technologies' and order of 'actions'
- Visual Studio project 
	- .NET Framework Console App 
		* **SERVER**
		- NuGet
			- Newtonsoft's JSON.NET
		- C# 
			- POCO serialization & deserilaization 
		- NuGet
			- RestSharp
		- C#
			- REST call 
			- deserialize 
		- NuGet 
			- Google's protobuf
		- C#
			- ...

- Visual Studio Project
	- .NET Framework Console App
		* **CLIENT**
		- ...

- Node project 
	- Angular
		* **CLIENT**
		- NPM install ... 
		- ...


### Resources

- [Github API docs](https://developer.github.com/v3/)


### Concepts

- [Serialization](https://en.wikipedia.org/wiki/Serialization).
- [Endpoint](https://stackoverflow.com/questions/9807382/what-is-a-web-service-endpoint) -- for the moment, I take to mean as strictly a URL endpoint, but this can be meant much more broadly.
- [Anonymous Type](https://en.wikipedia.org/wiki/Anonymous_type) 
- [Protocol Buffers](https://en.wikipedia.org/wiki/Protocol_Buffers)
- [Remote procedure call](https://en.wikipedia.org/wiki/Remote_procedure_call)
- Message.

### Gotchas

- small number of only 60 requests hourly allowed from GitHub without authentication.
	- >> different Authentication options liksted under [/#authentication](https://developer.github.com/v3/#authentication) here. >> need an **OAUTH-TOKEN** to access much larger number of 5000 per hour, as mentioned in the [/#rate-limiting](https://developer.github.com/v3/#rate-limiting) rules. 
	- how to get github oauth token ... perhaps unnecessary for now.