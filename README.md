# Candidate Management System Web Application
A ASP.NET MVC application with code first database (+dummy data) using entity framework.
Provides the following functionalities to the user:
  - Admin Service (CRUD on candidates and show all certificates for every Candidate)
  - Candidate Service (Login as a candidate to see your certificates or download them to a PDF file)

Includes 3 Projects
  - EFDataAccess (Class Library) that holds all the models, entities, and AppContext for the web application
  - WebApplication that holds the application
  - UnitTests used to run the unit tests for CRUD on admin service and verify the data
