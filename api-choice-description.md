# API choice description

## Restful API

RESTful APIs have several technical advantages that make them a popular choice for building web services:

* Layered: RESTful APIs are easy to scale because they don't rely on any specific technology or protocol. This means you can easily add more resources or update the API without having to make significant changes to the underlying architecture.

* Loose coupling: RESTful APIs are loosely coupled, meaning that the client and server can evolve independently of each other. This makes it easier to maintain the API and allows you to add new features or make changes without affecting existing clients.

* Interoperability: RESTful APIs are based on open standards such as HTTP and JSON, which makes them easy to integrate with other systems and services.

* Statelessness: RESTful APIs are stateless, meaning that the server does not store any client context between requests. This makes it easier to cache responses and improves the performance of the API.

The use of open standards such as HTTP and JSON makes it easier to integrate with other systems and services. All this technicals adventages listed above are the reason of why we choose this type of API to expose our Players and Statistics, it enable us to create a exposition of our app ressources very flexible.

## GRPC API

GRPC is a high-performance, open-source framework for building scalable, modern, and efficient APIs. Some of the technical advantages of gRPC are:

* Performance: GRPC uses Protocol Buffers (protobufs) as the default data format, which provides a compact and efficient encoding that can result in faster serialization and deserialization of data compared to traditional text-based formats like JSON.

* Interoperability: GRPC supports multiple programming languages, making it easy to integrate with a wide range of systems and services.

* Strong typing: GRPC uses protobufs for data encoding, which provides strong typing for data structures and makes it easier to catch errors during development.

* Bi-directional streaming: GRPC supports bi-directional streaming, which allows for the exchange of multiple messages between the client and server in both directions.

In summary, GRPC provides several technical advantages including high performance, interoperability, strong typing, and bi-directional streaming, making it a good choice for applications where performance and efficient data encoding are important considerations, for our bowling app, we want to create the fastest APIs as possible, so GRPC is a good choice.