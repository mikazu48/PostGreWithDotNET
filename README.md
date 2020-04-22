# PostGreWithDotNET
API CRUD with Web Api 2 (.NET) MVC with PostgreSQL Database.

# Token Auth
- Startup.Auth 
- - ConfigureAuth() : set Token expired time.
- ApplicationOAuthProvider
- - GrantResourceOwnerCredentials : check username & password for generate new token.

# API Controllers
- Pqs_Account 
- - [HttpPost] CreateAccount : create new account data / register.
- - [HttpPost] UpdateAccount : update account data / change password.
- Employee
- - [HttpGet] GetListEmployee : get all list employee data.
- - [HttpGet] GetSingleEmployee : get specific employee data with parameters/where employee_id.
- - [HttpPost] CreateEmployee : create employee data.
- - [HttpPut] UpdateEmployee : update employee data.
- - [HttpDelete] DeleteEmployee : delete specific employee data with parameters/where employee_id.
- Area
- - [HttpGet] GetListArea : get all list area data.
- - [HttpGet] GetSingleArea : get specific area data with parameters/where area_id.
- - [HttpPost] CreateArea : create area data.
- - [HttpPut] UpdateArea : update area data.
- - [HttpDelete] DeleteArea : delete specific area data with parameters/where area_id.

# Database
For database please use the file :
```
backupdb.sql
```
