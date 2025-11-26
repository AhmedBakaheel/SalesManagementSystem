# Sales Management System (SMS) API

## Project Overview

This is a comprehensive backend system developed using **ASP.NET Core Web API** based on the **Clean Architecture** principles. The system is designed to manage core sales operations, including customer relationship management, sales invoicing, inventory tracking, and payment processing.

## Core Features

* **Customer Management:** Track customer data, including credit limits and real-time balances.
* **Sales Invoicing:** Record cash and credit sales invoices with detailed line items.
* **Inventory Control:** Update stock quantities automatically upon sale.
* **Payment Tracking:** Record customer payments and allocate them against outstanding credit invoices.
* **Debt Management:** Automatic calculation and tracking of customer credit balance and adherence to predefined credit limits.

## Architecture

The solution is divided into four main layers for maintainability and testability:

1.  **Domain:** Core entities and business invariants.
2.  **Application:** Business logic, use cases (Commands/Queries), and Repository interfaces (leveraging **MediatR**).
3.  **Infrastructure:** Implementation of data persistence using **Entity Framework Core (SQL Server)**.
4.  **API:** ASP.NET Core controllers and dependency injection setup.
