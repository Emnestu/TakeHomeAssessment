> ⚠️ This branch is intended for local development and testing only.
> It adds mock data generation that should not be merged to `main`.

# Contact Search Web App

This repository contains a simple proof-of-concept web application built as part of a coding challenge. The task was to create a lightweight and user-friendly solution to help users find key contacts at office locations.

---

## ✨ Project Overview

**Challenge description:**

> A customer needs to quickly and easily find key contacts at office locations. Your task is to build a simple web application that enables users to search for office locations and view relevant contact details. The application should offer a clean, user-friendly interface, a set of straightforward APIs, and leverage the provided data store and sample data.

This solution is composed of:

* A **.NET 8 Web API backend** with an in-memory database
* A **React frontend** with a simple interface for searching and filtering contacts

---

## 🚀 Getting Started

### Prerequisites

* [.NET 8.0 SDK & Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [Node.js (with npm)](https://nodejs.org/) (for frontend)

### Running the App

1. **Start the backend**

   * Open the solution in your IDE (e.g. Rider or Visual Studio)
   * Use the `HTTP` run configuration to launch the backend at `http://localhost:5272`

2. **Start the frontend**

   * Navigate to the `contacts-ui` folder:

     ```bash
     cd contacts-ui
     npm install
     npm start
     ```
   * This will start the frontend at `http://localhost:3000`, which communicates with the backend at port `5272`

---

## 🔄 Features

* Search for contacts by **name or email**
* Filter contacts by **office location**
* Paginate through results
* Simple, modern UI using plain CSS

---

## 📂 Project Structure

* `ContactsSystem.API/` - ASP.NET Core Web API (entry point and controllers)
* `ContactSystem.Application/` - Application layer (business logic, interfaces)
* `ContactSystem.Infrastructure/` - Infrastructure layer (data access, EF Core)
* `contacts-ui/` - React frontend (plain JavaScript + CSS)

---

### ✅ Key Design Decisions & Tradeoffs

1. **The backend APIs are tightly coupled to the frontend**, simplifying development and aligning with the task, which emphasized providing read access over full CRUD. It's assumed that the company already has internal tools to manage this data.

2. **No mapping layer between EF entities and DTOs** was used to reduce boilerplate and keep the codebase concise for this proof of concept.

3. **Searching without a name is allowed** – for example, to view all users in an office – based on the interpretation of user intent: the user may not always know the name of the person they’re looking for.

4. **The interface is optimized for 1–30 offices and 10–500+ employees**. For larger datasets, UI enhancements (e.g. jump-to-page) would be necessary, but were omitted here in favor of **YAGNI** (You Aren’t Gonna Need It).

5. **Frontend error handling is absent**; this was a conscious tradeoff to deliver faster, given the proof-of-concept scope and time constraints.

6. **The frontend was developed with the assistance of an LLM** to accelerate areas I'm less familiar with (i.e. React), while retaining full understanding and control of the resulting code.

---

## 💼 API Endpoints

* `GET /api/v1/Contacts/GetContacts?searchTerm=alejandro&page=1&pageSize=10`
* `GET /api/v1/Offices/GetOfficesWithContacts`

These are consumed by the frontend to display contact listings and populate the office filter dropdown.

---

## 🙋‍ Notes

This project prioritises working software over completeness. It's a focused solution based on my interpretation of the outlined use case.

Thanks for reviewing!