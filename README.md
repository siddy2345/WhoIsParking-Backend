# WhoIsParking-Backend 🅿️
ASP.NET Core backend API for the WhoIsParking application

This repository contains the server-side API for the WhoIsParking project. It handles business logic, data storage, and exposes endpoints consumed by the [WhoIsParking frontend](https://github.com/siddy2345/WhoIsParking-Frontend).

## Purpose 💡

WhoIsParking exists to replace outdated, paper-based visitor parking registration systems with a simple digital solution.

This backend provides:

- A centralized place to store parking and visitor data

- APIs for registering, querying, and managing parked vehicles

- A secure and scalable foundation for the frontend application

## Architecture 🏢

ASP.NET Core Web API

RESTful endpoints

Separation of concerns between controllers, services, and data access (CQRS and EF Core)

Domain-Driven-Design with Clean Architecture

Designed to be consumed by the Angular frontend