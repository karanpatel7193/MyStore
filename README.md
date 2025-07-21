# MyStore - ECommerce Application

## Overview
MyStore is a ECommerce web application with a .NET core backend and a Angular frontend. 

## Prerequisites
- Angular 18+
- Node.js 18+, npm
- .NET Framewo 6.0 or later
- MongoDB
- MS SQL Server

## Setup and Installation

### Environment Configuration

#### Backend Setup

1. Open the `appsettings.json` file in the root of the API project and update the `ConnectionString` to match your database configuration. For example, if you are using SQL Server, it should look like this:
   ```bash
   "ConnectionString": "Data Source=Server name;Initial Catalog=DB Name;User Id=developer;Password=*****;MultipleActiveResultSets=True;Connection Timeout=300;",
   ```
2. Update the MongoDB connection string in the `appsettings.json` file:
   ```bash
    "DefaultMongoDB": {
      "ConnectionString": "mongodb://localhost:27017/",
      "DatabaseName": "E-Commerce",
      "ProviderName": "MongoDB.Driver"
    }
   ```

#### Frontend Setup
1. Navigate to the frontend Web and AdminWeb directory and install dependencies
   ```bash
   npm install
   ```

3. Start the admin and client website server:
   ```bash
   ng s -o
   ```
   The frontend development server will be available.
