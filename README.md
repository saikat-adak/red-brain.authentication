# RedBrain user management and authentication service

It is an ASP.NET Core 3.1 API that supports user registration, login with JWT authentication and user management. This API is configured to use a local SQLite database in development and a SQL Server database in production. It uses EF Core Migrations to automatically generate the database on startup.

## Running the ASP.NET Core Authentication API Locally

Download or clone the project  and start the kestrel server by running 'dotnet run' from the command line in the project root folder (where the RedBrain.Authentication.csproj file is located), you should see the message Now listening on: http://localhost:5000. Follow the instructions below to test with Postman:


## How to register a new user with Postman
To register a new user with the api follow these steps:

1. Open a new request tab by clicking the plus (+) button at the end of the tabs.
0. Change the http request method to "POST" with the dropdown selector on the left of the URL input field.
0. In the URL field enter the address to the register route of your local API - http://localhost:5000/users/register.
0. Select the "Body" tab below the URL field, change the body type radio button to "raw", and change the format dropdown selector to "JSON (application/json)".
0. Enter a JSON object containing the required user properties in the "Body" textarea, e.g:

        {  
            "firstName": "Saikat",
            "lastName": "Adak",
            "username": "saikat",
            "password": "some-password-123",
            "tenant": "my-app-1"
        }

0. Click the "Send" button, you should receive a "200 OK" response with an empty JSON object in the response body.


## How to authenticate a user with Postman
To authenticate a user with the api and get a JWT token follow these steps:
1. Open a new request tab by clicking the plus (+) button at the end of the tabs.
0. Change the http request method to "POST" with the dropdown selector on the left of the URL input field.
0. In the URL field enter the address to the authenticate route of your local API - http://localhost:5000/users/authenticate.
0. Select the "Body" tab below the URL field, change the body type radio button to "raw", and change the format dropdown selector to "JSON (application/json)".
0. Enter a JSON object containing the username and password in the "Body" textarea:

        {
            "username": "saikat",
            "password": "some-password-123",
            "tenant": "my-app-1"
        }

0. Click the "Send" button, you should receive a "200 OK" response with the user details including a JWT token in the response body, make a copy of the token value because we'll be using it in the next step to make an authenticated request.


## How to make an authenticated request to retrieve all users
To make an authenticated request using the JWT token from the previous step, follow these steps:

1. Open a new request tab by clicking the plus (+) button at the end of the tabs.
0. Change the http request method to "GET" with the dropdown selector on the left of the URL input field.
0. In the URL field enter the address to the users route of your local API - http://localhost:5000/users.
0. Select the "Authorization" tab below the URL field, change the type to "Bearer Token" in the type dropdown selector, and paste the JWT token from the previous authenticate step into the "Token" field.
0. Click the "Send" button, you should receive a "200 OK" response containing a JSON array with all the user records in the system.


## How to update a user with Postman
To update a user with the api follow these steps:

1. Open a new request tab by clicking the plus (+) button at the end of the tabs.
0. Change the http request method to "PUT" with the dropdown selector on the left of the URL input field.
0. In the URL field enter the address to the /users/{id} route with the id of the user you registered above, e.g - http://localhost:5000/users/1.
0. Select the "Authorization" tab below the URL field, change the type to "Bearer Token" in the type dropdown selector, and paste the JWT token from the previous authenticate step into the "Token" field.
0. Select the "Body" tab below the URL field, change the body type radio button to "raw", and change the format dropdown selector to "JSON (application/json)".
0. Enter a JSON object in the "Body" textarea containing the properties you want to update, for example to update the first and last names:

        {
            "firstName": "Brucee",
            "lastName": "Lei",
            "email": "another.email@gmail.com",
            "mobile": "123456789"
        }

0. Click the "Send" button, you should receive a "200 OK" response with an empty JSON object in the response body.
