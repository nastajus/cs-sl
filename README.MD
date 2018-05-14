### Practice Technology Stack

Technology stack todo list #1 

Tasks Googleable

1. .net framework console app
1. Import nuget package Newtonsoft.json
1. Serialize a POCO to JSON, restart app, deserialize (realistic usage)
1. Import pack REST Sharp
1. Make a REST call to an endpoint and deserialize the return with pack: Newtonsoft (lib: json.net)
1. Do same step as above but not as class but with an anonymous type (deseri.) (Probably use populate object method in ns) {that's how to use nuget, ns, and restsharp >> important }
1. Install nuget pack Google's Google.protobuff (not John's)...
1. Write a proto file that defines a message .. install nuget pack... Google.protobuff.tools ..
1. Use the protoc tool (proto compiler) to .... Compile the message .proto file I just wrote into c#  
1. Construct a message in c# code and serialize it to file
1. Restart app and deserialize that message file.
1. Serialize, deserialize... (.
1. Install GRPC nugget and grpc.tools nugget
1. Create a new proto file that defines a service (a collection of methods... A server... Can have multiple services) (eg car rental service... Exposes)
1. Using protoc (and the new GRPC tools) compile the proto file >>> should have c# files that expose ... Eg car rental service base (class)
1. Extend car rental service base and override all the methods.
1. Implement them!
1. Write a second. Net app that is a client. Make them talk to each other.
1. Those generated c# files, you don't want to copy paste, make a {shared} class library that contains them
1. Get proto files to build automatically with msbuild.
