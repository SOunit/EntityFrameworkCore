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

- use local db
  - SQL Server Object Explorer
    - open from window tab
