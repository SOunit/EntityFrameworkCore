# naming convention

- primary key
  - Id
- foreign key
  - `TableId` and `Table`
  - StudentId
  - Student

# steps to use EntityFramework

- create db classes
- install `EntityFrameworkCore.SqlServer`
- create `dbContext` class

  - add project reference to Data
    - right click on Dependencies
    - add project reference
  - override configuration
  - setup db-connection-string
    - to pass connection data to config file
- import `EntityFrameworkCore.Tools`
- add dependency from main project to data, domain project
  - create database command have to run in main project

- get help from entity-framework
  - command
     ```
     get-help entityframework
     ```

- use local db
  - SQL Server Object Explorer
    - open from window tab

# saveChanges
- actually generate SQL and execute it.
- context.tableName.Add is just in memory