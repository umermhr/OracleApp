# Oracle Application

This is a .NET 6 application designed to connect to an Oracle Database (Express Edition), read files from a specified directory, and store the data into an Oracle table. The solution consists of multiple projects, each serving a specific purpose.

## Project Structure

### 1. App.Oracle.Core.Shared
A shared library containing models and utility functions, such as cryptography, used across the application.

### 2. App.Oracle.Core.Web.API
A Web API that connects to the Oracle database and provides various functionalities to interact with the stored data.

### 3. App.Oracle.Core.Web.MVC
A front-end application built with Bootstrap 5 to display files and their contents in a user-friendly interface.

### 4. App.Oracle.Core.Worker.Service
A worker service that runs automatically every minute, calling the Web API to check a designated directory for new files.

## Setting Up the Oracle Database

Follow these steps to set up the Oracle Database required for this application:

### Step 1: Install Oracle Database
- If you do not already have access to an Oracle Database, download and install the latest version of **Oracle Database Express Edition (XE)**.
- Install **Oracle Developer Tools for Visual Studio** or **Oracle SQL Developer** for database management.

### Step 2: Create a Database User
Execute the following SQL script to create a user for the application:

```sql
CREATE USER ORAAPPUSER IDENTIFIED BY ORA_APP_2022
   DEFAULT TABLESPACE USERS QUOTA UNLIMITED ON USERS;

GRANT CREATE SESSION, CREATE VIEW, CREATE SEQUENCE,
   CREATE PROCEDURE, CREATE TABLE, CREATE TRIGGER,
   CREATE TYPE, CREATE MATERIALIZED VIEW TO ORAAPPUSER;
```

### Step 3: Create Tables and Stored Procedures
Execute the following SQL script to create the necessary database tables and stored procedures:

```sql
CREATE TABLE FILE_MASTER (
  id NUMBER GENERATED ALWAYS AS IDENTITY,
  FILE_NAME VARCHAR2(500),
  FILE_CREATION_TS TIMESTAMP,
  RECORD_CREATION_TS TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  RECORD_CREATED_BY VARCHAR2(400),
  PRIMARY KEY(id)
);

CREATE OR REPLACE PROCEDURE files_get_all
AS
  c1 SYS_REFCURSOR;  
BEGIN
  OPEN c1 FOR
  SELECT * FROM FILE_MASTER;
  DBMS_SQL.RETURN_RESULT(c1);
END;

CREATE OR REPLACE PROCEDURE insert_file_master(
    v_filename FILE_MASTER.FILE_NAME%TYPE,
    v_file_creation FILE_MASTER.FILE_CREATION_TS%TYPE,
    v_record_by FILE_MASTER.RECORD_CREATED_BY%TYPE,
    v_file_id OUT FILE_MASTER.id%TYPE)
AS
BEGIN
  INSERT INTO FILE_MASTER (FILE_NAME, FILE_CREATION_TS, RECORD_CREATED_BY)
  VALUES(v_filename, v_file_creation, v_record_by)
  RETURNING id INTO v_file_id;
END;

CREATE TABLE FILE_CONTENT (
  id NUMBER GENERATED ALWAYS AS IDENTITY,
  FILE_ID NUMERIC(10),
  LINE_NO NUMERIC(10),
  LINE_CONTENT VARCHAR2(400),
  RECORD_CREATION_TS TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  RECORD_CREATED_BY VARCHAR2(400),
  CONSTRAINT file_content_pk PRIMARY KEY(id),
  CONSTRAINT fk_file_master
    FOREIGN KEY (FILE_ID)
    REFERENCES FILE_MASTER(id)
);

CREATE OR REPLACE PROCEDURE get_file_content_by_file_id(
    v_file_id FILE_CONTENT.FILE_ID%TYPE)
AS
  c1 SYS_REFCURSOR;  
BEGIN
  OPEN c1 FOR
  SELECT * FROM FILE_CONTENT
  WHERE FILE_ID = v_file_id;
  DBMS_SQL.RETURN_RESULT(c1);
END;

CREATE OR REPLACE PROCEDURE insert_file_content(
    v_file_id FILE_CONTENT.FILE_ID%TYPE,
    v_line_no FILE_CONTENT.LINE_NO%TYPE,
    v_line_content FILE_CONTENT.LINE_CONTENT%TYPE,
    v_record_by FILE_CONTENT.RECORD_CREATED_BY%TYPE)
AS
BEGIN
  INSERT INTO FILE_CONTENT (FILE_ID, LINE_NO, LINE_CONTENT, RECORD_CREATED_BY)
  VALUES(v_file_id, v_line_no, v_line_content, v_record_by);
END;
```

### Step 4: Configure Database Connection
Update the **appsettings.json** file in **App.Oracle.Core.Web.API** to set the correct connection string for Oracle connectivity.

## Application Configuration

### Web API Configuration
Ensure the Web API URL is correctly configured in **appsettings.json**:
- **For App.Oracle.Core.Web.MVC**: Located under the `WebService` section.
- **For App.Oracle.Core.Worker.Service**: Located under the `WebService` section.

### Web API Basic Authentication Credentials
Use the following credentials to authenticate API requests:
- **Username**: `api-local-user`
- **Password**: `Api@2022`

## Summary
This application facilitates seamless interaction with an Oracle database, automating file processing and providing a structured API and UI for managing data. Follow the setup instructions carefully to ensure smooth deployment and functionality.

