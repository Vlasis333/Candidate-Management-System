# Candidate Management System (Web Application) - .NET Framework 4.8
The Candidate Management System Web Application is a robust, user-friendly solution for managing candidate information. Built using ASP.NET MVC and utilizing a Code First approach with Entity Framework and utilizing asynchronous programming, this application offers a comprehensive set of features to streamline the management of candidate data. 

The application includes a powerful admin service that allows for CRUD operations on candidates and the ability to view all certificates for each candidate. Additionally, candidates have access to a candidate service that allows them to login and view or download their certificates in PDF format. Lastly includes a certificate manager with CRUD operations for the certificates entities.

The application is composed of three projects: 
  - EFDataAccess: a class library containing the models, entities, and AppContext for the application, 
  - WebApplication: which houses the main application
  - UnitTests: which are used to perform CRUD operations on the admin service and verify data integrity
