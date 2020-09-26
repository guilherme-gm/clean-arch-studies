# Studies on Clean Architecture

This repository reunites stuff that I've studied / tests I made in order to better understand Clean Architecture.

# Usefull Links
- [The Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) on The Clean Code Blog (By Uncle Bob)
> Great overview of the architecture
- [Common Web Application Architectures - Clean Architecture](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture) on Microsoft Docs
- https://github.com/ardalis/cleanarchitecture
- Autofac - DI lib https://autofac.org/ (just to get an idea how the above repo works)
- https://medium.com/slalom-build/clean-architecture-with-java-11-f78bba431041


# Test Case Project

Implement Q&A application, like a simpler version of StackOverflow
1. Users are assumed to exist since the beggining (they are manually inserted into the DB). And they may be:
   1. Normal User
   2. Moderator
2. Users are able to create questions (being the 'Asker' of this Question)
3. Questions may receive answers from any user
4. The Asker is able to pick up an answer as the correct one
5. Users are able to edit their own answers/questions
6. Moderators are able to edit any message, from anyone.
7. Actions in the system must be logged
   1. Question/Answer Creation
   2. Question/Answer Edit
   3. Accepting answer

