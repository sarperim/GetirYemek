/* INIT.SQL - Simplified Monolith Setup
   - Creates Database
   - Creates Schemas ONLY (No separate users)
*/

USE [master];
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ModularMonolith')
BEGIN
    CREATE DATABASE [ModularMonolith];
END
GO

USE [ModularMonolith];
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Auth')
BEGIN
    EXEC('CREATE SCHEMA [Auth]');
END
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Ordering')
BEGIN
    EXEC('CREATE SCHEMA [Ordering]');
END
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Payment')
BEGIN
    EXEC('CREATE SCHEMA [Payment]');
END
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Catalog')
BEGIN
    EXEC('CREATE SCHEMA [Catalog]');
END
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Contracts')
BEGIN
    EXEC('CREATE SCHEMA [Contracts]');
END
GO

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Basket')
BEGIN
    EXEC('CREATE SCHEMA [Basket]');
END
GO