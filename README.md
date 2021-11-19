# EHIProject

Frontend Project Setup
1) Clone the repository.
2) Open Angular App "EHIApp" in Visual Studio Code and run the command "npm install".
3) Run the command "ng serve". App will open at http://localhost:4200.


Database Setup:
1) Run the CreateTable.sql file in Microsoft Sql Server. This will create a database named 'EHI' and a table named 'Contact' .

Backend Project Setup:

1) Open the "EHI" project in Visual Studio by double clicking on "EHI.sln" file.
2) Update the connecting string in appsettings.json file in EHI.WebApi Project.
3) Update the connecting string in ContactUnitTest.cs file in EHI.ContactUnitTest Project.
2) Build the project.
3) Make EHI.WebApi as startup project. Then Run.
4) WebAPI will run on  http://localhost:4200.