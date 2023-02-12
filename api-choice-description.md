# API choice description

## Restful API

RESTful APIs are a popular choice for building web services because they offer several technical advantages.

One advantage is scalability. RESTful APIs are designed to be layered, which makes them easy to scale. This means you can add more resources or update the API without having to make significant changes to the underlying architecture.

Another advantage is loose coupling. RESTful APIs are loosely coupled, meaning that the client and server can evolve independently of each other. This makes it easier to maintain the API and allows you to add new features or make changes without affecting existing clients.

Interoperability is another advantage of RESTful APIs. They are based on open standards such as HTTP and JSON, which makes them easy to integrate with other systems and services.

Finally, RESTful APIs are stateless, meaning that the server does not store any client context between requests. This makes it easier to cache responses and improves the performance of the API.

The use of open standards such as HTTP and JSON makes it easier to integrate with other systems and services. All this technicals adventages listed above are the reason of why we choose this type of API to expose our Players and Statistics, it enable us to create a exposition of our app ressources very flexible.

REST APIs are useful in the case of Bowling Scorer API, since they allow to retrieve data from bowling players. REST APIs are often used for applications that involve accessing read-only resources, such as retrieving data, such as bowlers' personal information, and it's perfectly suited in the case of a appication that allows you to retrieve bowling player data which is rather statistic (a player has little chance of changing his name). This provides a simple way for the user to access data.

## GRPC API

GRPC is a high-performance, open-source framework for building modern, scalable, and efficient APIs. The technical advantages of GRPC include:

Performance: GRPC uses Protocol Buffers (protobufs) as the default data format, which allows for more compact and faster encoding, compared to text-based data formats such as JSON.

Interoperability: GRPC supports several programming languages, which facilitates its integration with a wide range of systems and services.

Strong typing: GRPC uses protobufs for data encoding, which provides strong typing of data structures and facilitates error detection during development.

Bi-directional streaming: GRPC supports bi-directional streaming, which allows the exchange of multiple messages between client and server in both directions.

GRPC is a good option for applications that require high performance and efficient data encoding, indeed for our bowling application we want the fastest APIs possible, so GRPC is a good option.

GRPC is a good option for our API to retrieve statstical data from bowling players. Indeed, GRPC offers advantages such as increased performance through the use of protobufs for data encoding and its ability to handle large amounts of data efficiently. In addition, the strong data typing of GRPC facilitates error detection during development, which is important to ensure the reliability of bowler statistical data. When you know the amount of statistics that can be generated and sent, GRPC remains a good choice because it is reassuring to know that the risk of error is reduced on large volumes of data. Moreover, the statistics are likely to evolve over time and increase as games are played and it is therefore interesting to use GRPC to deal with these scenarios.

## Websockets (Not implemented)

We could also have used web sockets for the realization of this project, and it is even what we had planned to do initially. They would have been perfectly suited for the part about the bowling games. Indeed, we would have liked to set up APIs allowing various customers to quickly consult the scores of bowling games and their evolution over time.  

To do this, Web sockets would have been perfectly suited since we wanted to provide real-time score updates to your users. WebSockets enable real-time two-way communication between client and server, which means the server can send updates to the client without requiring a specific request from the client. This can be particularly useful for real-time applications such as online games, where users need to know real-time score updates. In other words for bowling it would have been perfect for score management, to be notified as soon as the pins fell, it would have been real time!

Unfortunately we didn't have time to implement them but it would have been a very good technical choice to use web sockets for such a need.

## Conclusion

In conclusion, both REST and GRPC have their own advantages, and choosing between the two will depend on the specific requirements and goals of the project. By using both REST and GRPC APIs in our bowling app, we can take advantage of the strengths of each, providing a high-performance, flexible, and scalable solution for managing players and statistics in the Bowling game. It could also have been interesting to use web sockets for game management.