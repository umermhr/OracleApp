# Oracle App

A .net 6 application to connect the oracle database (express edition), read files from directoy and save into oracle datatable. There are  projects in this solutions:

#### App.Oracle.Core.Shared

Share library for models and util functions like cryptography.

#### App.Oracle.Core.Web.API

Web API for connect Oracle database and perform other functionalities.

#### App.Oracle.Core.Web.MVC

Front end to display the files and their content using bootstrap 5.

#### App.Oracle.Core.Worker.Service

Worker service to execute WEB API automatically after every 1 minute to check the particular directory.

Now let's setup the Oracle Database:

### Setup Oracle Database

1. If you do not already have access to an Oracle Database, install latest version of Oracle Database Express Edition (XE).
2. Install Oracle Developer Tools For Visual Studio or Oracle SQL Developer tool
3. Connect to Oracle Database & Create the Database User using below script

```sql
CREATE USER ORAAPPUSER IDENTIFIED BY ORA_APP_2022
   DEFAULT TABLESPACE USERS QUOTA UNLIMITED ON USERS;

GRANT CREATE SESSION, CREATE VIEW, CREATE SEQUENCE,
   CREATE PROCEDURE, CREATE TABLE, CREATE TRIGGER,
     CREATE TYPE, CREATE MATERIALIZED VIEW TO ORAAPPUSER;
```
4. Execute Create Tables and Stored Procedure script

```sql


CREATE TABLE FILE_MASTER (
  id NUMBER GENERATED ALWAYS AS IDENTITY,
  FILE_NAME VARCHAR2(500),
  FILE_CREATION_TS TIMESTAMP,
  RECORD_CREATION_TS TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
  RECORD_CREATED_BY VARCHAR2(400),
  PRIMARY KEY(id)
);

create or replace PROCEDURE files_get_all
AS
  c1 SYS_REFCURSOR;  
BEGIN

  open c1 for
  SELECT *
  FROM FILE_MASTER;

  DBMS_SQL.RETURN_RESULT(c1);

END;


create or replace procedure insert_file_master(v_filename FILE_MASTER.FILE_NAME%TYPE,
    v_file_creation FILE_MASTER.FILE_CREATION_TS%TYPE,
    v_record_by FILE_MASTER.RECORD_CREATED_BY%TYPE,
    v_file_id out FILE_MASTER.id%TYPE)
as
begin
	INSERT INTO FILE_MASTER (FILE_NAME, FILE_CREATION_TS, RECORD_CREATED_BY) 
	VALUES(v_filename, v_file_creation, v_record_by)
    returning id into v_file_id;
end;


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

create or replace PROCEDURE get_file_content_by_file_id(v_file_id FILE_CONTENT.FILE_ID%TYPE)
AS
  c1 SYS_REFCURSOR;  
BEGIN

  open c1 for
  SELECT *
  FROM FILE_CONTENT
  WHERE FILE_ID = v_file_id;

  DBMS_SQL.RETURN_RESULT(c1);

END;

create or replace procedure insert_file_content(v_file_id FILE_CONTENT.FILE_ID%TYPE,
    v_line_no FILE_CONTENT.LINE_NO%TYPE,
    v_line_content FILE_CONTENT.LINE_CONTENT%TYPE,
    v_record_by FILE_CONTENT.RECORD_CREATED_BY%TYPE)
as
begin
	INSERT INTO FILE_CONTENT (FILE_ID, LINE_NO, LINE_CONTENT, RECORD_CREATED_BY) 
	VALUES(v_file_id, v_line_no, v_line_content, v_record_by);
end;



```
5. Change the connection string for Oracle connectivity in appsettings.json in App.Oracle.Core.Web.API

### Other Applciation Configurations

1. Check the Web API URL in appsettings.json in App.Oracle.Core.Web.MVC under WebService section
2. Check the Web API URL in appsettings.json in App.Oracle.Core.Worker.Service under WebService section

### Web API Basic Authentication Credentials

username: api-local-user
password: Api@2022
